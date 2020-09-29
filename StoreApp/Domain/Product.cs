using Newtonsoft.Json;

namespace StoreApp.UI.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public BasePromotion Promotion { get; set; }
    }
}
