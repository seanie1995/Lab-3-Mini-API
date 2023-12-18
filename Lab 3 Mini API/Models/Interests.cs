namespace Lab_3_Mini_API.Models
{
    public class Interests
    {
        public int InterestID { get; set; }
        public string InterestName { get; set; }
        public string InterestDescription { get; set;}

        public virtual ICollection<Persons> Persons {  get; set; }    
        public virtual ICollection<InterestLink> InterestLink { get; set; }
    }
}
