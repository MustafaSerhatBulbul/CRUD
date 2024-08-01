using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;

namespace WebApplication3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices((context, services) =>
                    {
                        services.AddDbContext<FakeData>(options =>
                            options.UseInMemoryDatabase("InMemoryDb"));
                        services.AddControllersWithViews();
                    });

                    webBuilder.Configure((appContext, app) =>
                    {
                        var env = appContext.HostingEnvironment;

                        if (env.IsDevelopment())
                        {
                            app.UseDeveloperExceptionPage();
                        }
                        else
                        {
                            app.UseExceptionHandler("/Home/Error");
                            app.UseHsts();
                        }

                        app.UseHttpsRedirection();
                        app.UseStaticFiles();

                        app.UseRouting();

                        app.UseAuthorization();

                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllerRoute(
                                name: "default",
                                pattern: "{controller=MusteriIslemleri}/{action=Index}/{id?}");
                        });

                        // Veritabanýný baþlangýç verisi ile doldur
                        using (var scope = app.ApplicationServices.CreateScope())
                        {
                            var context = scope.ServiceProvider.GetRequiredService<FakeData>();
                            if (!context.Musteriler.Any())
                            {
                                context.Musteriler.AddRange(
                                    new Models.Musteri { Adi = "Ahmet", Soyadi = "Yýlmaz", Adresi = "Ankara" },
                                    new Models.Musteri { Adi = "Ayþe", Soyadi = "Kara", Adresi = "Ýstanbul" }
                                );
                                context.SaveChanges();
                            }
                        }
                    });
                });
        }
    }
}













