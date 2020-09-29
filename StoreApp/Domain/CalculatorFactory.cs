using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace StoreApp.UI.Domain
{
    public interface IPromoCalculatorFactory
    {
        IPromotionCalculator GetCalculator(PromotionType promoType);
    }

    public class PromoCalculatorFactory : IPromoCalculatorFactory
    {
        private readonly List<IPromotionCalculator> calculators;

        public PromoCalculatorFactory(IEnumerable<IPromotionCalculator> calculators)
        {
            this.calculators = calculators.ToList();
        }

        public IPromotionCalculator GetCalculator(PromotionType promoType)
        {
            return calculators.Find(x => x.CalculatorForPromotion == promoType);
        }
    }
}
