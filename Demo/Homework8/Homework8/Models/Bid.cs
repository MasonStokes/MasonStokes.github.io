namespace Homework8.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bid
    {
        public int ID { get; set; }

        public int ITEMID { get; set; }

        public int BUYERID { get; set; }

        [Required]
        public int PRICE { get; set; }

        public DateTime? TIMESTAMP { get; set; } = DateTime.Now;

        public virtual Buyer Buyer { get; set; }

        public virtual Item Item { get; set; }
    }
}
