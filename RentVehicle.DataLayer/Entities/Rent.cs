using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentVehicle.DataLayer.Entities
{
    public class Rent
    {
        public Rent(double price)
        {
            Price = price;
        }
        public Rent(int day,int buyerId , int vehicleId, double price)
        {
            Day = day;
            BuyerId = buyerId;
            VehicleId = vehicleId;
            Price = price;

            RentDate = DateTime.UtcNow;
        }


        [Key]
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public int? VehicleId { get; set; }

        public string Number { get; set; }

        [Range(1 , 100000 , ErrorMessage = "")]
        public double Price { get; set; }
        public int Day { get; set; }
        public bool IsCanceled { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime? CancelDate { get; set; }

        [ForeignKey(nameof(BuyerId))]
        public User Buyer { get; set; }
        [ForeignKey(nameof(VehicleId))]
        public Vehicle Vehicle { get; set; }
    }
}