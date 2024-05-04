﻿namespace todoapp.Models
{
    internal class User
    {
        public int? Id { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? IsActivated { get; set; }
        public List<Todo>? Todos { get; set; }
    }
}