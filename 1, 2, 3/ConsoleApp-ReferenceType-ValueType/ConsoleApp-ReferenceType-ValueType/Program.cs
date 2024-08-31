using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_ReferenceType_ValueType
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //value type
            int a = 10; //set sebagai int
            Console.WriteLine($"contoh value type a: {a}");

            //reference type - diambil dari class ClsData
            ClsData cls = new ClsData();
            cls.Id = 10; //set Id dari cls menjadi 10 (int)
            cls.Name = "Namaku"; //set string
            Console.WriteLine($"contoh reference type Id: {cls.Id}");
            Console.WriteLine($"contoh reference type Name: {cls.Name}");

            Console.ReadLine();
        }
    }

    public class ClsData
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
