//using Microsoft.Data.Analysis;
//namespace PersonalFinanceApp.Components.Models;

//public class AnalyticsService
//{
//    private DataFrame _dataFrame;

//    public AnalyticsService(DataFrame df)
//    {
//        _dataFrame = df;
//    }
    
//    public decimal GetTotalExpenses()
//    {
//        var expenseRows = _dataFrame.Filter(_dataFrame["Type"].ElementwiseEquals("Expense"));
//        return Convert.ToDecimal(expenseRows["Amount"].Sum());
//    }
    
//    public decimal GetTotalIncome()
//    {
//        var incomeRows = _dataFrame.Filter(_dataFrame["Type"].ElementwiseEquals("Income"));
//        return Convert.ToDecimal(incomeRows["Amount"].Sum());
//    }
    
//    public List<CategorySummary> GetCategorySummary()
//    {
//        var categoryColumn = _dataFrame.Columns["Category"];
//        var amountColumn = _dataFrame.Columns["Amount"];

//        var result = new List<CategorySummary>();

//        var categories = categoryColumn.Cast<string>().Distinct().ToList();

//        foreach (var category in categories)
//        {
//            var rows = _dataFrame.Filter(categoryColumn.ElementwiseEquals(category));
//            decimal total = Convert.ToDecimal(rows["Amount"].Sum());
//            result.Add(new CategorySummary { Category = category.ToString(), Total = total });
//        }

//        return result;
//    }
//}

//public class CategorySummary
//{
//    public string Category { get; set; }
//    public decimal Total { get; set; }
//}
