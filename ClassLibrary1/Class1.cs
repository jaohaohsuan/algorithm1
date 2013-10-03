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
        public static IDictionary<string, Ball> Build(IEnumerable<Rule> rules)
        {
            var uniqStoreage = new ConcurrentDictionary<string, Ball>();
            foreach (var rule in rules)
            {
                Ball one, two;
                uniqStoreage.TryAdd(rule.One, new Ball(rule.One));
                one = uniqStoreage[rule.One];
                uniqStoreage.TryAdd(rule.Two, new Ball(rule.Two));
                two = uniqStoreage[rule.Two];

                one.Add(two);
            }

            return uniqStoreage;
        }
    }

    public class RulesGraph
    {
        private readonly IDictionary<string, Ball> _map;

        public RulesGraph(IEnumerable<Rule> rules)
        {
            _map = MapBuilder.Build(rules);
        }

        public Ball this[string key]
        {
            get { return _map[key]; }
        }
    }

    public class Ball
    {
        public Ball(string name)
        {
            Name = name;
            _balls = new Dictionary<Ball, Ball>();
        }

        public bool Add(Ball other)
        {
            var notEqual = !ReferenceEquals(other, this);

            if (notEqual && !_balls.ContainsKey(other))
            {
                _balls.Add(other, other);
                return true;
            }

            return false;
        }

        public IEnumerable<Ball> Balls
        {
            get { return _balls.Values; }
        }

        public string Name { get; private set; }

        private int? _level;
        private readonly Dictionary<Ball, Ball> _balls;

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

    public class ValidNeighbors
    {
        private readonly bool _result;

        public ValidNeighbors(RulesGraph rules, LinkedListNode<string> node)
        {
            try
            {
                var ball = rules[node.Value];

                if (!ball.Balls.Any())
                    _result = true;
                else
                    _result = ball.Balls.FirstOrDefault(o => o.Name == node.Next.Value) != null;
            }
            catch (KeyNotFoundException)
            {
                _result = true;
            }
            catch (NullReferenceException)
            {
                _result = true;
            }
            catch (Exception)
            {
                _result = false;
            }
        }

        public bool Result { get { return _result; } }
    }
}
