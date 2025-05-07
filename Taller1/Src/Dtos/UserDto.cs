namespace Taller1.Src.Dtos
{
    public class UserDto
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Email { get; set; }

        public required string Thelephone { get; set; }

        public string? Street { get; set; }

        public string? Number { get; set; }

        public string? Commune { get; set; }

        public string? Region { get; set; }

        public string? PostalCode { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime? LastAccess { get; set; }
        public bool IsActive { get; set; }
    }
}