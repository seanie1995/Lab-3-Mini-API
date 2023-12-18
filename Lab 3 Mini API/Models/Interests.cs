using System.ComponentModel.DataAnnotations;

namespace Lab_3_Mini_API.Models
{
    public class Interests
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set;}
        public string Url { get; set; }

        public virtual ICollection<Persons> Persons { get; set; }

        public virtual ICollection<InterestLink> InterestLinks { get; set; }



    }
}
