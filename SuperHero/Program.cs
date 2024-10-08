﻿
namespace SuperHero240327
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Scaffold-DbContext "Data Source=localhost;Database=SuperHero;User ID=Hero;Password=Hero@20240327;TrustServerCertificate=true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -NoOnConfiguring -UseDatabaseNames -NoPluralize -Force

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //builder.Services.AddDbContext<SuperHeroContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

            var app = builder.Build();

            // 跨域资源共享
            app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

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
}
