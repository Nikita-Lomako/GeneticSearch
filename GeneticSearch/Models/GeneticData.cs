using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticSearch.Models
{
    public struct GeneticData
    {
        public string Protein { get; private set; }
        public string Organism { get; private set; }
        public string AminoAcids { get; private set; }

        public GeneticData(string protein, string organism, string aminoAcids)
        {
            Protein = protein;
            Organism = organism;
            AminoAcids = aminoAcids;
        }
    }
}


