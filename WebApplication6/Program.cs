using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApplication6.Data;
using WebApplication6.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WebApplication6Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebApplication6Context") ?? throw new InvalidOperationException("Connection string 'WebApplication6Context' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
    .AddCookie(options =>
    {
        options.LoginPath = "/Users/Login/";
        options.AccessDeniedPath = "/Users/AccessDenied/";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=users}/{action=Register}/{id?}");
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<MyHub>("/myhub");
});

app.Run();
