using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using dstress;

namespace test.dstress
{
    public class DataDriver
    {
        IDataDriver driver = new InMemoryDataDriver();
        readonly object MODEL = new { name = "Fabricio" };

        [Fact]
        public void Create()
        {
            string id = driver.Create(MODEL);

            Assert.NotNull(id);
        }

        [Fact]
        public void Workflow()
        {
            string id = driver.Create(MODEL);
            object original = driver.Read(id);
            Assert.Equal(original, MODEL); 

            bool wasUpdated = driver.Update(id, new { height = 10, width = 10 });
            Assert.True(wasUpdated);

            object modified = driver.Read(id);
            Assert.NotEqual(modified, MODEL);

            bool wasDeleted = driver.Delete(id);
            Assert.True(wasDeleted);
        }
    }
}
