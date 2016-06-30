using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dstress
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("DSTRESS");

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
