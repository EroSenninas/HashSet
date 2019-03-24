using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    public class HashSet
    {
        public static int DEFAULT_INITIAL_CAPACITY = 16;
        public static float LOAD_FACTOR = 0.8f;

        public Node[] table;

        private int size = 0;
        private float loadFactor;

        protected int rehashesCounter = 0;
        protected int lastUpdatedChain = 0;
        protected int chainsCounter = 0;
        protected int index = 0;

        public HashSet()
        {
            this.table = new Node[DEFAULT_INITIAL_CAPACITY];
            this.loadFactor = LOAD_FACTOR;
        }

        public HashSet(int initialCapacity)
        {
            table = new Node[initialCapacity];
            this.loadFactor = LOAD_FACTOR;
        }

        public int GetChains()
        {
            return chainsCounter;
        }

        public int Rehashes()
        {
            return rehashesCounter;
        }

        public string TwoElements(string value1, string value2)
        {
            int key1 = Hash(value1);
            int key2 = Hash(value2);
            if (key1 == key2)
            {
                return "Both elements are in " + key1 + " chain";
            }
            else
                return "Elements are in different chains:" + key1 + " and " + key2;
        }

        public void Put(string value)
        {
            int key = Hash(value);
            Node node = new Node(key, value);
            if (table.Length * loadFactor <= size)
            {
                Rehash(value);
            }
            else if (table[key] != null && key == table[key].getKey())
            {
                node.setNextNode(table[key].getNextNode());
                table[key].setNextNode(node);
                lastUpdatedChain = key;
            }
            else
            {
                table[key] = node;
                chainsCounter++;
                size++;
            }
        }

        public string Get(string value)
        {
            int key = Hash(value);
            if (table[key] != null)
            {
                Node current = table[key];
                while (current.getKey() != key && current.getNextNode() != null)
                {
                    current = current.getNextNode();
                }
                if (current.getKey() == key)
                {
                    return current.getData();
                }
            }
            return null;
        }

        public bool Contains(string value)
        {
            if (Get(value) != null)
            {               
                return true;                  
            }
            else 
                return false;
        }

        private int Hash(string value)
        {
            if (value != null)
            {
                int index = Math.Abs(value.GetHashCode() % table.Length);
                return index;
            }
            else
                throw new ArgumentException("Value equals null");
        }


        private void Rehash(string value)
        {
            HashSet map = new HashSet(table.Length * 2);
            
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null)
                {
                    map.Put(table[i].getData());
                }
                
            }
            map.Put(value);
            table = map.table;
            chainsCounter = map.chainsCounter;
            rehashesCounter++;
        }

        public void Print()
        {
            Node current = null;
            for (int i = 0; i < size; i++)
            {
                current = table[i];
                while (current != null)
                {
                    Console.Write(current.getData() + " ");
                    current = current.getNextNode();
                }
                Console.WriteLine();
            }
        }


        public class Node
        {
            public int key;
            public string value;
            public Node next;

            public Node(int key, string value)
            {
                this.key = key;
                this.value = value;
                next = null;
            }

            public int getKey()
            {
                return key;
            }

            public string getData()
            {
                return value;
            }

            public void setNextNode(Node obj)
            {
                next = obj;
            }

            public Node getNextNode()
            {
                return this.next;
            }
        }
    }
}
