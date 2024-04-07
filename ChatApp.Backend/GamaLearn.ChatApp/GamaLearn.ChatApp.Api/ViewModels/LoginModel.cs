namespace GamaLearn.ChatApp.Api.ViewModels
{
    /// <summary>
    /// User Login Model
    /// </summary>
    public class LoginModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } 
        public string ChatRecipient { get; set; }
         
        public bool IsChatRoom { get; set; }
    }
}
