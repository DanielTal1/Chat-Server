﻿using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using server_try.Data;
using server_try.Hubs;
using server_try.Models;

var builder = WebApplication.CreateBuilder(args);

object p = builder.Services.AddDbContext<server_tryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("server_tryContext") ?? throw new InvalidOperationException("Connection string 'server_tryContext' not found.")));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddSingleton<MyHub>();
builder.Services.AddSingleton<PushNotifications>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });


    //options.AddPolicy("Allow All",
    //    builder =>
    //    {
    //        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    //    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//app.UseCors("Allow All");
app.UseCors();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<MyHub>("/myHub");
});


app.Run();
