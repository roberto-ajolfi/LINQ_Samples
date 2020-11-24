using System;
using System.Collections.Generic;
using System.Text;

namespace LINQ.ConsoleApp
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Where<T>(this IEnumerable<T> data, string propertyName, string propertyValue)
        {
            // crea oggetto ritornato
            var results = new List<T>();

            // eseguo la lambda generata pe rogni elemento dell'IEnumerable
            foreach (T value in data)
                //if (condizione(value))
                results.Add(value);

            // ritorno il risultato
            return results as IEnumerable<T>;
        }

        public static List<int> Where(this List<int> data, Func<int, bool> condizione)
        {
            var results = new List<int>();

            foreach (int value in data)
                if (condizione(value))
                    results.Add(value);

            return results;
        }
    }
}
