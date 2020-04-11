namespace MCBLabber.Models
{
    public class Journal
    {
        public uint Id { get; set; }

        public uint GroupId { get; set; }
        public Group Group { get; set; }

        public uint SubjectId { get; set; }
        public Subject Subject { get; set; }

        public uint TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public string SubGroup { get; set; }

        public Journal(uint groupId, uint subjectId, uint teacherId, string subGroup)
        {
            GroupId = groupId;
            SubjectId = subjectId;
            TeacherId = teacherId;
            SubGroup = subGroup;
        }
    }
}
