namespace CRUDMVC.Models
{
    // Specific ce date doresc sa vad in "Add", pagina unde pot face un nou user
    public class UserViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
