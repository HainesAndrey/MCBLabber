namespace MCBLabber.Models
{
    public class Teacher
    {
        public uint UserId { get; set; }
        public User User { get; set; }

        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }

        public Teacher(uint userId, string surname, string firstName, string secondName)
        {
            UserId = userId;
            Surname = surname;
            FirstName = firstName;
            SecondName = secondName;
        }
    }
}
