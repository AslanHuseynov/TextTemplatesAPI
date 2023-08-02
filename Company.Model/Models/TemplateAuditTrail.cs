using Company.Application;

namespace Company.Model.Models
{
    public class TemplateAuditTrail
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime ChangeDate { get; set; }
        public int TemplateId { get; set; }
        public Template Template { get; set; }
        public TemplateAction Action { get; set; }
        public string UpdatedContent { get; set; }
    }
}
