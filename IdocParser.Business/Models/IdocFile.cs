using System;

namespace IdocParser.Business.Models
{
    /// <summary>
    /// Represents an SAP IDocs file
    /// </summary>
    public class IdocFile
    {
        public string IdocNumber { get; set; }
        public string SenderSystem { get; set; }
        public string ReceiverSystem { get; set; }
        public DateTime CreationDate { get; set; }
        public string IdocType { get; set; }
        public string Content { get; set; }
        public string RelatedPONumber { get; set; }
    }
} 