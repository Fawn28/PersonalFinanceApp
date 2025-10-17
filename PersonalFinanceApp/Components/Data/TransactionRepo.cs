using PersonalFinanceApp.Components.Entities;

namespace PersonalFinanceApp.Components.Data;

public class TransactionRepo : IRepository<ITransaction>
{
    private readonly string _filePath = "Components/Data/transactions.txt";

    public List<ITransaction> Load()
    {
        var transactions = new List<ITransaction>();

        if (!File.Exists(_filePath))
            return transactions;

        var lines = File.ReadAllLines(_filePath);

        foreach (var line in lines)
        {
            var fields = line.Split(',');

            if (fields.Length < 5)
                continue;

            try
            {
                int id = int.Parse(fields[0]);
                DateTime date = DateTime.Parse(fields[1]);
                decimal amount = decimal.Parse(fields[2]);
                string category = fields[3];
                string type = fields[4];

                ITransaction transaction = type.Equals("income", StringComparison.OrdinalIgnoreCase)
                    ? new Income { Id = id, Date = date, Amount = amount, Category = category }
                    : new Expense { Id = id, Date = date, Amount = amount, Category = category };

                transactions.Add(transaction);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to parse line: {line}\n{ex.Message}");
            }
        }

        Console.WriteLine($"Loaded {transactions.Count} transactions from {_filePath}");
        return transactions;
    }

    public void Append(ITransaction transaction)
    {
        using (var writer = new StreamWriter(_filePath, append: true))
        {
            writer.WriteLine(
                $"{transaction.Id},{transaction.Date:o},{transaction.Amount},{transaction.Category},{transaction.Type}");
        }

        Console.WriteLine($"Appended transaction {transaction.Id}: {transaction.Category}");
    }

    public void Save(List<ITransaction> transactions)
    {
        using (var writer = new StreamWriter(_filePath, false))
        {
            foreach (var transaction in transactions)
            {
                writer.WriteLine(
                    $"{transaction.Id},{transaction.Date:o},{transaction.Amount},{transaction.Category},{transaction.Type}");
            }
        }

        Console.WriteLine($"Saved {transactions.Count} transactions to {_filePath}");
    }
}
    