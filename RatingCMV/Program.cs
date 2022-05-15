﻿

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CommunicationAppApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//using CommunicationAppApi.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RegistersContext>(options =>
//using CommunicationAppApi.Hubs;

    options.UseSqlServer(builder.Configuration.GetConnectionString("RegistersContext") ?? throw new InvalidOperationException("Connection string 'RegistersContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddSignalR();
builder.Services.AddDbContext<RatingsContext>();
builder.Services.AddDbContext<ContactsContext>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow All",
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});
    var app = builder.Build();



    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
    }
    app.UseCors("Allow All");

  

    app.UseAuthorization();
    app.UseStaticFiles();
    app.UseRouting();
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Registers}/{action=Index}/{id?}");

    app.Run();

