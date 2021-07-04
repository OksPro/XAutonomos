using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XAutonomos.Helpers;
using XAutonomos.Models;
using XAutonomos.Views.ViewsAuxiliar;

namespace XAutonomos.Views
{
    public partial class ClienteView : ContentPage
    {
        HelperClienteAzure helper;
        public ClienteView()
        {
            InitializeComponent();
            helper = new HelperClienteAzure();
            

        }
        public async void AniadirCliente(Object sender, EventArgs args)
        {
            DetallesCliente detalleCliente = new DetallesCliente();
            detalleCliente.inabilitarBorrarEliminar();
            await Application.Current.MainPage.Navigation.PushAsync(detalleCliente);
           // await Navigation.PushModalAsync(detalleCliente);
        }

        private async void CargarDatos_Clicked(object sender, EventArgs e)
        {
            List<Cliente> lista = await helper.GetClientes();
            listaClientes.ItemsSource = lista;
        }
    }
}
