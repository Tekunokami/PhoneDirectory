using System;

namespace PhoneDirectory.Application.DTOs.Group
{
    public class GroupDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ContactCount { get; set; }
    }
}
