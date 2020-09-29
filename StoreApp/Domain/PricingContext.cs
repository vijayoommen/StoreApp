using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.UI.Domain
{
    public class PromotionPrice
    {
        public double PromoPrice { get; set; }
        public string Description { get; set; }
    }

    public class PricingContext
    {
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public IPromotion Promotion { get; set; }
    }
}
