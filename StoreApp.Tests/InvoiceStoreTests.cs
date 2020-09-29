using NUnit.Framework;
using StoreApp.UI.DataAccess;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using StoreApp.UI.Application;

namespace StoreApp.Tests
{
    [TestFixture]
    public class InvoiceStoreTests
    {
        private InvoiceStore invoiceStore;

        [SetUp]
        public void Setup()
        {
            var fileHandler = new Mock<IFileHandler>();
            fileHandler.Setup(x => x.ReadAll(It.IsAny<string>())).Returns(GetInvoiceSample());

            var appConfig = new Mock<IAppConfiguration>();

            invoiceStore = new InvoiceStore(appConfig.Object, fileHandler.Object);
        }

        [Test]
        public void InvoiceStoreShouldreturnInvoices()
        {
            var invoiceSummary = invoiceStore.GetInvoiceSummary();
            Assert.That(invoiceSummary.Count == 2);
        }

        private string GetInvoiceSample()
        {
            return @"[
                          {
                            ""Date"": ""2020-09-29T00:00:00-05:00"",
                            ""LineItems"": [
                              {
                                ""ProductName"": ""Rubber bands"",
                                ""Units"": 10,
                                ""UnitPrice"": 10.0,
                                ""Discount"": """",
                                ""TotalPrice"": 100.0
                              }
                            ],
                            ""Total"": 100.0
                          },
                          {
                            ""Date"": ""2020-09-29T00:00:00-05:00"",
                            ""LineItems"": [
                              {
                                ""ProductName"": ""Rubber bands"",
                                ""Units"": 10,
                                ""UnitPrice"": 10.0,
                                ""Discount"": """",
                                ""TotalPrice"": 100.0
                              },
                              {
                                ""ProductName"": ""Pepto Bismol"",
                                ""Units"": 10,
                                ""UnitPrice"": 5,
                                ""Discount"": """",
                                ""TotalPrice"": 50.00
                              }
                            ],
                            ""Total"": 150.0
                          },
                        ]
                        ";
        }
    }
}
