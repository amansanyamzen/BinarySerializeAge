using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace BinarySerialization
{
    [Serializable]
    class Binary : IDeserializationCallback
    {
        public int Year { get; set; }
        [NonSerialized]
        public int Age;

        public Binary(int year)
        {
            this.Year = year;

        }


        public void OnDeserialization(object sender)
        {
            DateTime d = DateTime.Now;
            Age = DateTime.Now.Year - Year;


        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your birth year:");
            int Year = int.Parse(Console.ReadLine());
            Binary by = new Binary(Year);
            FileStream fs = new FileStream(@"dob.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, by);
            fs.Seek(0, SeekOrigin.Begin);
            Binary b1 = (Binary)bf.Deserialize(fs);
            Console.WriteLine( $"Age : {b1.Age}");

        }
    }
}