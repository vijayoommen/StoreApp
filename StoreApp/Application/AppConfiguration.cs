using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace StoreApp.UI.Application
{
    public interface IAppConfiguration
    {
        public string ProductStore { get; }
        public string InvoiceStore { get; }
    }

    public class AppConfiguration : IAppConfiguration
    {
        public string ProductStore => ConfigurationManager.AppSettings.Get("ProductStore");
        public string InvoiceStore => ConfigurationManager.AppSettings.Get("InvoiceStore");
    }
}
