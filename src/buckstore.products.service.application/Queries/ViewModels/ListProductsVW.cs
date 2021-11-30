using System;
using System.Collections;
using System.Collections.Generic;

namespace buckstore.products.service.application.Queries.ViewModels
{
    public class ListProductsVW
    {
        public Guid Id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string description { get; set; }
        public int stock_quantity { get; set; }
        public int categoryId { get; set; }
        public string category { get; set; }
        public double number_of_products { get; set; }
        public List<string> imagesUrl { get; set; }
    }
}
