using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary1
{
    public class Rule
    {
        public string One { get; set; }
        public string Two { get; set; }
    }

    public class MapBuilder
    {
        public static  IDictionary<string,Ball> Build(IEnumerable<Rule> rules)
        {
            var uniqStoreage = new ConcurrentDictionary<string, Ball>();
            foreach (var rule in rules)
            {
                Ball one, two;
                uniqStoreage.TryAdd(rule.One, new Ball(rule.One));
                one = uniqStoreage[rule.One];
                uniqStoreage.TryAdd(rule.Two, new Ball(rule.Two));
                two = uniqStoreage[rule.Two];

                one.Balls.Add(two);
            }

            return uniqStoreage;
        }
    }

    public class Ball
    {
        public Ball(string name)
        {
            Balls = new List<Ball>();
        }

        public List<Ball> Balls { get; set; }
        public string Name { get; private set; }

        private int? _level;
        public int Level
        {
            get
            {
                if (_level == null)
                {
                    if (Balls.Any())
                        _level = Balls.Max(o => o.Level) + 1;
                    else
                        _level = 0;

                }
                return _level.Value;
            }
        }
    }
}
