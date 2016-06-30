using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestMSdstress
{
    [TestClass]
    public class Class
    {
        [TestMethod]
        public void TestMethodFailing()
        {
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void Sucess222()
        {
            System.Diagnostics.Debug.WriteLine("Time {0}", DateTime.Now);
        }

        [TestMethod]
        public void FailIfWrong()
        {
#if NET452
            throw new Exception();
#endif
        }
    }
}
