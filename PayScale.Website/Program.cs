using Microsoft.Extensions.Configuration;
using PayScale.Website.Clients;
using PayScale.Website.Clients.IClientServices;
using PayScale.Website.Extensions;
using PayScale.Website.Models;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();
builder.Services.Configure<ClientOptions>(builder.Configuration.GetSection(nameof(ClientOptions.PayScaleAPI)));

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}
app.UseExceptionHandler("/Home/Error");
app.UseHsts();
app.UseStatusCodePages(Text.Plain, "Ooopps!! Something went wrong with your input, please try again. Error code: {0}");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
