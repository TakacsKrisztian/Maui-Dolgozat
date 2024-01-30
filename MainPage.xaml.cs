namespace Dolgozat
{
    public class Users
    {
        public Users(string UserName, string Password)
        {
            this.UserName = UserName;
            this.Password = Password;
        }

        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public partial class MainPage : ContentPage
    {
        List<Users> UserList = new List<Users>();
        public static string currentuser;
        public static string CurrentUser { get => currentuser; set => currentuser = value; }

        public MainPage()
        {
            InitializeComponent();
            UserName.Text = "";
            Password.Text = "";
        }

        private async void OnLoginBtnClicked(object sender, EventArgs e)
        {
                if (UserName.Text == "" || Password.Text == "")
                {
                    await DisplayAlert("Hiba a bejelentkezéskor!", "Adjon meg minden adatot!", "OK");
                }
                else
                {
                    bool userExists = UserList.Any(u => u.UserName == UserName.Text);

                    if (userExists)
                    {
                        var user = UserList.First(u => u.UserName == UserName.Text);

                        if (user.Password == Password.Text)
                        {
                            CurrentUser = UserName.Text;
                            await DisplayAlert("Sikeres bejelentkezés!", "Üdvözöljük!", "OK");
                            await Shell.Current.GoToAsync("ListEditor");
                        }
                        else
                        {
                            await DisplayAlert("Hiba a bejelentkezéskor!", "Hibás jelszó! Próbálja újra.", "OK");
                        }
                    }
                    else
                    {
                        UserList.Add(new Users(UserName.Text, Password.Text));
                        CurrentUser = UserName.Text;
                        await DisplayAlert("Sikeres regisztráció!", "Üdvözöljük!", "OK");
                        await Shell.Current.GoToAsync("ListEditor");
                    }
                }
        }
    }
}