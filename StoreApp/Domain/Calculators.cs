using System;
using System.Collections.Generic;

namespace StoreApp.UI.Domain
{
    public interface IPromotionCalculator
    {
        public PromotionType CalculatorForPromotion { get; }
        public PromotionPrice Calculate(PricingContext context);
    }

    public class NonePromotionCalculator : IPromotionCalculator
    {
        public PromotionType CalculatorForPromotion => PromotionType.None;

        public PromotionPrice Calculate(PricingContext context)
        {
            return new PromotionPrice()
            {
                PromoPrice = context.UnitPrice * context.Quantity,
                Description = ""
            };
        }
    }

    public class PercentagePromotionCalculator : IPromotionCalculator
    {
        public PromotionType CalculatorForPromotion => PromotionType.Percentage;

        public PromotionPrice Calculate(PricingContext context)
        {
            var discountPercent = ((PercentagePromotion)context.Promotion).PromotionPercentage;

            return new PromotionPrice()
            {
                PromoPrice = (double)(((decimal)(context.UnitPrice * context.Quantity)) * (1 - (decimal)(discountPercent / 100))),
                Description = $"{discountPercent}% off"
            };
        }
    }

    public class BogoPromotionCalculator : IPromotionCalculator
    {
        public PromotionType CalculatorForPromotion => PromotionType.BOGO;

        public PromotionPrice Calculate(PricingContext context)
        {
            var promo = ((BogoPromotion)context.Promotion);
            var buy = promo.Buy;
            var get = promo.Get;

            var factor = (double)buy / (buy + get);

            var pricingQty = (double)context.Quantity * factor;
            pricingQty += (pricingQty - (int)pricingQty) > 0 ? 1 : 0;

            return new PromotionPrice()
            {
                PromoPrice = (context.UnitPrice * (int)pricingQty),
                Description = $"Buy {buy} get {get} free"
            };
        }
    }
}
