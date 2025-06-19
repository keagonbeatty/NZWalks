using Microsoft.EntityFrameworkCore;
using NZWalks.Data;

namespace NZWalks;


// Need to change all dbcontexts to the correct dbContexts based on whichever one im using

//To create Migrations for SQL Server:
//        Add-Migration "Initial migration" -Context NzWalksSqlServerDbContext -OutputDir Migrations/SqlServer
//        Update-Database -Context NzWalksSqlServerDbContext


//To create Migrations to MySQL Server:
//    Add-Migration "InitialMySqlMigration" -Context NZWalksDbContextMySQL -OutputDir Migrations/MySQL
//    Update-Database -Context NZWalksDbContextMySQL




public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        var connectionString = builder.Configuration.GetConnectionString("NZWalksConnectionStringMySQL");
        var connectionStringSQL = builder.Configuration.GetConnectionString("NZWalksConnectionStringMicroSQL");

        builder.Services.AddDbContext<NzWalksSqlServerDbContext>(options =>
    options.UseSqlServer(connectionStringSQL));

        //builder.Services.AddDbContext<NZWalksDbContextMySQL>(options =>
        //    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}