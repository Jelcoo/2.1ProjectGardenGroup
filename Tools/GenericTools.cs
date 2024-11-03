using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public class GenericTools
    {
        public static int GenerateNumber(int min, int max)
        {
            Random r = new Random();
            return r.Next(min, max);
        }
    }
}
