using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticSearch.Models
{
    public class Command
    {
        public string Operation { get; set; }
        public string[] Parameters { get; set; }

        public Command(string operation, string[] parameters)
        {
            Operation = operation;
            Parameters = parameters;
        }
    }
}


