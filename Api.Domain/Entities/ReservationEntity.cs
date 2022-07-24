using System.Collections.Generic;

namespace Api.Domain.Entities
{
    public class ReservationEntity : BaseEntity
    {
        public UserEntity User { get; set; }
        public List<ItemEntity> Itens { get; set; }
        public bool Status { get; set; }
        public int TipePayment { get; set; }
        public int? FlagCardPayment { get; set; }
        public int DeliveryMethod { get; set; }
        public string Address { get; set; }
        public int AddressNumber { get; set; }
        public string Neighborhood { get; set; }
    }
}