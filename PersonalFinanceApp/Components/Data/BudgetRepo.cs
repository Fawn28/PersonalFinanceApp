using BlazorApp1.Components.Entities;

namespace BlazorApp1.Components.Data;

public class BudgetRepo
{
    private readonly string _filePath = "Components/Data/budgets.txt";

    public List<Budget> Load()
    {
        if (!File.Exists(_filePath))
            return new List<Budget>();

        var lines = File.ReadAllLines(_filePath);
        var budgets = new List<Budget>();

        foreach (var line in lines)
        {
            var parts = line.Split(',');
            budgets.Add(new Budget
            {
                Id = int.Parse(parts[0]),
                Name = parts[1],
                TargetAmount = decimal.Parse(parts[2]),
                CurrentAmount = decimal.Parse(parts[3]),
                StartDate = DateTime.Parse(parts[4]),
                EndDate = DateTime.Parse(parts[5])
            });
        }

        return budgets;
    }

    public void Save(List<Budget> budgets)
    {
        using var writer = new StreamWriter(_filePath, false);
        foreach (var budget in budgets)
        {
            writer.WriteLine($"{budget.Id},{budget.Name},{budget.TargetAmount},{budget.CurrentAmount},{budget.StartDate:o},{budget.EndDate:o}");
        }
    }

    public void Append(Budget budget)
    {
        using var writer = new StreamWriter(_filePath, true);
        writer.WriteLine($"{budget.Id},{budget.Name},{budget.TargetAmount},{budget.CurrentAmount},{budget.StartDate:o},{budget.EndDate:o}");
    }
}