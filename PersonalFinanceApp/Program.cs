using PersonalFinanceApp.Components.Data;
using PersonalFinanceApp.Components.Models;
using PersonalFinanceApp.Components.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<IRepository<ITransaction>, TransactionRepo>();
builder.Services.AddScoped<IRepository<Budget>, BudgetRepo>();

builder.Services.AddScoped<TransactionService>();
builder.Services.AddScoped<BudgetService>();

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
