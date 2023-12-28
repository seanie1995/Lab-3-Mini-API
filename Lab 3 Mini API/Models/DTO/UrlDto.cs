namespace Lab_3_Mini_API.Models.DTO
{
    public class UrlDto
    {
        public string Url { get; set; }
        public virtual Interests Interests { get; set; }
        public virtual Persons Persons { get; set; }
    }
}
