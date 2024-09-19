// <copyright file="Program.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

using Azure.Monitor.OpenTelemetry.AspNetCore;
using CleanArchCQRSMediatorAPI.API.Exceptions;
using CleanArchCQRSMediatorAPI.API.Extensions;
using CleanArchCQRSMediatorAPI.Application;
using CleanArchCQRSMediatorAPI.Identity;
using CleanArchCQRSMediatorAPI.Identity.Extensions;
using CleanArchCQRSMediatorAPI.Persistence;
using CleanArchCQRSMediatorAPI.Persistence.Extensions;
using CleanArchCQRSMediatorAPI.Utility;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer",
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",
                },
            },
            new string[] { }
        },
    });
});
builder.Services.AddSingleton<IExceptionHandler, GlobalExceptionHander>();
builder.Services.ConfigurePersistenceService(configuration);
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureUtilityServices(configuration);
// builder.Services.RegisterIdentityService(configuration);
builder.Host.RegisterSerilogLogging(configuration);

builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddOpenTelemetry().UseAzureMonitor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

// app.MigrateAuthDbContext();
// app.MigrateLibraryDbContext();

// app.UseAuthentication();
// app.UseAuthorization();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseExceptionHandler("/error");
app.MapEndpoint();

app.Run();