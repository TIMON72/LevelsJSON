using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace LevelsJSON
{
    /// <summary>
    /// Главный класс программы
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Главный метод программы
        /// </summary>
        /// <param name="args">Входные аргументы</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        /// <summary>
        /// Интерфейс создания хоста
        /// </summary>
        /// <param name="args">Входные аргументы</param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
