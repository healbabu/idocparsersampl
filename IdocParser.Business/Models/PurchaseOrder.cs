using System;
using System.Collections.Generic;

namespace IdocParser.Business.Models
{
    /// <summary>
    /// Represents a simple Purchase Order
    /// </summary>
    public class PurchaseOrder
    {
        public string PONumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string VendorId { get; set; }
        public string VendorName { get; set; }
        public decimal TotalAmount { get; set; }
        public string Currency { get; set; }
        public List<PurchaseOrderLine> LineItems { get; set; } = new List<PurchaseOrderLine>();
    }

    /// <summary>
    /// Represents a line item in a Purchase Order
    /// </summary>
    public class PurchaseOrderLine
    {
        public int LineNumber { get; set; }
        public string MaterialNumber { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }
} 