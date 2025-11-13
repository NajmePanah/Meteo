
namespace Main
{
    public class SupportContact
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string InternalPhone { get; set; }
    }
    public class SystemCategory
    {
        public string CategoryTitle { get; set; }
        public string Icon { get; set; }   // اضافه شد
        public List<SystemItem> Systems { get; set; }
    }

    public class SystemItem
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public List<int> SupportIds { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }   // اضافه شد
    }

}
