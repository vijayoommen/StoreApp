using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace StoreApp.UI.Domain
{
    public class Invoice
    {
        public DateTime Date { get; set; }
        public List<LineItem> LineItems { get; private set; }

        [JsonIgnore]
        public double Total => LineItems.Sum(x => x.TotalPrice);

        public class LineItem
        {
            public string ProductName { get; set; }
            public int Units { get; set; }
            public double UnitPrice { get; set; }
            public string Discount { get; set; }
            public double TotalPrice { get; set; }
        }

        public Invoice()
        {
            Date = DateTime.Today;
            LineItems = new List<LineItem>();
        }

        public void AddLineItem(string productName, int units, double unitPrice, string discount, double totalPrice)
        {
            LineItems.Add(new LineItem() { 
                ProductName = productName, 
                Units = units, 
                UnitPrice = unitPrice, 
                Discount = discount, 
                TotalPrice = totalPrice });

        }
    }
}
