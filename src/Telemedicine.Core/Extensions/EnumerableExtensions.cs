using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telemedicine.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<TArg>(this IEnumerable<TArg> input, Action<TArg> itemAction)
        {
            foreach (var item in input)
            {
                itemAction(item);
            }
        }
    }
}
