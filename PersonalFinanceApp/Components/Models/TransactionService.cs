using BlazorApp1.Components.Data;
using BlazorApp1.Components.Entities;

namespace BlazorApp1.Components.Models;

public class TransactionService
{
    private readonly TransactionRepo _repo;
    private readonly BudgetService _budgetService;
    private readonly List<Transaction> _transactions;

    public TransactionService(TransactionRepo repo, BudgetService budgetService)
    {
        _repo = repo;
        _budgetService = budgetService;
        _transactions = _repo.Load();
    }
    
    public IEnumerable<Transaction> GetAll() => _transactions;
    
    public void AddTransaction(Transaction transaction)
    {
        transaction.Id = _transactions.Count + 1;
        _transactions.Add(transaction);
        _repo.Append(transaction);
        Console.WriteLine($"[DEBUG] Added {transaction.GetType().Name}: {transaction.Description}, {transaction.Amount}");
        _budgetService?.UpdateProgress(transaction);
        // _transactions.Clear();
        // _transactions.AddRange(_repo.Load());
    }
    
    public IEnumerable<Expense> GetExpenses() =>
        _transactions.OfType<Expense>();
    
    public IEnumerable<Income> GetIncome() =>
        _transactions.OfType<Income>();
    
    public decimal GetTotalExpenses() =>
        _transactions.OfType<Expense>().Sum(e => e.Amount);
    
    public decimal GetTotalIncome() =>
        _transactions.OfType<Income>().Sum(i => i.Amount);
    
    public decimal GetBalance() =>
        GetTotalIncome() - GetTotalExpenses();
}