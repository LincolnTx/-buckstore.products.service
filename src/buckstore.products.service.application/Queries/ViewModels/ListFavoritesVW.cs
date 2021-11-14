using System;
using System.Collections.Generic;

namespace buckstore.products.service.application.Queries.ViewModels
{
    public class ListFavoritesVW
    {
        public Guid product_id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string description { get; set; }
        public int stock_quantity { get; set; }
        public string category { get; set; }
        public List<string> imagesUrl { get; set; }
    }
}
