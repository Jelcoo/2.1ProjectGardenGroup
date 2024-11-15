﻿using Model.Enums;
using Model.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DAL
{
	public class TicketDao : DAO
	{
		public TicketDao() : base()
		{
		}

		public void SaveTicket(Ticket ticket)
		{
			IMongoCollection<Ticket> ticketCollection = Db.GetCollection<Ticket>("tickets");
			ticketCollection.InsertOne(ticket);
		}

		public List<Ticket> GetTicketsEmployees(PartialUser loggedInUser)
		{
			IAggregateFluent<Ticket> tickets = Db.GetCollection<Ticket>("tickets")
				.Aggregate()
				.Match(e => e.status == "open");

			if (loggedInUser.role == Role.Employee)
			{
				tickets = tickets.Match(t => t.reported_by._id == loggedInUser._id);
			}

			return tickets.SortByDescending(t => t.created_at)
				.Limit(25)
				.ToList();
		}

		public List<Ticket> GetTicketsByStatus(Status_Enum status)
		{
			List<Ticket> tickets = Db.GetCollection<Ticket>("tickets")
				.Aggregate()
				.Match(Builders<Ticket>.Filter.Eq(ticket => ticket.status, status.ToString()))
				.SortByDescending(t => t.created_at)
				.Limit(25)
				.ToList();

			return tickets;
		}

		public List<Ticket> SearchTickets(string searchQuery, PartialUser loggedInUser)
		{
			FilterDefinitionBuilder<Ticket> builder = Builders<Ticket>.Filter;
			FilterDefinition<Ticket> filter = builder.Empty;

			if (searchQuery.Contains(" AND ") || searchQuery.Contains(" & "))
			{
				filter = CreateAndFilter(searchQuery, builder);
			}
			else if (searchQuery.Contains(" OR ") || searchQuery.Contains(" | "))
			{
				filter = CreateOrFilter(searchQuery, builder);
			}
			else
			{
				filter = CreateAndFilter(searchQuery, builder);
			}

			IAggregateFluent<Ticket> tickets = Db.GetCollection<Ticket>("tickets")
				.Aggregate()
				.Match(filter);

			if (loggedInUser.role == Role.Employee)
			{
				tickets = tickets.Match(t => t.reported_by._id == loggedInUser._id);
			}

			return tickets.Sort(Builders<Ticket>.Sort.Descending(ticket => ticket.created_at))
				.Limit(25)
				.ToList();
		}

		private static FilterDefinition<Ticket> CreateOrFilter(string searchQuery, FilterDefinitionBuilder<Ticket> builder)
		{
			// split the search where "OR" or "|" is found
			string[] terms = searchQuery.Split(new[] { " OR ", " | " }, StringSplitOptions.RemoveEmptyEntries);

			//search for one of the terms in the title or description
			return builder.Or(terms.Select(term =>
				builder.Or(
					builder.Regex(x => x.title, new BsonRegularExpression(term.Trim(), "i")),
					builder.Regex(x => x.description, new BsonRegularExpression(term.Trim(), "i"))
				)
			));
		}

		private static FilterDefinition<Ticket> CreateAndFilter(string searchQuery, FilterDefinitionBuilder<Ticket> builder)
		{
			// split the search where "AND" or "&" is found
			string[] terms = searchQuery.Split(new[] { " AND ", " & " }, StringSplitOptions.RemoveEmptyEntries);

			//search for all term in the title or description
			return builder.And(terms.Select(term =>
				builder.Or(
					builder.Regex(x => x.title, new BsonRegularExpression(term.Trim(), "i")),
					builder.Regex(x => x.description, new BsonRegularExpression(term.Trim(), "i"))
				)
			));
		}

		public void ChangeTicketStatus(ObjectId ticketId, Status_Enum status)
		{
			var tickets = Db.GetCollection<Ticket>("tickets")
				.UpdateOne(
				Builders<Ticket>.Filter.Eq(ticket => ticket._id, ticketId),
				Builders<Ticket>.Update.Set(ticket => ticket.status, status.ToString())
				);
		}

		public List<TicketsCount> GetTicketsCountByStatus(string status)
		{
			try
			{
				List<TicketsCount> tickets = Db.GetCollection<Ticket>("tickets")
					.Aggregate()
					.Match(e => e.status == status)
					.Group(
						g => $"{status} tickets",
						g => new TicketsCount
						{
							_id = g.Key,
							count = g.Count()
						}).ToList();

				return tickets;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			return null;
		}

		public List<TicketsCount> GetTicketsPastDeadlineCount()
		{
			var tickets = Db.GetCollection<Ticket>("tickets")
				.Aggregate()
				.AppendStage<BsonDocument>(new BsonDocument
				{
					{ "$match", new BsonDocument
						{
							{ "status", "open" }
						}
					}
				})
				.AppendStage<BsonDocument>(new BsonDocument
				{
					{ "$addFields", new BsonDocument
						{
							{ "createdAtDate", new BsonDocument
								{
									{ "$dateFromString", new BsonDocument { { "dateString", "$created_at" } } }
								}
							}
						}
					}
				})
				.AppendStage<BsonDocument>(new BsonDocument
				{
					{ "$match", new BsonDocument
						{
							{ "createdAtDate", new BsonDocument { { "$lt", DateTime.UtcNow } } }
						}
					}
				})
				.AppendStage<BsonDocument>(new BsonDocument
				{
					{ "$group", new BsonDocument
						{
							{ "_id", "Tickets Past Deadline" },
							{ "count", new BsonDocument { { "$sum", 1 } } }
						}
					}
				})
				.ToList()
				.Select(g => new TicketsCount
				{
					_id = g["_id"].AsString,
					count = g["count"].AsInt32
				})
				.ToList();

			return tickets;
		}

		public void UpdateEmployeeNameInTickets(string employeeId, string newName)
		{
			var filter = Builders<Ticket>.Filter.Or(
				Builders<Ticket>.Filter.Eq("reported_by.Id", employeeId),
				Builders<Ticket>.Filter.Eq("assigned_to.Id", employeeId),
				Builders<Ticket>.Filter.Eq("resolved_by.Id", employeeId)
			);

			var update = Builders<Ticket>.Update
				.Set("reported_by.name", newName)
				.Set("assigned_to.name", newName)
				.Set("resolved_by.name", newName);

			Db.GetCollection<Ticket>("tickets").UpdateMany(filter, update);
		}

		public Ticket GetTicketById(ObjectId ticketId)
		{
			var filter = Builders<Ticket>.Filter.Eq("_id", ticketId);
			return Db.GetCollection<Ticket>("tickets").Find(filter).FirstOrDefault();
		}

		public void AddCommentIdToTicket(ObjectId ticketId, ObjectId commentId)
		{
			var filter = Builders<Ticket>.Filter.Eq("_id", ticketId);
			var update = Builders<Ticket>.Update.Push("commentIds", commentId);
			Db.GetCollection<Ticket>("tickets").UpdateOne(filter, update);
		}

		public void UpdateAssigneTo(Ticket ticket, PartialUser employee)
		{
			var filter = Builders<Ticket>.Filter.Or(
				Builders<Ticket>.Filter.Eq("_id", ticket._id)
			);

			var update = Builders<Ticket>.Update
				.Set("assigned_to._id", employee._id)
				.Set("assigned_to.name", employee.name)
				.Set("resolved_by.email", employee.email)
				.Set("resolved_by.phone_number", employee.phone_number);

			Db.GetCollection<Ticket>("tickets").UpdateOne(filter, update);
		}
	}
}
