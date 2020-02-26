using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GitVersion.Models
{
    public static class Stats
    {
        private static IDictionary<string, int> _stats = new Dictionary<string, int>();

        public static void Called(string msg, Status status)
        {
            Increment(msg);
            Increment(msg + "-" + status);
        }

        public static void Called(string msg)
        {
            Increment(msg);
        }

        public static void Dump(TextWriter writer)
        {
            var builder = new StringBuilder("\nCall statistics");
            builder.Append("\n====================================================================================================\n\n");

            foreach (var stat in _stats.OrderBy(s => s.Key))
            {
                builder.AppendFormat("{0,-80}{1:10}\n", stat.Key, stat.Value);
            }

            builder.Append("\n====================================================================================================\n\n");

            writer.Write(builder.ToString());
        }

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
