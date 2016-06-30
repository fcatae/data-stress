using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Test_DataDriver
{
    public class Class1
    {
        [Fact]
        public void Teste1()
        {
            System.Diagnostics.Debug.WriteLine("Hello world");
        }

        [Fact]
        public void Somar()
        {
            Assert.Equal( 1 + 1 , 2 );
        }

        [Fact]
        void Teste2()
        {
        }

    }
}
