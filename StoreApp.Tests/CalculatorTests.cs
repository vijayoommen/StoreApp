using NUnit.Framework;
using StoreApp.UI.Domain;

namespace StoreApp.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        private NonePromotionCalculator nonePromoCalculator;
        private PercentagePromotionCalculator percentagePromoCalculator;
        private BogoPromotionCalculator bogoPromoCalculator;

        [SetUp]
        public void Setup()
        {
            nonePromoCalculator = new NonePromotionCalculator();
            percentagePromoCalculator = new PercentagePromotionCalculator();
            bogoPromoCalculator = new BogoPromotionCalculator();
        }

        [TestCase(1, 10, 10)]
        [TestCase(2, 10, 20)]
        [TestCase(2, 1.5, 3)]
        [TestCase(2, 1.75, (2*1.75))]
        public void NonePromotionCalculatorShouldReturnPrice(int quantity, double unitPrice, double expectedPrice )
        {
            var price = nonePromoCalculator.Calculate(new PricingContext() { Promotion = new NonePromotion(), Quantity = quantity, UnitPrice = unitPrice });
            Assert.That(price.PromoPrice == expectedPrice, "Price did not match for NonePromo");
            Assert.That(price.Description == string.Empty, "Promo description did not match");
        }

        [TestCase(1, 10, 2, 9.8)]
        [TestCase(1, 10, 50, 5)]
        [TestCase(1, 10, 60, 4)]
        [TestCase(1, 10, 75, 2.5)]
        [TestCase(1, 10, 80, 2)]
        public void PercentagePromotionCalculatorShouldReturnPrice(int quantity, double unitPrice, double promoPercent, double expectedPrice)
        {
            var price = percentagePromoCalculator.Calculate(
                new PricingContext() { Promotion = new PercentagePromotion() { PromotionPercentage = promoPercent }, 
                Quantity = quantity, 
                UnitPrice = unitPrice });

            Assert.That(price.PromoPrice == expectedPrice, "Price did not match");
            Assert.That(price.Description == $"{promoPercent}% off", "Promo description did not match");
        }

        [TestCase(4, 1, 1, 1, 2)]
        [TestCase(5, 1, 1, 1, 3)]
        [TestCase(7, 1, 1, 1, 4)]
        [TestCase(8, 1, 1, 1, 4)]
        [TestCase(10, 1, 1, 1, 5)]
        [TestCase(10, 1, 2, 1, 7)]
        [TestCase(10, 1, 4, 1, 8)]
        [TestCase(10, 1, 9, 1, 9)]
        public void BogoPromotionCalculatorShouldReturnPrice(int quantity, double unitPrice, int buy, int get, double expectedPrice)
        {
            var price = bogoPromoCalculator.Calculate(
                new PricingContext()
                {
                    Promotion = new BogoPromotion() { Buy = buy, Get = get},
                    Quantity = quantity,
                    UnitPrice = unitPrice
                });

            Assert.That(price.PromoPrice == expectedPrice, "Price did not match");
            Assert.That(price.Description == $"Buy {buy} get {get} free", "Promo description did not match");
        }
    }
}
