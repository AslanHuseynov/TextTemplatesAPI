namespace Company.Application.Dtos.TemplateDto
{
    public class UpdateTemplateDto : BaseTemplateDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public bool IsExpired { get; set; }
    }
}
