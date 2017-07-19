using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TestLibrary
{
    [TestFixture]
    public class Class1
    {

        [Test]
        public void TestFirstTest()
        {
            Assert.AreEqual(true, false);
        }

        [Test]
        public void TestSecondTest()
        {
            Assert.AreEqual(true, true);
        }
    }
}
