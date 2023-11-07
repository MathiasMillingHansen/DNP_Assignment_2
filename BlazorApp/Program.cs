using BlazorApp.Authentication;
using BlazorApp.Services;
using BlazorApp.Services.Impl;
using Clients.ClientInterfaces;
using Clients.Implementations;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<IUserService, UserHttpClient>();
builder.Services.AddScoped<IRedditPostService, RedditPostHttpClient>();
builder.Services.AddScoped<AuthenticationStateProvider, SimpleAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthManager, AuthManagerImpl>();
builder.Services.AddScoped<BlazorIUserService, BlazorIUserServiceImpl>();
builder.Services.AddScoped(
    sp =>
        new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7188")
        }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();