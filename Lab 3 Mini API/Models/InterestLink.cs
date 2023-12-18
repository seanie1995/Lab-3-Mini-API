namespace Lab_3_Mini_API.Models
{
    public class InterestLink
    {
        public int LinkID { get; set; }
        public int PersonID_FK { get; set; }
        public int InterestID_FK { get; set; }

        public virtual Persons Persons { get; set; }
        public virtual Interests Interests { get; set; }
    }
}
