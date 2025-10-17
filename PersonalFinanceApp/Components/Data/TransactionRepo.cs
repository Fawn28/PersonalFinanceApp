using PersonalFinanceApp.Components.Entities;

namespace PersonalFinanceApp.Components.Data;

public class TransactionRepo
{
    private readonly string _filePath = "Components/Data/transactions.txt";

    public List<Transaction> Load()
    {
        var transactions = new List<Transaction>();

        if (!File.Exists(_filePath))
            return transactions;

        var lines = File.ReadAllLines(_filePath);

        foreach (var line in lines)
        {
            var parts = line.Split(',');

            if (parts.Length < 5)
                continue;

            try
            {
                int id = int.Parse(parts[0]);
                DateTime date = DateTime.Parse(parts[1]);
                decimal amount = decimal.Parse(parts[2]);
                string category = parts[3];
                string type = parts[4];

                Transaction transaction = type.Equals("income", StringComparison.OrdinalIgnoreCase)
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
    