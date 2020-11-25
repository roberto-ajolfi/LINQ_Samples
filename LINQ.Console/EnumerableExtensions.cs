using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace LINQ.ConsoleApp
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Where<T>(this IEnumerable<T> data, string propertyName, string propertyValue)
        {
            // crea oggetto ritornato
            var results = new List<T>();

            // quale è il tipo contenuto nell'IEnumerable?
            var dataType = typeof(T);

            // definire il parametro della lambda
            var parameter = Expression.Parameter(dataType, "p");

            // quale proprietà di T devo considerare?
            var propertyReference = Expression.Property(parameter, propertyName);

            // converto il tipo di propertyValue a quello necessario per propertyName
            var changedObj = Convert.ChangeType(propertyValue, propertyReference.Type);

            // il valore da ricercare come costante
            var propertyValueAsExpression = Expression.Constant(changedObj);

            // costruisco la lambda (con una espressione di uguaglianza come body) e la compilo
            var condizione = Expression.Lambda<Func<T, bool>>(
                Expression.Equal(propertyReference, propertyValueAsExpression),
                new[] { parameter }
            ).Compile();

            // eseguo la lambda generata pe rogni elemento dell'IEnumerable
            foreach (T value in data)
                if (condizione(value))
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
