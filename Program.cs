using AgenciaTurismo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using AgenciaTurismo.Models;

namespace AgenciaTurismo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login";
                    options.AccessDeniedPath = "/AccessDenied"; 
                });
            builder.Services.AddDbContext<AgenciaTurismoContext>(options =>
                options.UseSqlite(connectionString));
            // Add services to the container.
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<AgenciaTurismoContext>();

                context.Database.EnsureCreated();

                // Verifica se a tabela de pa�ses est� vazia
                if (!context.PaisesDestino.Any())
                {
                    // Se estiver vazia, adiciona os dados iniciais
                    context.PaisesDestino.AddRange(
                        new PaisDestino { Nome = "Brasil" },
                        new PaisDestino { Nome = "Portugal" },
                        new PaisDestino { Nome = "It�lia" },
                        new PaisDestino { Nome = "Fran�a" },
                        new PaisDestino { Nome = "Jap�o" }
                    );
                    // Salva os dados no banco
                    context.SaveChanges();
                }
            }
            app.Run();
        }
    }
}
