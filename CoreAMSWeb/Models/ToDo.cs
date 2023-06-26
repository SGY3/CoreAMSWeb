namespace CoreAMSWeb.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? PageName { get; set; }
        public string? StoredProcedureName { get; set; }
        public DateTime AddedOn { get; set; }
        public int ProjectNameId { get; set; }
        public int ActivityNameId { get; set; }
        public ProjectType? ProjectName { get; set; }
        public ActivityType? ActivityName { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
