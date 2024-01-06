namespace Lab_3_Mini_API.Models
{
    public class InterestUrl
    {
        public int Id { get; set; } 
        public string Url { get; set; }

        public virtual Interest Interest { get; set; }

        public virtual Person Person { get; set; }
    }
}
