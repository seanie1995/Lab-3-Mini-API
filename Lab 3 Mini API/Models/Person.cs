using System.ComponentModel.DataAnnotations;

namespace Lab_3_Mini_API.Models
{
    public class Person
    {
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
             
        public virtual ICollection<Interest> Interests { get; set; }   
      
    }
}
