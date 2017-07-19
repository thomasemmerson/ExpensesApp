namespace Expenses
{
    interface IUI
    {
        void DeclineRequest();
        void DisplayPendingExpense(ExpenseRequest expense);
        void DisplayWelcomeMessage();
        void Exit();
    }
}