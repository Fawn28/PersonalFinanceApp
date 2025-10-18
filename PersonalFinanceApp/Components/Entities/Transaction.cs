using PersonalFinanceApp.Components.Data;

namespace PersonalFinanceApp.Components.Entities;

// Base class for all financial transactions in our app
// This abstract class provides common properties shared by all transaction types
public abstract class Transaction : ITransaction
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Category { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public abstract string Type { get; }
}