using StoreApp.UI.Application;
using StoreApp.UI.Domain;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace StoreApp.UI.DataAccess
{
    public interface IInvoiceStore
    {
        void SaveInvoice(Invoice invoice);
        List<InvoiceStore.SalesSummary> GetInvoiceSummary();
    }

    public class InvoiceStore : IInvoiceStore
    {
        private readonly IAppConfiguration config;
        private readonly IFileHandler fileHandler;

        public InvoiceStore(IAppConfiguration config, IFileHandler handler)
        {
            this.config = config;
            this.fileHandler = handler;
        }

        public void SaveInvoice(Invoice invoice)
        {
            var invoices = GetInvoices();
            invoices.Add(invoice);
            SaveInvoices(invoices);
        }

        public List<SalesSummary> GetInvoiceSummary()
        {
            var invoices = GetInvoices();
            var result = invoices
                .SelectMany(s => s.LineItems, (s, li) => new {Date = s.Date.ToShortDateString(), s.Total, li.ProductName, li.Units, li.UnitPrice, li.Discount, li.TotalPrice })
                .GroupBy(i => new { i.Date, i.ProductName })
                .Select(g => new SalesSummary
                {
                    Date = g.Key.Date,
                    ProductName = g.Key.ProductName,
                    Sold = g.Sum(inv => inv.Units),
                    Price = g.Sum(inv => inv.TotalPrice)
                }).ToList();
            
            return result;
        }

        private List<Invoice> GetInvoices()
        {
            var contents = fileHandler.ReadAll(config.InvoiceStore);
            var invoices = JsonConvert.DeserializeObject<List<Invoice>>(contents);
            return invoices;
        }

        private void SaveInvoices(List<Invoice> invoices)
        {
            var json = JsonConvert.SerializeObject(invoices);
            fileHandler.Writeall(config.InvoiceStore, json);
        }

        public class SalesSummary
        {
            public string Date { get; internal set; }
            public string ProductName { get; internal set; }
            public int Sold { get; internal set; }
            public double Price { get; internal set; }
        }
    }
}
