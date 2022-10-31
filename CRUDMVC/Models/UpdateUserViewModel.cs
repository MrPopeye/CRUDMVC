namespace CRUDMVC.Models
{
    // Specific ce date doresc sa vad in View, mai exact atunci cand selectez un user si vreau sa-i modific ceva la datele personale
    public class UpdateUserViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
