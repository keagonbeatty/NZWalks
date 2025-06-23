using System.Text;
using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Mappings;
using NZWalks.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Experimental;

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
        var connectionStringAuth = builder.Configuration.GetConnectionString("NZWalksAuthConnectionString");
        //var connectionStringSQL = builder.Configuration.GetConnectionString("NZWalksConnectionStringMicroSQL");

    //     builder.Services.AddDbContext<NzWalksSqlServerDbContext>(options =>
    // options.UseSqlServer(connectionStringSQL));

    builder.Services.AddScoped<IRegionRepository, MySqlRegionRepository>();
    builder.Services.AddScoped<IWalkRepository, MySQLWalkRepository>();
    builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

    builder.Services.AddIdentityCore<IdentityUser>()
        .AddRoles<IdentityRole>()
        .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks")
        .AddEntityFrameworkStores<NZWalksAuthDbContext>().AddDefaultTokenProviders();

    builder.Services.Configure<IdentityOptions>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 1;
    });
    
    
    builder.Services.AddDbContext<NZWalksDbContextMySQL>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
    
    builder.Services.AddDbContext<NZWalksAuthDbContext>(options =>
        options.UseMySql(connectionStringAuth, ServerVersion.AutoDetect(connectionStringAuth)));

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer((options) =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                }
            );

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}