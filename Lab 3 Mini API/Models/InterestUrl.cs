namespace Lab_3_Mini_API.Models
{
    public class InterestUrl
    {
        public int Id { get; set; } 
        public string Url { get; set; }

        public virtual Interests Interests { get; set; }

        public virtual Persons Persons { get; set; }
    }
}
