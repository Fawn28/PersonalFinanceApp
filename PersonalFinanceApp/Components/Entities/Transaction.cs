namespace BlazorApp1.Components.Entities;

public abstract class Transaction
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public abstract string Type { get; }
}