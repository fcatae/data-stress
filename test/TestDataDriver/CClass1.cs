using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Verification
{
    public class Ghost
    {
        [Fact]
        public void C()
        {
            System.Diagnostics.Debug.WriteLine("Hello world");
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

#if NET452
            throw new Exception();
#endif

        }

    }
}
