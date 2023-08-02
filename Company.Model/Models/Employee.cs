namespace Company.Model.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
