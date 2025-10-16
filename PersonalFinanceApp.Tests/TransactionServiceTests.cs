using NUnit.Framework;
using PersonalFinanceApp.Components.Entities;
using PersonalFinanceApp.Components.Models;
using PersonalFinanceApp.Components.Data;

namespace PersonalFinanceApp.Tests;

[TestFixture]
public class TransactionServiceTests
{
    [Test]
    public void GetBalance_CalculatesCorrectly()
    {
        // Arrange
        var transactionRepo = new TransactionRepo();
        var budgetRepo = new BudgetRepo();
        var budgetService = new BudgetService(budgetRepo);
        var transactionService = new TransactionService(transactionRepo, budgetService);

        // Create the necessary directories if they don't exist
        Directory.CreateDirectory("Components/Data");

        var income = new Income
        {
            Amount = 1000,
            Category = "Salary",
            Date = DateTime.Today
        };

        var expense = new Expense
        {
            Amount = 300,
            Category = "Food",
            Date = DateTime.Today
        };

        // Act
        transactionService.AddTransaction(income);
        transactionService.AddTransaction(expense);
        var balance = transactionService.GetBalance();

        // Assert
        Assert.That(balance, Is.EqualTo(700), "Balance should be 1000 - 300 = 700");
    }

    [TearDown]
    public void Cleanup()
    {
        // Clean up test data files after test runs
        if (File.Exists("Components/Data/transactions.txt"))
        {
            File.Delete("Components/Data/transactions.txt");
        }
        if (File.Exists("Components/Data/budgets.txt"))
        {
            File.Delete("Components/Data/budgets.txt");
        }
    }
}