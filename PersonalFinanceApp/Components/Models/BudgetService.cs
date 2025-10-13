using BlazorApp1.Components.Data;
using BlazorApp1.Components.Entities;

namespace BlazorApp1.Components.Models;

public class BudgetService
{
    private readonly BudgetRepo _repo;
    private readonly List<Budget> _budgets;

    public BudgetService(BudgetRepo repo)
    {
        _repo = repo;
        _budgets = _repo.Load();
    }

    public IEnumerable<Budget> GetAll() => _budgets;
    
    public void AddBudget(Budget budget)
    {
        budget.Id = _budgets.Count + 1;
        _budgets.Add(budget);
        _repo.Append(budget);
    }
    
    public void UpdateProgress(Transaction transaction)
    {
        foreach (var budget in _budgets)
        {
            if (transaction.Date >= budget.StartDate && transaction.Date <= budget.EndDate)
            {
                if (transaction is Expense)
                {
                    budget.CurrentAmount -= transaction.Amount;
                }
                else if (transaction is Income)
                {
                    budget.CurrentAmount += transaction.Amount;
                }
            }
        }

        _repo.Save(_budgets);
    }
    
    public decimal GetProgress(Budget budget) =>
        budget.CurrentAmount / budget.TargetAmount;
}
    