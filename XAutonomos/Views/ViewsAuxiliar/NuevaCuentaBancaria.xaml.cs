using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XAutonomos.Helpers;
using XAutonomos.Models;

namespace XAutonomos.Views.ViewsAuxiliar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NuevaCuentaBancaria : ContentPage
    {
        HelperCuentaBancaria helper;
        private string cif_usuario ;
        public static CuentaBancaria Cuenta;
        
        public NuevaCuentaBancaria()
        {
            InitializeComponent();
            helper = new HelperCuentaBancaria();
            cif_usuario = LoginPage.UsuarioAutentificado.Cif;
            
            if (Cuenta != null)
            {
                entryEntidadBancaria.Text = Cuenta.Entidad;
                entryNumCuenta.Text = Cuenta.Num_Cuenta;
                entryIban.Text = Cuenta.Iban;
                entryBic.Text = Cuenta.Bic;
                btnGuardarCuenta.Text = "Modifiar";
            }

        }

        private bool ValidarCampos()
        {
            return !String.IsNullOrWhiteSpace(entryNumCuenta.Text);
        }
        private async void btnGuardarCuenta_Clicked(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                if (Cuenta != null)
                {
                    var respuesta = await DisplayAlert("Aviso", "Ya tiene una cuenta. ¿Desea sustituir la cuenta actual?", "Si", "No");
                    if (respuesta == true)
                    {
                        var resultado = await helper.EliminarCuenta(cif_usuario);
                        if (resultado)
                        {
                            insertarCuenta();
                        }
                    }
                    else
                    {
                        await Navigation.PopAsync();
                    }

                }
                else
                {
                    insertarCuenta();
                }
            }
            else
            {
                await DisplayAlert("Aviso", "Tiene que rellenar los campos marcados con *", "Ok");
            }



        }

        public async void insertarCuenta()
        {
            CuentaBancaria nueva = new CuentaBancaria();
            nueva.Cif_Usuario = cif_usuario;
            nueva.Entidad = entryEntidadBancaria.Text;
            nueva.Num_Cuenta = entryNumCuenta.Text;
            nueva.Iban = entryIban.Text;
            nueva.Bic = entryBic.Text;

            var insert = await helper.InsertarCuenta(nueva);
            if (insert)
            {
                await DisplayAlert("Aviso", "Resultado: Cuenta Bancaria insertadada.", "Ok");
                NuevaCuentaBancaria.Cuenta = nueva;
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Resultado: No se ha podido Insertar.", "Ok");
            }
        }
    }
}