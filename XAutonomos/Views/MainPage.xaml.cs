using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XAutonomos.Views;
using XAutonomos.Views.ViewsAuxiliar;

namespace XAutonomos
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            btnUsuConfig.Clicked += ir_UsuariosView;
            btnEmpresasConfig.Clicked += ir_ClientesView;
            btnServiConfig.Clicked += ir_ServiciosView;
           
        }

        private void ir_UsuariosView(Object sender, EventArgs args)
        {
            UsuarioView userpage = new UsuarioView();
            Device.BeginInvokeOnMainThread(async () => await Navigation.PushAsync(userpage)); // Con posibilidad de retorno a la página madre.
            // await Navigation.PushModalAsync(new UsuarioView()); // Abre la pestaña sin posibilidad de retorno 
        }

        private void ir_ClientesView(Object sender, EventArgs args)
        {
            Device.BeginInvokeOnMainThread(async () => await Navigation.PushAsync(new ClienteView()));

        }

        private void ir_ServiciosView(Object sender, EventArgs args)
        {
            Device.BeginInvokeOnMainThread(async () => await Navigation.PushAsync(new ServicioView()));

        }

        private void btnGenerarFactura_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () => await Navigation.PushAsync(new GenerarFacturaView()));
        }

        private void btnFacturasConfig_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () => await Navigation.PushAsync(new FacturasView()));
        }
    }
}
