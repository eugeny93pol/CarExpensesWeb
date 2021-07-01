using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CE.DataAccess.Models
{
    public class SparePart : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        public byte Quantity { get; set; }

        public uint? LimitByMileage { get; set; }

        [DataType(DataType.Duration)]
        public DateTime? LimitByTime { get; set; }

        public string Description { get; set; }

        public Guid CarActionId { get; set; }
    }
}