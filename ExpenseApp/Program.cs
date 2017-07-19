using System;
using System.IO;
using System.Net;
using System.Text;

namespace ExpenseApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the awesome expenses app.");
            Console.WriteLine("There is a 1 pending request awaiting your approval");
            Console.WriteLine("Mark Kirschstein (75831) - £125.66 for London Client Visit");
            Console.WriteLine();
            Console.WriteLine("Type YES to approve. (Anything else will decline)");
            var approve = Console.ReadLine();

            if (approve == "YES")
            {
                Console.WriteLine("Approving...");
                var payRollId = "75831";
                var description = "London Client Visit";
                var amount = 125.66;
                var data = Encoding.ASCII.GetBytes($"payrollId={payRollId}&description={description}&amount={amount}");
                var request = WebRequest.Create("https://addpayrollitem.azurewebsites.net/api/AddPayRoleItem?code=gAdm5Wsr35a7nKXKlbuY9kMAWfNfjv3bWnyzR74aDpn8sL9ifM/tTg==");
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                string responseContent;

                using (var response = request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            responseContent = reader.ReadToEnd();
                        }
                    }
                }
                Console.WriteLine("Expense approved.");
                Console.WriteLine("Response from server: " + responseContent);
            }
            else
            {
                Console.WriteLine("Expense declined.");
            }

            Console.WriteLine("Press any key to quit");
            Console.Read();
        }
    }
}