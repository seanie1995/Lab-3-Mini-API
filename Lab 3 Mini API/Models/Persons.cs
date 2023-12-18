namespace Lab_3_Mini_API.Models
{
    public class Persons
    {
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public virtual ICollection<Interests> Interests { get; set; }
        public virtual ICollection<InterestLink> InterestLinks { get; set; }



    }
}
