using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Domain.Entities
{
    public class ItemReservationEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        private DateTime? _createAt;
        public DateTime? createAt
        {
            get { return _createAt; }
            set { _createAt = (value == null ? DateTime.UtcNow : value); }
        }

        public string IdMaterialOrig { get; set; }   
        public DateTime? UpdateAt { get; set; }
        public string Name { get; set; }
        public string PathPhoto { get; set; }
        public string Description { get; set; }
        public bool Fee { get; set; }
        public double ValueFee { get; set; }
        public int AmountDemanded { get; set; }
        public double Price { get; set; }
    }
}