using System;
using System.ComponentModel.DataAnnotations;

namespace RentVehicle.DataLayer.Entities
{
    public class User
    {
        public User()
        {
            
        }
        public User(string name)
        {
            Name = name;
            RegisterDate = DateTime.UtcNow;
        }
        [Key]
        public int Id { get; set; }

        [MinLength(3 , ErrorMessage = "")]
        [MaxLength(40 , ErrorMessage = "")]
        [Required(AllowEmptyStrings = false , ErrorMessage = "")]
        public string Name { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime? RemoveDate { get; set; }
    }
}