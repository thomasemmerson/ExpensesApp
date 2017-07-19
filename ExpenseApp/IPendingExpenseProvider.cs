namespace Expenses
{
    interface IPendingExpenseProvider
    {
        ExpenseRequest GetPendingExpenseRequest();
    }
}