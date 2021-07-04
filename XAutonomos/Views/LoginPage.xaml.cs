using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XAutonomos.Helpers;
using XAutonomos.Models;

namespace XAutonomos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        HelperUsuarioAzure helper;
        private List<Usuario> Usuarios;
        public static Usuario UsuarioAutentificado;
        public LoginPage()
        {
            InitializeComponent();
            helper = new HelperUsuarioAzure();
            Task.Run(async () => {
                List<Usuario> lista = await helper.GetUsuarios();
                this.Usuarios = new List<Usuario>(lista);
            });
        }
        private bool ValidarCampos()
        {
            return !String.IsNullOrWhiteSpace(entryUserEmail.Text)
                && !String.IsNullOrWhiteSpace(entryUserPasswor.Text);
        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                UsuarioAutentificado = comprobarUsuario(entryUserEmail.Text, entryUserPasswor.Text);
                if (UsuarioAutentificado != null)
                {
                    Application.Current.MainPage = new NavigationPage(new MainPage());
                }
                else
                {
                    DisplayAlert("Error", "Usuario o Contraseña incorrectos", "Ok");
                }
            }
            else
            {
                DisplayAlert("Error", "Tiene que introducir usuario y contraseña", "Ok");
            }


        }

        private async void NewUser_Clicked(object sender, EventArgs e)
        {
            NuevoUsuario nuevoUsuario = new NuevoUsuario();
            await Application.Current.MainPage.Navigation.PushAsync(nuevoUsuario);
        }

        private Usuario comprobarUsuario(string email, string password)
        {
            
            foreach (Usuario u in Usuarios)
            {
                if (u.Email.ToLower().Equals(email) && u.Password.Equals(password))
                {
                    
                    return u;
                }
            }
            return null;
        }
    }
}