using System.ComponentModel.DataAnnotations;

namespace Lab_3_Mini_API.Models
{
    public class Interests
    {
        [Key]
        public int Id { get; set; }
        public string InterestName { get; set; }
        public string InterestDescription { get; set;}

        public virtual ICollection<Persons> Persons {  get; set; }    
        
    }
}
