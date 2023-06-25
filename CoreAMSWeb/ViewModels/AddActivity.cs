using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreAMSWeb.ViewModels
{
    public class AddActivity
    {
        public int Id { get; set; }
        public int SelectedProjectId { get; set; }
        public List<SelectListItem>? ProjectName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int SelectedActivityId { get; set; }
        public List<SelectListItem>? ActivityName { get; set; }
        public string? PageName { get; set; }
        public string? StoredProcedureName { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
