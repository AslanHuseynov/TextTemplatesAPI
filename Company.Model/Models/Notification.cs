namespace Company.Model.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string SavedBy { get; set; }
        public string Text { get; set;}
        public string Addressee { get; set; }
    }
}
