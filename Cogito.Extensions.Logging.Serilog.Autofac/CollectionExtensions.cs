using System;
using System.Collections.Generic;
using System.Linq;

namespace Cogito.Extensions.Logging.Serilog.Autofac
{

    static class CollectionExtensions
    {

        /// <summary>
        /// Removes all of the items from the collection that match the specified condition.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="predicate"></param>
        public static void RemoveWhere<T>(this ICollection<T> self, Func<T, bool> predicate)
        {
            if (self == null)
                throw new ArgumentNullException(nameof(self));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            foreach (var i in self.Where(predicate).ToList())
                self.Remove(i);
        }

    }

}
