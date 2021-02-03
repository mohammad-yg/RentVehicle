using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace RentVehicle.DataLayer.Entities
{
    public class Vehicle
    {
        public Vehicle()
        {
        }
        public Vehicle(int creatorId, string name, VehicleType vehicleType, double price)
        {
            CreatorId = creatorId;
            Name = name;
            VehicleType = vehicleType;
            Price = price;
            CreateDate = DateTime.UtcNow;
        }
        [Key]
        public int Id { get; set; }
        public int CreatorId { get; set; }
        public int? RemoverId { get; set; }

        [MinLength(3, ErrorMessage = "")]
        [MaxLength(40, ErrorMessage = "")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "")]
        public string Name { get; set; }

        [Range(1 , 1000 , ErrorMessage = "")]
        public double Price { get; set; }
        public VehicleType VehicleType { get; set; }
        public bool IsRented { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? RemoveDate { get; set; }

        [ForeignKey(nameof(CreatorId))]
        public virtual User Creator { get; set; }
        [ForeignKey(nameof(RemoverId))]
        [AllowNull]
        public virtual User Remover { get; set; }
    }
}