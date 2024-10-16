﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class PartialUser
    {
        public string Id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }

        public override string ToString()
        {
            return name;
        }
    }
}