using PersonalFinanceApp.Components.Data;
using PersonalFinanceApp.Components.Entities;

namespace PersonalFinanceApp.Components.Models;

// Service that manages transaction operations and provides financial calculations
public class TransactionService
{
    private readonly IRepository<ITransaction> _repo;
    private readonly BudgetService _budgetService;
    private readonly List<ITransaction> _transactions;

    public TransactionService(IRepository<ITransaction> repo, BudgetService budgetService)
    {
        _repo = repo;
        _budgetService = budgetService;
        _transactions = _repo.Load();
    }
    
    public IEnumerable<ITransaction> GetAll() => _transactions;
    
    // Adds a new transaction, assigns an ID, persists it, and updates related budgets
    public void AddTransaction(ITransaction transaction)
    {
        transaction.Id = _transactions.Any() ? _transactions.Max(t => t.Id) + 1 : 1;
        _transactions.Add(transaction);
        _repo.Append(transaction);
        Console.WriteLine($"[DEBUG] Added {transaction.GetType().Name}: {transaction.Category}, {transaction.Amount}");
        _budgetService?.UpdateProgress(transaction);
    }

    public void DeleteTransaction(int transactionId)
{
    var transactionToRemove = _transactions.FirstOrDefault(t => t.Id == transactionId);
    
    if (transactionToRemove != null)
    {
        _transactions.Remove(transactionToRemove);
        _repo.Save(_transactions); 
        Console.WriteLine($"[DEBUG] Deleted transaction {transactionId}");
    }
}
    
    public IEnumerable<Expense> GetExpenses() =>
        _transactions.OfType<Expense>();
    
    public IEnumerable<Income> GetIncome() =>
        _transactions.OfType<Income>();
    
    public decimal GetTotalExpenses() =>
        _transactions.OfType<Expense>().Sum(e => e.Amount);
    
    public decimal GetTotalIncome() =>
        _transactions.OfType<Income>().Sum(i => i.Amount);
    
    // Calculates the current balance by subtracting total expenses from total income
    public decimal GetBalance() =>
        GetTotalIncome() - GetTotalExpenses();
}