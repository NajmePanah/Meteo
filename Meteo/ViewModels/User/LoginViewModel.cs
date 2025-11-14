namespace Main.Meteo.ViewModels.User
{
    public class LoginViewModel
    {
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
        public bool RememberMe { get; set; } = false;
        public string? ReturnUrl { get; set; }
    }
}
