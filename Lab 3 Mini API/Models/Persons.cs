using System.ComponentModel.DataAnnotations;

namespace Lab_3_Mini_API.Models
{
    public class Persons
    {
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int phoneNumber { get; set; }
             
        public virtual ICollection<Interests> Interests { get; set; }   
      
    }
}
