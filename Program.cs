using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSetTest();
            HashSetDTest();
        }

        public static void HashSetTest()
        {
            
            Random rand = new Random();
            List<string> names = File.ReadAllLines(@"E:\Programavimas\Semestras_04\Algortimu_sudarymas\Labaratorinis_01\HashTable\HashTable\vardai.txt").ToList();
            List<string> surenames = File.ReadAllLines(@"E:\Programavimas\Semestras_04\Algortimu_sudarymas\Labaratorinis_01\HashTable\HashTable\pavardes.txt").ToList();
            int operationsCount = 0;
            int quantity = 100;
            Console.WriteLine("Maisos lentele su sarasais operatyvioje atmintyje:");
            Console.WriteLine("______________________________________________________________");
            Console.WriteLine("| Elementu kiekis |  Paieskos laikas  | Grandineliu skaicius |");
            Console.WriteLine("|-----------------|-------------------|----------------------|");
            for (int i = 1; i <= 7; i++)
            {
                List<string> result = new List<string>();
                HashSet hashSetOP = new HashSet(quantity);
                for (int m = 0; m < quantity; m++)
                {
                    int rndName = rand.Next(names.Count - 1);
                    int rndSureName = rand.Next(surenames.Count - 1);
                    hashSetOP.Put(names[rndName] + " " + surenames[rndSureName]);
                    result.Add(names[rndName] + " " + surenames[rndSureName]);

                }
                var watch = System.Diagnostics.Stopwatch.StartNew();
                for (int j = 0; j < result.Count; j++)
                {
                    hashSetOP.Contains(result[j]);
                    //operationsCount = operationsCount + 3;
                }
                watch.Stop();

                Console.WriteLine("| {0,15} | {1,17} | {2,20} |", result.Count, watch.Elapsed, hashSetOP.GetChains());
                watch.Reset();
                operationsCount = 0;
                quantity = quantity * 2;
            }
            Console.WriteLine("|-----------------|-------------------|----------------------|");
            
            
        }

        public static void HashSetDTest()
        {
            
            Random rand = new Random();
            List<string> names = File.ReadAllLines(@"E:\Programavimas\Semestras_04\Algortimu_sudarymas\Labaratorinis_01\HashTable\HashTable\vardai.txt").ToList();
            List<string> surenames = File.ReadAllLines(@"E:\Programavimas\Semestras_04\Algortimu_sudarymas\Labaratorinis_01\HashTable\HashTable\pavardes.txt").ToList();
            int operationsCount = 0;
            int quantity = 100;
            Console.WriteLine("Maisos lentele su sarasais isorineje atmintyje:");
            Console.WriteLine("______________________________________________________________");
            Console.WriteLine("| Elementu kiekis |  Paieskos laikas  | Grandineliu skaicius |");
            Console.WriteLine("|-----------------|-------------------|----------------------|");
            for (int i = 1; i <= 7; i++)
            {
                HashSet_D hashSetD = new HashSet_D(quantity);
                List<string> result = new List<string>();

                for (int m = 0; m < quantity; m++)
                {
                    int rndName = rand.Next(names.Count - 1);
                    int rndSureName = rand.Next(surenames.Count - 1);
                    hashSetD.Put(names[rndName] + " " + surenames[rndSureName]);
                    result.Add(names[rndName] + " " + surenames[rndSureName]);

                }
                hashSetD.Print();
                var watch = System.Diagnostics.Stopwatch.StartNew();
                for (int j = 0; j < result.Count; j++)
                {
                    hashSetD.Contains(result[j]);
                   // operationsCount = operationsCount + 3;
                }
                watch.Stop();

                Console.WriteLine("| {0,15} | {1,17} | {2,20} |", result.Count, watch.Elapsed, hashSetD.GetChains());
                watch.Reset();
                operationsCount = 0;
                hashSetD.Clean();
                quantity = quantity * 2;
            }
            Console.WriteLine("|-----------------|-------------------|----------------------|");
        }
    }
}
