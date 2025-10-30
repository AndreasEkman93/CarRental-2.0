
using CarRentalAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using CarRentalAPI.Controllers;
using CarRental.Data;

namespace CarRentalAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.

            builder.Services.AddIdentityCore<ApplicationUser>(options => { })
                .AddRoles<Microsoft.AspNetCore.Identity.IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddScoped<ICar, CarRepository>();

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddOpenApi();

            builder.Services.AddAuthentication(options => 
            { 
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtSwtings:Audience"],
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
                };
            });

            builder.Services.AddAuthorizationBuilder()
                .AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"))
                .AddPolicy("UserOnly", policy => policy.RequireRole("User"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapOpenApi();
            }

                        if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
