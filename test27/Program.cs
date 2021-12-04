using System;
using System.Collections.Generic;
using System.IO;

namespace test27
{
    class DataDna
    {
        List<string> elements = new List<string>();
        private Dictionary<string, string> people;
        string dbPath = null;

        public DataDna(string dbFileName)
        {
            dbPath = dbFileName;
            people = ReadDbFromFile(dbFileName);
        }
        
        private Dictionary<string, string> ReadDbFromFile(string dbFileName)
        {
            string[] lines = File.ReadAllLines(dbPath);
            Dictionary<string, string> dict = new Dictionary<string, string>();
            elements.AddRange(lines[0].Split(','));//заполнить список элементами для поиска                   
            for (int y = 1; y < lines.Length; y++)
            {
                string[] temp = lines[y].Split(',');
                string t = null;
                for (int j = 1; j < temp.Length; j++)
                {
                    t += temp[j];
                }
                dict.Add(t, temp[0]);
            }
            return dict;
        }
        
        private string CountingOneElement(string dna, string element)
        {
            int index = 0;
            int count = 0;
            int max = 0;

            while (index < dna.Length && dna.IndexOf(element, index) != -1)
            {
                index = dna.IndexOf(element, index);
                while (index < dna.Length && dna.IndexOf(element, index) == index)
                {
                    index = dna.IndexOf(element, index) + element.Length;
                    count++;
                }
                max = (count > max) ? count : max;
                count = 0;
            }
            return max.ToString();
        }

        string readDnaFromFile(string fileName)
        {
            return File.ReadAllText(fileName);
        }

        private string CountingAllElementsOneFile(string dna)
        {
            string output = null;
            for (int x = 1; x < elements.Count; x++)
            {
                output += CountingOneElement(dna, elements[x]);
            }
            return output;
        }
      
        public string Search(string dnaFileName)
        {
            string output = "No Match.";
            Dictionary<string, string> symbols = people;
            
            string elementsCountSequence = CountingAllElementsOneFile(readDnaFromFile(dnaFileName));

            if (symbols.ContainsKey(elementsCountSequence))
            {
                output = symbols[elementsCountSequence];
            }
            return output;
        }

        public void Print(string[,] symb)
        {
            for (int i = 0; i < symb.GetLength(0); i++)
            {
                for (int j = 0; j < symb.GetLength(1); j++)
                {
                    Console.Write(symb[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            foreach (var s in elements)
            {
                Console.Write(s + " ");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DataDna data = new DataDna("..\\..\\dna\\databases\\small.csv");
            for (int x = 1; x < 5; x++)
            {
                string pathSequence = "..\\..\\dna\\sequences\\" + x + ".txt";
                Console.WriteLine(data.Search(pathSequence));
            }
            data = new DataDna("..\\..\\dna\\databases\\large.csv");
            for (int x = 5; x < 21; x++)
            {
                string pathSequence = "..\\..\\dna\\sequences\\" + x + ".txt";
                Console.WriteLine(data.Search(pathSequence));
            }
            Console.ReadLine();
        }
    }
}