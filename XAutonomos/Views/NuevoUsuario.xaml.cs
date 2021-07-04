using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XAutonomos.Helpers;
using XAutonomos.Models;

namespace XAutonomos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NuevoUsuario : ContentPage
    {

        HelperUsuarioAzure helper;
        private List<Usuario> Usuarios;
        public NuevoUsuario()
        {
            InitializeComponent();
            helper = new HelperUsuarioAzure();
            Task.Run(async () => {
                List<Usuario> lista = await helper.GetUsuarios();
                this.Usuarios = new List<Usuario>(lista);
            });
        }

        public bool comprobarExistencia(string cif, string correo)
        {
            foreach (Usuario u in Usuarios)
            {
                if (u.Cif.Equals(cif) || u.Email.Equals(correo))
                {
                    return true;
                }
            }
            return false;
        }
        private bool ValidarCampos()
        {
            return !String.IsNullOrWhiteSpace(entryCif.Text)
                && !String.IsNullOrWhiteSpace(entryPassword.Text)
                && !String.IsNullOrWhiteSpace(entryCorreo.Text)
                && !String.IsNullOrWhiteSpace(entryNomEmpresa.Text);
        }

        private async void crearUsuarioNuevo_Clicked(Object sender, EventArgs args)
        {
            if (ValidarCampos())
            {
                Usuario usuario = new Usuario();
                usuario.Email = entryCorreo.Text;
                usuario.Password = entryPassword.Text;
                usuario.Cif = entryCif.Text;
                usuario.NombreEntidad = entryNomEmpresa.Text;
                usuario.Nombre = entryNombre.Text;
                usuario.Apellidos = entryApellidos.Text;
                usuario.Direccion = entryDireccion.Text;
                usuario.Localidad = entryLocalidad.Text;
                usuario.Municipio = entryMunicipio.Text;
                usuario.Telefono1 = entryTelefono.Text;
                if(entryCodPostal.Text != null)
                {
                    usuario.CodigoPostal = int.Parse(entryCodPostal.Text);
                }

                if (!comprobarExistencia(usuario.Cif, usuario.Email))
                {
                    var insert = await helper.InsertarUsuario(usuario);
                    if (insert)
                    {
                        await DisplayAlert("Insertar Usuario", "Resultado: Usuario guardado.", "Ok");
                        //await Shell.Current.GoToAsync("..");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Insertar Usuario", "Resultado: No se ha podido insertar.", "Ok");
                        
                    }
                }
                else
                {
                    await DisplayAlert("Error", "Ya exixte un usuario con mismos credenciales. ", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Aviso", "Tiene que rellenar los campos marcados con *", "Ok");
            }
        }

    }
}