using System;

namespace buckstore.products.service.infra.environment.Configurations
{
    public class KafkaConfiguration
    {
        public string ConnectionString {get;set;}
        public string Group { get; set; }
        public string ProductsToOrders {get;set;}
        public string ProductsFromOrders {get;set;}
        public string ProductsFromManager {get;set;}
    }
}