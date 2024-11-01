using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.IO;
using GeneticSearch.Models;
using GeneticSearch.Services;

namespace GeneticSearch.Helpers
{
    public static class FileHelper
    {
        public static List<GeneticData> LoadGeneticData(string filePath)
        {
            var geneticDataList = new List<GeneticData>();

            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var data = line.Split('\t');
                    if (data.Length == 3)
                    {
                        geneticDataList.Add(new GeneticData(data[0], data[1], RLECompressionService.RLDecoding(data[2])));
                    }
                }
            }

            return geneticDataList;
        }

        public static IEnumerable<Command> LoadCommands(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split('\t');
                    if (parts.Length > 0)
                    {
                        yield return new Command(parts[0], parts[1..]);
                    }
                }
            }
        }
    }
}

