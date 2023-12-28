namespace Lab_3_Mini_API.Models.DTO
{
    public class InterestDto
    {
        public string Name {  get; set; }
        public string Description { get; set; }

        public virtual ICollection<Persons> Persons { get; set; }

        public virtual ICollection<InterestUrl> InterestUrls { get; set; }

    }
}
