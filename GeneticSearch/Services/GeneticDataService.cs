using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// мои
using GeneticSearch.Models;
using GeneticSearch.Interfaces;

namespace GeneticSearch.Services
{
    public class GeneticDataService : ICommandProcessor
    {
        private List<GeneticData> _geneticDataList;
        private int _operationNumber = 0;
        private string _outputFilePath;

        // Конструктор принимает путь к выходному файлу
        public GeneticDataService(List<GeneticData> geneticDataList, string outputFilePath)
        {
            _geneticDataList = geneticDataList;
            _outputFilePath = outputFilePath;
        }

        // Обработка команды (вызывает соответствующий метод в зависимости от операции)
        public void ProcessCommand(Command command)
        {
            switch (command.Operation)
            {
                case "header":
                    WriteOutput($"{command.Parameters[0]}\n{command.Parameters[1]}");
                    break;
                case "search":
                    WriteOutput($"{_operationNumber:000}\tsearch\t{RLECompressionService.RLDecoding(command.Parameters[0])}\norganism\t\tprotein");
                    SearchAminoAcids(RLECompressionService.RLDecoding(command.Parameters[0]));
                    break;
                case "diff":
                    WriteOutput($"{_operationNumber:000}\tdiff\t{command.Parameters[0]}\t{command.Parameters[1]}");
                    DiffProteins(command.Parameters[0], command.Parameters[1]);
                    break;
                case "mode":
                    WriteOutput($"{_operationNumber:000}\tmode\t{command.Parameters[0]}");
                    FindMostFrequentAminoAcid(command.Parameters[0]);
                    break;
                default:
                    WriteOutput("Unknown command.");
                    break;
            }
            _operationNumber++;
        }

        // Поиск аминокислотной последовательности
        private void SearchAminoAcids(string aminoSequence)
        {
            bool found = false;
            foreach (var data in _geneticDataList)
            {
                if (data.AminoAcids.Contains(aminoSequence))
                {
                    WriteOutput($"{data.Organism}\t{data.Protein}");
                    found = true;
                }
            }

            if (!found)
            {
                WriteOutput("NOT FOUND");
            }
        }

        // Сравнение двух белков
        private void DiffProteins(string protein1, string protein2)
        {
            var data1 = _geneticDataList.FirstOrDefault(d => d.Protein == protein1);
            var data2 = _geneticDataList.FirstOrDefault(d => d.Protein == protein2);

            if (data1.Protein == null || data2.Protein == null)
            {
                WriteOutput($"MISSING: " + (data1.Protein == null ? protein1 : "") + (data2.Protein == null ? protein2 : ""));
                return;
            }

            int diffLength = Math.Abs(data1.AminoAcids.Length - data2.AminoAcids.Length);
            int diffCount = data1.AminoAcids.Zip(data2.AminoAcids, (c1, c2) => c1 != c2 ? 1 : 0).Sum();
            WriteOutput($"amino-acids difference:\n{diffCount + diffLength}");
        }

        // Поиск наиболее частой аминокислоты
        private void FindMostFrequentAminoAcid(string protein)
        {
            var data = _geneticDataList.FirstOrDefault(d => d.Protein == protein);

            if (data.Protein == null)
            {
                WriteOutput($"MISSING: {protein}");
                return;
            }

            var mostFrequent = data.AminoAcids
                .GroupBy(c => c)
                .OrderByDescending(g => g.Count())
                .ThenBy(g => g.Key)
                .First();

            WriteOutput($"amino-acid occurs:\n{mostFrequent.Key}\t{mostFrequent.Count()}");
        }

        // Функция для записи данных в файл
        private void WriteOutput(string output)
        {
            using (StreamWriter writer = new StreamWriter(_outputFilePath, true)) // true для дозаписи в файл
            {
                writer.WriteLine(output);
            }
        }
    }
}

