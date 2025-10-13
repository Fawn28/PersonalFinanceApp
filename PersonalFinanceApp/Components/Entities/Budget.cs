namespace BlazorApp1.Components.Entities;

public class Budget
{
    public int Id { get; set; }
    public string Name { get; set; }      
    public decimal TargetAmount { get; set; }
    public decimal CurrentAmount { get; set; } = 0;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
