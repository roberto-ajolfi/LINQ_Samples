using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace LINQ.ConsoleApp
{
    public class Product
    {
        public int ID { get; set; }

        public string ProductCode { get; set; }
    }

    public class ProductComparer : IEqualityComparer<Product>
    {
        public bool Equals([AllowNull] Product x, [AllowNull] Product y)
        { 
            return x?.ID == y?.ID;
        }

        public int GetHashCode([DisallowNull] Product obj)
        {
            return obj.ID.GetHashCode();
        }
    }
}
