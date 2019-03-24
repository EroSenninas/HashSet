using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    public class HashSet_D
    {
        public static int DEFAULT_INITIAL_CAPACITY = 16;
        public static float LOAD_FACTOR = 0.8f;

        private int size = 0;
        private float loadFactor;
        private int capacity;

        protected int rehashesCounter = 0;
        protected int lastUpdatedChain = 0;
        protected int chainsCounter = 0;
        protected int index = 0;

        public HashSet_D()
        {
            this.loadFactor = LOAD_FACTOR;
            this.capacity = DEFAULT_INITIAL_CAPACITY;
            for (int i = 0; i < capacity; i++)
            {
                string fileName = String.Format(@"hashEntry{0}.dat", i);
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                try
                {
                    using (BinaryWriter writer = new BinaryWriter(File.Open(fileName,
                    FileMode.Create))) {}
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }  
        }

        public HashSet_D(int capacity)
        {
            this.loadFactor = LOAD_FACTOR;
            this.capacity = capacity;
            for (int i = 0; i < capacity; i++)
            {
                string fileName = String.Format(@"hashEntry{0}.dat", i);
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                try
                {
                    using (BinaryWriter writer = new BinaryWriter(File.Open(fileName,
                    FileMode.Create))) {}
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public FileStream fs { get; set; }

        public int GetChains()
        {
            return chainsCounter;
        }

        public int Rehashes()
        {
            return rehashesCounter;
        }

        public void Put(string value)
        {
            if (capacity * loadFactor <= size)
            {
                Rehash(value);
            }
            else
            {
                int key = Hash(value);
                string fileName = String.Format(@"hashEntry{0}.dat", key);
                string text = System.IO.File.ReadAllText(fileName);
                int byteLength = Encoding.ASCII.GetBytes(text).Count();
                if (byteLength > 0)
                {
                    try
                    {
                        using (BinaryWriter writer = new BinaryWriter(File.Open(fileName,
                        FileMode.Append)))
                        {
                            writer.Write(value + Environment.NewLine);
                            writer.Close();
                            lastUpdatedChain = key;
                        }
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
                else
                {
                    try
                    {
                        using (BinaryWriter writer = new BinaryWriter(File.Open(fileName,
                        FileMode.Append)))
                        {
                            writer.Write(value);
                            writer.Close();
                            chainsCounter++;
                            size++;
                        }
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
        }

        private int Hash(string value)
        {
            if (value != null)
            {
                int index = Math.Abs(value.GetHashCode() % capacity);
                return index;
            }
            else
                throw new ArgumentException("Value equals null");
        }

        public bool Contains(string value)
        {
            int key = Hash(value);
            string fileName = String.Format(@"hashEntry{0}.dat", key);
            string text = System.IO.File.ReadAllText(fileName);
            int byteLength = Encoding.ASCII.GetBytes(text).Count();
            if (byteLength > 0)
            {
                using (var reader = new StreamReader(fileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Equals(value))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        private void Rehash(string value)
        {
            HashSet_D map = new HashSet_D(capacity * 2);
            int key = Hash(value);
            for (int i = 0; i < capacity; i++)
            {
                string fileName = String.Format(@"hashEntry{0}.dat", i);
                string text = System.IO.File.ReadAllText(fileName);
                int byteLength = Encoding.ASCII.GetBytes(text).Count();
                if (byteLength > 0)
                {
                    using (var reader = new StreamReader(fileName))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            map.Put(line);                         
                        }
                    }
                }
            }
            map.Put(value);
            chainsCounter = map.chainsCounter;
            rehashesCounter++;
        }

        public void Print()
        {
            for (int i = 0; i < capacity; i++)
            {
                string fileName = String.Format(@"hashEntry{0}.dat", i);
                string text = System.IO.File.ReadAllText(fileName);
                int byteLength = Encoding.ASCII.GetBytes(text).Count();
                if (byteLength > 0)
                {
                    using (var reader = new StreamReader(fileName))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(line);
                        }
                    }
                }

            }
        }

        public void Clean()
        {
            for (int i = 0; i < capacity; i++)
            {
                string fileName = String.Format(@"hashEntry{0}.dat", i);
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }
        }
    }
}
