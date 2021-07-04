using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using XAutonomos.Helpers;
using XAutonomos.Models;
using XAutonomos.ViewModels;
using XAutonomos.Views.ViewsAuxiliar;

namespace XAutonomos.Views
{
    public partial class UsuarioView : ContentPage
    {
        HelperUsuarioAzure helper;
        HelperCuentaBancaria helperCuenta;
        public Usuario UsuarioAutentificado;
        CuentaBancaria cuenta;

        public UsuarioView()
        {
            InitializeComponent();
            UsuarioAutentificado = LoginPage.UsuarioAutentificado;
            helper = new HelperUsuarioAzure();
            helperCuenta = new HelperCuentaBancaria();
            cuenta = new CuentaBancaria();
            if (UsuarioAutentificado != null)
            {
                entryNomEmpresa.Text = UsuarioAutentificado.NombreEntidad;
                entryNombre.Text = UsuarioAutentificado.Nombre;
                entryApellidos.Text = UsuarioAutentificado.Apellidos;
                entryCif.Text = UsuarioAutentificado.Cif;
                entryDireccion.Text = UsuarioAutentificado.Direccion;
                entryCodPostal.Text = UsuarioAutentificado.CodigoPostal.ToString();
                entryLocalidad.Text = UsuarioAutentificado.Localidad;
                entryMunicipio.Text = UsuarioAutentificado.Municipio;
                entryTelefono.Text = UsuarioAutentificado.Telefono1;
                entryCorreo.Text = UsuarioAutentificado.Email;
                entryPassword.Text = UsuarioAutentificado.Password;

            }
            Task.Run(async () => {
                CuentaBancaria c = await helperCuenta.BuscarCuenta(UsuarioAutentificado.Cif);
                cuenta = c;
                NuevaCuentaBancaria.Cuenta = cuenta;
            });



        }

        public void HabilitarModificarUsu(Object sender, EventArgs args)
        {
            stackDatosUsu.IsEnabled = true;
            btnGuardarUsuario.IsEnabled = true;
            btnGuardarUsuario.IsVisible = true;
            entryCif.IsEnabled = false;
            entryCorreo.IsEnabled = false;
            btnGuardarUsuario.BackgroundColor = Color.Aqua;
            btnCuentaBancaria.IsVisible = false;
            btnCuentaBancaria.IsEnabled = false;

        }


        // Este método lo llamaremos cuando sepamos que los campos se han modificado y guardado.
        public void DeshabilitarModificarUsu()
        {
            stackDatosUsu.IsEnabled = false;
            btnGuardarUsuario.IsEnabled = false;
            btnGuardarUsuario.IsVisible = false;
            btnGuardarUsuario.BackgroundColor = Color.Pink;
            btnCuentaBancaria.IsVisible = true;
            btnCuentaBancaria.IsEnabled = true;
        }



        private bool ValidarCampos()
        {
            return !String.IsNullOrWhiteSpace(entryCif.Text)
                && !String.IsNullOrWhiteSpace(entryPassword.Text)
                && !String.IsNullOrWhiteSpace(entryCorreo.Text)
                && !String.IsNullOrWhiteSpace(entryNomEmpresa.Text);
        }

        private async void btnGuardarUsuario_Clicked(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                Usuario usuario = new Usuario();
                usuario.Cif = UsuarioAutentificado.Cif;
                usuario.Email = UsuarioAutentificado.Email;
                usuario.Password = entryPassword.Text;
                usuario.NombreEntidad = entryNomEmpresa.Text;
                usuario.Nombre = entryNombre.Text;
                usuario.Apellidos = entryApellidos.Text;
                usuario.Direccion = entryDireccion.Text;
                usuario.Localidad = entryLocalidad.Text;
                usuario.Municipio = entryMunicipio.Text;
                usuario.Telefono1 = entryTelefono.Text;
                if (entryCodPostal.Text != null)
                {
                    usuario.CodigoPostal = int.Parse(entryCodPostal.Text);
                }
                var insert = await helper.ModificarUsuario(usuario);
                if (insert)
                {
                    //DisplayAlert("Guardar Usuario", "Resultado: Usuario guardado.", "Ok");
                    //System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                    
                    var respuesta = await DisplayAlert("Guardar Usuario", "Usuario guardado correctamenta. ¿Reiniciar para que los cambios se apliquen?", "Si", "No");
                    if (respuesta == true)
                    {
                        System.Diagnostics.Process.GetCurrentProcess().Kill();
                    }
                    else
                    {
                        DeshabilitarModificarUsu();
                    }
                }
                else
                {
                    await DisplayAlert("Guardar Usuario", "Resultado: No se ha podido insertar.", "Ok");
                }

            }
            else
            {
                await DisplayAlert("Aviso", "Tiene que rellenar los campos marcados con *", "Ok");
            }
        }

        private async void btnCuentaBancaria_Clicked(object sender, EventArgs e)
        {
            NuevaCuentaBancaria cuentaB = new NuevaCuentaBancaria();
            
            await Application.Current.MainPage.Navigation.PushAsync(cuentaB);
        }

        
    }
}

