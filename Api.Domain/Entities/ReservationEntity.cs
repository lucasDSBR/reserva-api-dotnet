using System.Collections.Generic;

namespace Api.Domain.Entities
{
    public class ReservationEntity : BaseEntity
    {
        public string NameClient { get; set; }
        public string PhoneNumberClient { get; set; }
        public List<ItemReservationEntity> Itens { get; set; }
        public int Status { get; set; }
        public string TipePayment { get; set; }
        public int? FlagCardPayment { get; set; }
        public string DeliveryMethod { get; set; }
        public string Address { get; set; }
        public int? AddressNumber { get; set; }
        public string Neighborhood { get; set; }
    }
}