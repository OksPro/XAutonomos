using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XAutonomos.Helpers;
using XAutonomos.Models;

namespace XAutonomos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GenerarFacturaView : ContentPage
    {
        HelperFacturaAzure helper;
        
        public GenerarFacturaView()
        {
            InitializeComponent();
            
            helper = new HelperFacturaAzure();
        }


        public async void btnInsertarNumero_Clicked(object sender, EventArgs e)
        {
            string anio = DateTime.Now.Year.ToString();
            anio = anio.Substring(2);
            string result = await DisplayPromptAsync("Cambiar Número de Factura", "¿Cuál es el número de próxima factura?", initialValue: anio + "001", maxLength: 5, keyboard: Keyboard.Numeric);
            labelNumFactura.Text = result;
        }

        private async void btnGenerarFactura_Clicked(object sender, EventArgs e)
        {
            if (GenerarFacturaViewModel.Mensaje)
            {
                var respuesta = await DisplayAlert("Aviso", "Resultado: Factura creada con éxito.", "Abrir", "Salir");

                if (respuesta)
                {
                    string direccion = "https://apiautonomos.azurewebsites.net/api/Facturas/MostrarFactura/" + labelNumFactura.Text;
                    Uri uri = new Uri(direccion);
                    await Browser.OpenAsync(uri);

                }
                else
                {
                    await Navigation.PopAsync(); //Volver al Main
                }
            }else
            {
                 await DisplayAlert("Error", "Datos incompletos.", "Ok");
            }
           
        }
        
    }
}

