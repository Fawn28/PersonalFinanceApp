using BlazorApp1.Components.Entities;

namespace BlazorApp1.Components.Data;

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
                string description = parts[3];
                string type = parts[4];

                Transaction transaction = type.Equals("income", StringComparison.OrdinalIgnoreCase)
                    ? new Income { Id = id, Date = date, Amount = amount, Description = description }
                    : new Expense { Id = id, Date = date, Amount = amount, Description = description };

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

    public void Append(Transaction transaction)
    {
        using (var writer = new StreamWriter(_filePath, append: true))
        {
            // if (new FileInfo(_filePath).Length > 0)
            // {
            //     writer.WriteLine();
            // }

            writer.WriteLine($"{transaction.Id},{transaction.Date:o},{transaction.Amount},{transaction.Description},{transaction.Type}");
        }

        Console.WriteLine($"Appended transaction {transaction.Id}: {transaction.Description}");
    }

    // private readonly string _filePath = "Components/Data/transactions.json";
    // private readonly JsonSerializerOptions _options;
    //
    // public TransactionRepo()
    // {
    //     _options = new JsonSerializerOptions
    //     {
    //         WriteIndented = true,
    //         Converters = { new TransactionJsonConverter() } 
    //     };
    // }
    //
    // public List<Transaction> Load()
    // {
    //     try
    //     {
    //         Console.WriteLine($"[DEBUG] Loading transactions from: {_filePath}");
    //
    //         if (!File.Exists(_filePath))
    //         {
    //             Console.WriteLine("[DEBUG] File does not exist. Returning empty list.");
    //             return new List<Transaction>();
    //         }
    //
    //         var json = File.ReadAllText(_filePath);
    //         Console.WriteLine($"[DEBUG] File content:\n{json}");
    //
    //         var list = JsonSerializer.Deserialize<List<Transaction>>(json, _options)
    //                    ?? new List<Transaction>();
    //
    //         Console.WriteLine($"[DEBUG] Loaded {list.Count} transactions.");
    //         return list;
    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine($"[ERROR] Failed to load transactions: {ex.Message}");
    //         return new List<Transaction>();
    //     }
    // }
    // public void Append(Transaction transaction)
    // {
    //     var json = JsonSerializer.Serialize(transaction, _options);
    //     File.AppendAllText(_filePath, json + Environment.NewLine);
    //     Console.WriteLine($"[DEBUG] Appended {transaction.Description}");
    // }
    // public void Save(List<Transaction> transactions)
    // {
    //     try
    //     {
    //         var json = JsonSerializer.Serialize(transactions, _options);
    //         File.WriteAllText(_filePath, json);
    //         Console.WriteLine($"[DEBUG] Saved {transactions.Count} transactions to: {_filePath}");
    //         Console.WriteLine($"[DEBUG] File content:\n{json}");
    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine($"[ERROR] Failed to save transactions: {ex.Message}");
    //     }
    // }
}