using PersonalFinanceApp.Components.Entities;

namespace PersonalFinanceApp.Components.Data;

// Repository for handling transaction data persistence using a text file
public class TransactionRepo : IRepository<ITransaction>
{
    private readonly string _filePath = "Components/Data/transactions.txt";

    // Loads all transactions from the file storage
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

                // Create appropriate transaction type (Income or Expense) based on the type field
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

    // Adds a single transaction to the end of the file
    public void Append(ITransaction transaction)
    {
        using (var writer = new StreamWriter(_filePath, append: true))
        {
            writer.WriteLine(
                $"{transaction.Id},{transaction.Date:o},{transaction.Amount},{transaction.Category},{transaction.Type}");
        }

        Console.WriteLine($"Appended transaction {transaction.Id}: {transaction.Category}");
    }

    // Overwrites the file with a complete list of transactions
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
    