namespace Task_M.Models
{
    public class TaskModel
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string UId { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateByName { get; set; }
        public string UpdateOn { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedOn { get; set; }
    }
}
