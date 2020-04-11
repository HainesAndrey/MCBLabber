namespace MCBLabber.Models
{
    public class Group
    {
        public uint Id { get; set; }
        public string Title { get; set; }

        public Group(string title)
        {
            Title = title;
        }
    }
}
