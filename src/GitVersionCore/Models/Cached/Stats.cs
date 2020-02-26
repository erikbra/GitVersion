using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GitVersion.Models
{
    public static class Stats
    {
        private static IDictionary<string, int> _stats = new Dictionary<string, int>();

        public static void Called(string context, string method, object value)
        {
            var msg = context + "." + method;
            Increment(msg);
            Increment(msg + " :: " + value);
        }

        public static void Called(string context, string method)
        {
            var msg = context + "." + method;
            Increment(msg);
        }

        public static void Dump(TextWriter writer)
        {
            int buffer = 10;
            var maxLen = _stats.Keys.Max(k => k.Length);

            var builder = new StringBuilder("\nCall statistics");

            builder.Append("\n");
            builder.Append('=', maxLen + buffer + buffer);
            builder.Append("\n");
            builder.Append("\n");

            foreach (var stat in _stats.OrderBy(s => s.Key))
            {
                var name = stat.Key;
                var padded = name.PadRight(maxLen + buffer);

                var paddedNumber = stat.Value.ToString().PadLeft(7);

                builder.Append($"{padded}{paddedNumber}\n");
            }

            builder.Append("\n");
            builder.Append('=', maxLen + buffer + buffer);
            builder.Append("\n");
            builder.Append("\n");

            writer.Write(builder.ToString());
        }

        // private static (IDictionary<string,IList<string>>, IDictionary<string, IList<string>>) GroupStats()
        // {
        //     var tokens = _stats.Keys.Select(k => k.Split('.'));
        //
        //     var lvl1 = tokens.Select(t => t[0]);
        //     var lvl2 = tokens.Select(t => (t[0], t[1]));
        //     var lvl3 = tokens.Select(t => t.Length > 2 ? (t[0], t[1], t[2]) : (t[0], t[1], null));
        //
        //     var innerJoinQuery =
        //         from l1 in lvl1
        //         join l2 in lvl2 on l2.Item1 equals l1.
        //             sele
        //         //select new { ProductName = prod.Name, Category = category.Name };
        //
        //
        //     return (null, null);
        // }

        private static void Increment(string msg)
        {
            if (_stats.ContainsKey(msg))
            {
                _stats[msg] += 1;
            }
            else
            {
                _stats[msg] = 1;
            }
        }
    }
}
