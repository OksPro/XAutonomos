using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;
using XAutonomos.Helpers;
using XAutonomos.Models;
using XAutonomos.ViewModels;
using XAutonomos.Views.ViewsAuxiliar;

namespace XAutonomos.Views
{
    public partial class ServicioView : ContentPage
    {
        HelperServiciosAzure helper;
        
        
        public ServicioView()
        {
            InitializeComponent();
            helper = new HelperServiciosAzure();
            deshabilitarStackAltaServicio();
            
        }
        
        
            

        private async void btnBorrarServicio_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Datos", "Servicio eliminado con exito.", "Ok");
            refrescarVistaServicios();
        }

        

        private void btnAniadirServicio_Clicked(object sender, EventArgs e)
        {
            stackAltaServisio.IsVisible = true;
           
        }

        async void btnGuardarNuevo_Clicked(object sender, EventArgs e)
        {
            
            if (descripcionServicio.Text != "" && precioServicio.Text != "" && precioServicio.Text != "0" && precioServicio.Text != null)
            {
                String descripcion = descripcionServicio.Text;
                double precio = double.Parse(precioServicio.Text);
                Servicio s = new Servicio(descripcion,precio);
                await helper.InsertarServicio(s);
                deshabilitarStackAltaServicio();
                refrescarVistaServicios();
            } else
            {
                await DisplayAlert("Datos", "Tiene que indicar la Descripción y el Precio.", "Ok");
            } 

        }

        // METODOS
        public async void refrescarVistaServicios()
        {
            listServisios.ItemsSource = await helper.GetServicios();
        }
        public void deshabilitarStackAltaServicio()
        {
            stackAltaServisio.IsVisible = false;
            descripcionServicio.Text = "";
            precioServicio.Text ="0";
        }
    }
}
