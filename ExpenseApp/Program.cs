using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Approve? (Y/N)");
            var response = Console.ReadLine();
            if (response.ToUpper() == "Y")
            {
                Console.WriteLine("Approved");
            } else
            {
                Console.WriteLine("Denied!");
            }
            Console.ReadKey();
        }
    }
}
