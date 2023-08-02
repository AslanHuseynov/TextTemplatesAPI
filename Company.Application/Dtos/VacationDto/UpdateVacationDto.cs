namespace Company.Application.Dtos.VacationDto
{
    public class UpdateVacationDto : BaseVacationDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
    }
}
