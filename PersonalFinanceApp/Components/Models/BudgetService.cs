using PersonalFinanceApp.Components.Data;
using PersonalFinanceApp.Components.Entities;

namespace PersonalFinanceApp.Components.Models;

public class BudgetService
{
    private readonly IRepository<Budget> _repo;
    private readonly List<Budget> _budgets;

    public BudgetService(IRepository<Budget> repo)
    {
        _repo = repo;
        _budgets = _repo.Load();
    }

    public IEnumerable<Budget> GetAll() => _budgets;
    
    public void AddBudget(Budget budget)
    {
        // Check if budget already exists 
        var existingBudget = _budgets.FirstOrDefault(b => b.Id == budget.Id);
        
        if (existingBudget != null)
        {
            // Update existing budget
            existingBudget.Name = budget.Name;
            existingBudget.TargetAmount = budget.TargetAmount;
            existingBudget.CurrentAmount = budget.CurrentAmount;
            existingBudget.StartDate = budget.StartDate;
            existingBudget.EndDate = budget.EndDate;
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
    
    public void UpdateProgress(ITransaction transaction)
    {
        foreach (var budget in _budgets)
        {
            if (transaction.Date >= budget.StartDate && transaction.Date <= budget.EndDate)
            {
                if (transaction.Type == "Expense")
                {
                    budget.CurrentAmount -= transaction.Amount;
                }
                else if (transaction.Type == "Income")
                {
                    budget.CurrentAmount += transaction.Amount;
                }
            }
        }

        _repo.Save(_budgets);
    }
    
}
    