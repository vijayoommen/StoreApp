using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.Json.Serialization;

namespace StoreApp.UI.Domain
{
    public enum PromotionType
    {
        None = 0,
        Percentage = 1,
        BOGO = 2
    }

    public interface IPromotion
    {
        string Name { get; set; }
        PromotionType PromotionType { get; set; }
    }

    public class BasePromotion : IPromotion
    {
        public string Name { get; set; }
        public PromotionType PromotionType { get; set; }


        public BasePromotion() { }
    }

    [Serializable]
    public class NonePromotion : BasePromotion
    {
        public NonePromotion() 
        {
            Name = "None";
            PromotionType = PromotionType.None;
        }
    }

    [Serializable]
    public class PercentagePromotion : BasePromotion
    {
        public double PromotionPercentage { get; set; }

        public PercentagePromotion()
        {
            Name = "Percentage";
            PromotionType = PromotionType.Percentage;
        }
    }

    [Serializable]
    public class BogoPromotion : BasePromotion
    {
        public int Buy { get; set; }
        public int Get { get; set; }

        public BogoPromotion() 
        {
            Name = "Bogo";
            PromotionType = PromotionType.BOGO;
        }
    }
}
