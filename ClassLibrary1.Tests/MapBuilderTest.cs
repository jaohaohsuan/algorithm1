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

        [Test]
        public void BallAddingTest()
        {
            var a = new Ball("A");
            var b = new Ball("B");

            Assert.IsFalse(a.Add(a));
            Assert.IsTrue(a.Add(b));
            Assert.IsFalse(a.Add(b));
        }

        [Test]
        public void NeighborsTest()
        {
            var rules = new[]
                {
                    new Rule { One = "A", Two = "B" },
                    new Rule { One = "A", Two = "C" },
                };
            var rulesGraph = new RulesGraph(rules);



            Assert.IsTrue(new ValidNeighbors(rulesGraph, new LinkedList<string>(new[] { "A", "C" }).First).Result);
            Assert.IsTrue(new ValidNeighbors(rulesGraph, new LinkedList<string>(new[] { "D", "A" }).First).Result);

            Assert.IsFalse(new ValidNeighbors(rulesGraph, new LinkedList<string>(new[] { "C", "A" }).First).Result);
            Assert.IsFalse(new ValidNeighbors(rulesGraph, new LinkedList<string>(new[] { "A" }).First).Result);
            Assert.IsFalse(new ValidNeighbors(rulesGraph, new LinkedList<string>(new[] { "A", "D" }).First).Result);
        }
    }
}
