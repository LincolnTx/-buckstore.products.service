using System;
using System.Collections.Generic;
using System.Linq;
using buckstore.products.service.domain.SeedWork;

namespace buckstore.products.service.domain.Aggregates.ProductAggregate
{
    public class ProductCategory : Enumeration
    {
        public static ProductCategory Gamer = new ProductCategory(1, nameof(Gamer));
        public static ProductCategory SmartPhones = new ProductCategory(2, nameof(SmartPhones));
        public static ProductCategory Pc = new ProductCategory(3, "Computador");
        public static ProductCategory Gadgets = new ProductCategory(4, "Periféricos");
        public static ProductCategory Hardware = new ProductCategory(5, nameof(Hardware));
        public static ProductCategory Office = new ProductCategory(6, nameof(Office));

        public ProductCategory(int id, string name) : base(id, name)
        {
        }
       
        public static IEnumerable<ProductCategory> List() =>
            new[] { Gamer, SmartPhones, Pc, Gadgets };
       
        public static ProductCategory FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new Exception($"Possible values for ProductCategory: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static ProductCategory FromId(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new Exception($"Possible values for ProductCategory: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}