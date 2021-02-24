namespace Results.WebAPI.Models.ViewModels
{
    public class UserViewModel : AdminViewModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
    }
}