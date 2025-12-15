using MindLog.Application.Common.Models;
using MindLog.WebApp.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServices(builder.Configuration);

builder.Services.AddAuthorizationBuilder()
    .AddPolicy(ApplicationRoles.Admin, policy => policy.RequireRole("Admin"))
    .AddPolicy(ApplicationRoles.ManageBooks, policy => policy.RequireRole("Admin"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

await app.SeedDatabaseAsync();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
