namespace PersonalFinanceApp.Components.Data;

public interface ITransaction
{
    int Id { get; set; }
    DateTime Date { get; set; }
    string Category { get; set; }
    decimal Amount { get; set; }
    string Type { get; }
}