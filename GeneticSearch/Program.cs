using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using GeneticSearch.Helpers;
using GeneticSearch.Models;
using GeneticSearch.Services;

namespace GeneticSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            string sequencesFile = Path.Combine(Environment.CurrentDirectory, "Data", "sequences.0.txt");
            string commandsFile = Path.Combine(Environment.CurrentDirectory, "Data", "commands.0.txt");
            string outputFile = Path.Combine(Environment.CurrentDirectory, "Data", "genedata.0.txt");

            // Каждый раз создаем новый файл
            if (File.Exists(outputFile))
            {
                File.Delete(outputFile);
            }

            List<GeneticData> geneticDataList = FileHelper.LoadGeneticData(sequencesFile);
            GeneticDataService geneticService = new GeneticDataService(geneticDataList, outputFile);

            // Начало программы
            geneticService.ProcessCommand(new Command("header", new string[] { "Nikita Lomako", "Genetic Searching" }));
            WriteSeparator(outputFile);

            foreach (var command in FileHelper.LoadCommands(commandsFile))
            {
                geneticService.ProcessCommand(command);
                WriteSeparator(outputFile); // Для разделения результатов операций
            }
        }

        // Функция для записи разделителя в файл
        private static void WriteSeparator(string outputFilePath)
        {
            using (StreamWriter writer = new StreamWriter(outputFilePath, true))
            {
                writer.WriteLine(new string('-', 75));
            }
        }
    }
}

