using Amzaon_DataWarehouse_BackEnd.IRepositories;
using Amzaon_DataWarehouse_BackEnd.Models;
using Amzaon_DataWarehouse_BackEnd.Repositories;
using Amzaon_DataWarehouse_BackEnd.Services;
using Amzaon_DataWarehouse_BackEnd.Services.ServiceImpl;
using DataWarehouse.IRepositories;
using DataWarehouse.Repositories;
using ExecutionTime;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllersWithViews().AddNewtonsoftJson();



builder.Services.AddDbContext<DataWarehouseContext>(options =>
                    options.UseMySql(builder.Configuration["AmazonDataWarehouse:MySQLConnectionString"],
                    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.27-mysql")));

// add Repository DI.
builder.Services.AddScoped<IMovieRepository,MovieRespository>();
builder.Services.AddScoped<IDirectorRepository, DirectorRepository>();
builder.Services.AddScoped<IActorRepository, ActorRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();



//Cors

string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(op => {
    op.AddPolicy(MyAllowSpecificOrigins, set => {
        set.SetIsOriginAllowed(origin => true)
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

//执行时间中间件
app.UseCalculateExecutionTime();//只需在此添加


app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

