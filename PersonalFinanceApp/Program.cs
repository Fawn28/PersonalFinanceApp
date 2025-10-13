using PersonalFinanceApp.Components.Data;
using PersonalFinanceApp.Components.Models;
using PersonalFinanceApp.Components.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<TransactionRepo>();
builder.Services.AddSingleton<TransactionService>();

builder.Services.AddSingleton<BudgetRepo>();
builder.Services.AddSingleton<BudgetService>();

//builder.Services.AddSingleton<AnalyticsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<PersonalFinanceApp.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();
