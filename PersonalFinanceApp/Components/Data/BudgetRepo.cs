using PersonalFinanceApp.Components.Entities;

namespace PersonalFinanceApp.Components.Data;

public class BudgetRepo : IRepository<Budget>
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
            var fields = line.Split(',');
            budgets.Add(new Budget
            {
                Id = int.Parse(fields[0]),
                Name = fields[1],
                TargetAmount = decimal.Parse(fields[2]),
                CurrentAmount = decimal.Parse(fields[3]),
                StartDate = DateTime.Parse(fields[4]),
                EndDate = DateTime.Parse(fields[5])
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