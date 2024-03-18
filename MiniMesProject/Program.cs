using Microsoft.EntityFrameworkCore;

/**
 * Klasa g��wna programu MiniMesProject.
 * Zawiera metod� Main, kt�ra jest punktem wej�cia aplikacji.
 */
namespace MiniMesProject
{
    public class Program
    {
        /**
         * G��wna metoda programu, kt�ra jest punktem wej�cia aplikacji.
         * @param args Argumenty wiersza polece� przekazane do aplikacji.
         */
        public static void Main(string[] args)
        {
            // Tworzenie obiektu WebApplicationBuilder
            var builder = WebApplication.CreateBuilder(args);

            // Dodawanie us�ug do kontenera DI (Dependency Injection).
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<MiniMesDbContext>(o => 
            o.UseSqlServer(@"Data Source=364-LAP-2020\SQLEXPRESS;Initial Catalog=MiniMesDB;Integrated Security=SSPI;TrustServerCertificate=True;",
            o => o.MigrationsAssembly("MiniMesProject")));

            // Rejestracja interfejs�w repozytori�w z odpowiadaj�cymi im implementacjami.
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

            // Konfiguracja potoku ��da� HTTP.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Konfiguracja autoryzacji.
            app.UseAuthorization();

            // Mapowanie kontroler�w do endpoint�w HTTP.
            app.MapControllers();

            // Uruchomienie aplikacji.
            app.Run();
        }
    }
}
