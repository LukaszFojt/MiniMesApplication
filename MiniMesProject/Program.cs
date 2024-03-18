using Microsoft.EntityFrameworkCore;

/**
 * Klasa g³ówna programu MiniMesProject.
 * Zawiera metodê Main, która jest punktem wejœcia aplikacji.
 */
namespace MiniMesProject
{
    public class Program
    {
        /**
         * G³ówna metoda programu, która jest punktem wejœcia aplikacji.
         * @param args Argumenty wiersza poleceñ przekazane do aplikacji.
         */
        public static void Main(string[] args)
        {
            // Tworzenie obiektu WebApplicationBuilder
            var builder = WebApplication.CreateBuilder(args);

            // Dodawanie us³ug do kontenera DI (Dependency Injection).
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<MiniMesDbContext>(o => 
            o.UseSqlServer(@"Data Source=364-LAP-2020\SQLEXPRESS;Initial Catalog=MiniMesDB;Integrated Security=SSPI;TrustServerCertificate=True;",
            o => o.MigrationsAssembly("MiniMesProject")));

            // Rejestracja interfejsów repozytoriów z odpowiadaj¹cymi im implementacjami.
            builder.Services.AddScoped<IMachineRepository, MachineRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IProcessesRepository, ProcessesRepository>();
            builder.Services.AddScoped<IParametersRepository, ParametersRepository>();
            builder.Services.AddScoped<IProcessParametersRepository, ProcessParametersRepository>();
            builder.Services.AddScoped<IMachineParameterRepository, MachineParameterRepository>();

            // Budowanie aplikacji na podstawie konfiguracji.
            var app = builder.Build();

            // Konfiguracja CORS (Cross-Origin Resource Sharing).
            app.UseCors(options =>
            {
                options.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });

            // Konfiguracja potoku ¿¹dañ HTTP.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Konfiguracja autoryzacji.
            app.UseAuthorization();

            // Mapowanie kontrolerów do endpointów HTTP.
            app.MapControllers();

            // Uruchomienie aplikacji.
            app.Run();
        }
    }
}
