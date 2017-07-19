﻿using System;
using System.IO;
using System.Net;
using System.Text;

namespace Expenses
{
    public class ExpenseRequest
    {
        public string PayrollId { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
    }
    class UI : IUI
    {
        public void DisplayWelcomeMessage()
        {
            Console.WriteLine("Welcome to the awesome expenses app.");
        }
        public void DisplayPendingExpense(ExpenseRequest expense)
        {
            Console.WriteLine("There is a 1 pending request awaiting your approval");
            Console.WriteLine($"Mark Kirschstein ({expense.PayrollId}) - £{expense.Amount} for {expense.Description}");
        }
        public void DeclineRequest()
        {
            Console.WriteLine("Expense declined.");
        }
        public void Exit()
        {
            Console.WriteLine("Press any key to quit");
            Console.Read();
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            var ui = new UI();
            Main(ui);
        }

        static void Main(UI ui)
        {
            ui.DisplayWelcomeMessage();
            var expense = GetPendingExpenseRequest();
            ui.DisplayPendingExpense(expense);
            var approvalDecision = GetApprovalForRequest();
            if (approvalDecision == "YES")
            {
                ApproveRequest(expense);
            }
            else
            {
                ui.DeclineRequest();
            }
            ui.Exit();
        }

        private static void ApproveRequest(ExpenseRequest expense)
        {
            Console.WriteLine("Approving...");
            var responseContent = SendRequestToPayRollServer(expense);
            Console.WriteLine("Expense approved.");
            Console.WriteLine("Response from server: " + responseContent);
        }

        private static string GetApprovalForRequest()
        {
            Console.WriteLine();
            Console.WriteLine("Type YES to approve. (Anything else will decline)");
            return Console.ReadLine();
        }


        private static ExpenseRequest GetPendingExpenseRequest()
        {
            return new ExpenseRequest
            {
                PayrollId = "75831",
                Description = "London Client Visit",
                Amount = 125.66
            };
        }

        private static string SendRequestToPayRollServer(ExpenseRequest expense)
        {
            var data = Encoding.ASCII.GetBytes($"payrollId={expense.PayrollId}&description={expense.Description}&amount={expense.Amount}");
            var request =
                WebRequest.Create(
                    "https://addpayrollitem.azurewebsites.net/api/AddPayRoleItem?code=gAdm5Wsr35a7nKXKlbuY9kMAWfNfjv3bWnyzR74aDpn8sL9ifM/tTg==");
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
            return responseContent;
        }
    }
}