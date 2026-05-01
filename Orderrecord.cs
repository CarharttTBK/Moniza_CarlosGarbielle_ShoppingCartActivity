using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartActivity
{
    public class OrderRecord
    {
        public string ReceiptNumber { get; set; } = "";
        public string CheckoutDateTime { get; set; } = "";
        public double GrandTotal { get; set; }
        public double Discount { get; set; }
        public double FinalTotal { get; set; }
        public double Payment { get; set; }
        public double Change { get; set; }
    }
}