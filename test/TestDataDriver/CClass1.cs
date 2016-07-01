using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Verification
{
    public class Ghost
    {
        private readonly ITestOutputHelper output;

        public Ghost(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void CC()
        {
            output.WriteLine("Hello world");
            System.Diagnostics.Debug.WriteLine("Time {0}", DateTime.Now);
        }

        [Fact]
        public void AA()
        {
            Assert.Equal( 1 + 1 , 2 );

            var b = new dstress.InMemoryDataDriver();
        }

        [Fact]
        void BB()
        {            
        }

    }
}
