using PersonalFinanceApp.Components.Data;
using PersonalFinanceApp.Components.Entities;

namespace PersonalFinanceApp.Components.Models;

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
        // Check if budget already exists (for updates)
        var existing = _budgets.FirstOrDefault(b => b.Id == budget.Id);
        
        if (existing != null)
        {
            // Update existing budget
            existing.Name = budget.Name;
            existing.TargetAmount = budget.TargetAmount;
            existing.CurrentAmount = budget.CurrentAmount;
            existing.StartDate = budget.StartDate;
            existing.EndDate = budget.EndDate;
            _repo.Save(_budgets);
        }
        else
        {
            // Add new budget
            budget.Id = _budgets.Any() ? _budgets.Max(b => b.Id) + 1 : 1;
            _budgets.Add(budget);
            _repo.Append(budget);
        }
    }

        public void DeleteBudget(int budgetId)
    {
        var budgetToRemove = _budgets.FirstOrDefault(b => b.Id == budgetId);
        
        if (budgetToRemove != null)
        {
            _budgets.Remove(budgetToRemove);
            _repo.Save(_budgets);
        }
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
        budget.TargetAmount > 0 ? budget.CurrentAmount / budget.TargetAmount : 0;
}
    