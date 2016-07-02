using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace dstress
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("DSTRESS");

            var builder = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddEnvironmentVariables("DSTRESS_")

                .AddJsonFile("config.json", optional: true);

            //.AddEnvironmentVariables()

            builder.AddUserSecrets("aspnet-dstress-20160702094745");

            var config = builder.Build();

            Console.WriteLine(config["hello"]);
            Console.WriteLine(config["hello2"]);

            Console.ReadKey();

            CheckDataDriver(new InMemoryDataDriver());
        }

        static void CheckDataDriver(IDataDriver driver)
        {
            string id = driver.Create(new { name = "Fabricio" });
            object original = driver.Read(id);

            bool wasUpdated = driver.Update(id, new { height = 10, width = 10 });

            object modified = driver.Read(id);

            bool wasDeleted = driver.Delete(id);
        }
    }
}
