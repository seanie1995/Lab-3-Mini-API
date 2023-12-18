namespace Lab_3_Mini_API.Models
{
    public class InterestLink
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int InterestId { get; set; }

        public virtual Persons Person { get; set; }

        public virtual Interests Interest { get; set; }




    }
}
