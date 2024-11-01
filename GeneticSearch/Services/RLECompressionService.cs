using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticSearch.Services
{
    public class RLECompressionService
    {
        public static string RLEncoding(string aminoAcids)
        {
            var result = new System.Text.StringBuilder();
            int count = 1;

            for (int i = 1; i < aminoAcids.Length; i++)
            {
                if (aminoAcids[i] == aminoAcids[i - 1])
                {
                    count++;
                }
                else
                {
                    if (count > 1)
                    {
                        result.Append(count);
                    }
                    result.Append(aminoAcids[i - 1]);
                    count = 1;
                }
            }
            if (count > 1) result.Append(count);
            result.Append(aminoAcids[^1]);

            return result.ToString();
        }

        public static string RLDecoding(string encodedAminoAcids)
        {
            var result = new System.Text.StringBuilder();
            int i = 0;

            while (i < encodedAminoAcids.Length)
            {
                if (char.IsDigit(encodedAminoAcids[i]))
                {
                    int count = int.Parse(encodedAminoAcids[i].ToString());
                    result.Append(new string(encodedAminoAcids[i + 1], count));
                    i += 2;
                }
                else
                {
                    result.Append(encodedAminoAcids[i]);
                    i++;
                }
            }

            return result.ToString();
        }
    }
}

