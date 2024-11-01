using GeneticSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticSearch.Interfaces
{
    public interface ICommandProcessor
    {
        void ProcessCommand(Command command);
    }
}

