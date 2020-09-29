using NUnit.Framework;
using StoreApp.UI.Domain;
using System;
using System.Collections.Generic;

namespace StoreApp.Tests
{
    public class CalculatorFactoryTests
    {
        List<IPromotionCalculator> calculators;
        PromoCalculatorFactory factory;
        [SetUp]
        public void Setup()
        {
            calculators = new List<IPromotionCalculator>();

            calculators.AddRange(new IPromotionCalculator[] { new NonePromotionCalculator(), new PercentagePromotionCalculator(), new BogoPromotionCalculator() });
            factory = new PromoCalculatorFactory(calculators);
        }

        [TestCase(PromotionType.None, typeof(NonePromotionCalculator))]
        [TestCase(PromotionType.Percentage, typeof(PercentagePromotionCalculator))]
        [TestCase(PromotionType.BOGO, typeof(BogoPromotionCalculator))]
        public void ShouldGetCalculatorFor(PromotionType promotionType, Type calculatorType)
        {
            Assert.That(factory.GetCalculator(promotionType).GetType() == calculatorType);
        }
    }
}