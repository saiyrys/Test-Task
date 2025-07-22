using TestTask.Application.Interface.DapperInterface;
using TestTask.Application.Interface.EmployeeInterface;
using TestTask.Application.Services.Employee;
using TestTask.Domain.IRepository.Company;
using TestTask.Domain.IRepository.Employee;
using TestTask.Infrastructure.Data;
using TestTask.Infrastructure.Repositories.Company;
using TestTask.Infrastructure.Repositories.Employee;

namespace TestTask.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IDapperDbConnection, DapperDbConnection>();
            builder.Services.AddScoped<IEmployeeService, EmployeesService>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
}
