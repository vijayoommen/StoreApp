using NUnit.Framework;
using Moq;
using StoreApp.UI.DataAccess;
using StoreApp.UI.Application;
using StoreApp.UI.Domain;

namespace StoreApp.Tests
{
    [TestFixture]
    public class ProductStoreTests
    {
        private ProductStore productStore;
        private Mock<IFileHandler> fileHandler;

        [SetUp]
        public void Setup()
        {
            fileHandler = new Moq.Mock<IFileHandler>();
            fileHandler.Setup(x => x.ReadAll(It.IsAny<string>())).Returns(GetSampleProductsJson());

            var appConfig = new Mock<IAppConfiguration>();
            productStore = new ProductStore(appConfig.Object, fileHandler.Object);
        }

        [Test]
        public void ProductStoreShouldSaveProduct()
        {
            Product p = new Product() { Sku = "12", Id = 4, Name = "test", UnitPrice = 1 };
            productStore.SaveProduct(p);
            fileHandler.Verify(x => x.Writeall(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void ProductStoreShouldGetProductById(int productId)
        {
            var product = productStore.GetProductbyId(productId);
            Assert.That(product.Id == productId);
        }

        [TestCase("1245")]
        [TestCase("839")]
        [TestCase("99999")]
        public void ProductStoreShouldGetProductBySku(string sku)
        {
            var product = productStore.GetProductBySku(sku);
            Assert.That(product.Sku == sku);
            fileHandler.Verify(x => x.ReadAll(It.IsAny<string>()), Times.Once);
        }

        private string GetSampleProductsJson()
        {
            return @"{
                          ""$type"": ""System.Collections.Generic.List`1[[StoreApp.UI.Domain.Product, StoreApp.UI]], System.Private.CoreLib"",
                          ""$values"": [
                            {
                              ""$type"": ""StoreApp.UI.Domain.Product, StoreApp.UI"",
                              ""Id"": 1,
                              ""Sku"": ""1245"",
                              ""Name"": ""Bananas"",
                              ""UnitPrice"": 1.25,
                              ""Promotion"": {
                                ""$type"": ""StoreApp.UI.Domain.NonePromotion, StoreApp.UI"",
                                ""Name"": ""None"",
                                ""PromotionType"": 0
                              }
                            },
                            {
                              ""$type"": ""StoreApp.UI.Domain.Product, StoreApp.UI"",
                              ""Id"": 2,
                              ""Sku"": ""839"",
                              ""Name"": ""Rubber bands"",
                              ""UnitPrice"": 10.0,
                              ""Promotion"": {
                                ""$type"": ""StoreApp.UI.Domain.PercentagePromotion, StoreApp.UI"",
                                ""PromotionPercentage"": 10.0,
                                ""Name"": ""Percentage"",
                                ""PromotionType"": 1
                              }
                            },
                            {
                              ""$type"": ""StoreApp.UI.Domain.Product, StoreApp.UI"",
                              ""Id"": 3,
                              ""Sku"": ""99999"",
                              ""Name"": ""Pepto Bismol"",
                              ""UnitPrice"": 4.88,
                              ""Promotion"": {
                                ""$type"": ""StoreApp.UI.Domain.BogoPromotion, StoreApp.UI"",
                                ""Buy"": 2,
                                ""Get"": 2,
                                ""Name"": ""Bogo"",
                                ""PromotionType"": 2
                              }
                            }
                          ]
                        }
                    ";
        }
    }
}
