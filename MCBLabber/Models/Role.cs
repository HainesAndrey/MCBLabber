﻿namespace MCBLabber.Models
{
    public class Role
    {
        public uint Id { get; set; }
        public string Title { get; set; }

        public Role(string title)
        {
            Title = title;
        }
    }
}
