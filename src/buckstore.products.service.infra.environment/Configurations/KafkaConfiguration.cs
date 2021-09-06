using System;

namespace buckstore.products.service.infra.environment.Configurations
{
    public class KafkaConfiguration
    {
        public string ConnectionString {get;set;}
        public string Group { get; set; }
        public string ProductsToOrders {get;set;}
        public string ProductsFromOrders {get;set;}
        public string ProductsFromManagerCreate {get;set;}
        public string ProductsFromManagerUpdate {get;set;}
        public string ProductsFromManagerDelete {get;set;}
        public string ProductsStockResponseSuccess { get; set; }
        public string ProductsStockResponseFail { get; set; }
    }
}
