using Microsoft.Maui.Controls.Platform;
using System.Net.Http.Json;
using System.Text;

namespace FrontMaui
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void DoLogin(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            var resp = await client.PostAsync(
                "http://localhost:5072/Auth/Login",
                JsonContent.Create(new
                {
                    id = usuarioEntry.Text,
                    contraseña = contraseñaEntry.Text
                })
            );

            if (!resp.IsSuccessStatusCode)
            {
                await DisplayAlert("Error", "Usuario o contraseña incorrectos", "OK");
                return;
            }

            var token = await resp.Content.ReadFromJsonAsync<LoginResponse>();
            if (token == null)
            {
                await DisplayAlert("Error", "No se ha podido obtener el token", "OK");
                return;
            }

            Sesion.Token = token.Token;

            await Shell.Current.GoToAsync("///TareasPage");
        }
    }

    internal class LoginResponse
    {
        public string Token { get; set; }
    }
}

