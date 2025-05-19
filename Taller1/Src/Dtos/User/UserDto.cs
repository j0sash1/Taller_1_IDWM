namespace Taller1.Src.Dtos.User
{
    public class UserDto
    {
        public required string FirstName { get; set; } = null!;
        public required string LastName { get; set; } = null!;
        public required string Email { get; set; } = null!;
        public required string Telephone { get; set; } = null!;
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? Commune { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public DateOnly? BirthDate { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime? LastAccess { get; set; }
        public bool IsActive { get; set; }
    }
}