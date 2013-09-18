using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ClassLibrary1.Tests
{
    [TestFixture]
    public class MapBuilderTest
    {
        [Test]
        public void LevelTest()
        {
            var rules = new[]
                {
                    new Rule { One = "A", Two = "B" },
                    new Rule { One = "A", Two = "C" },
                };
            var map = MapBuilder.Build(rules);

            Assert.AreEqual(map["A"].Level, 1);
        }
    }
}
