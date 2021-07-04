using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XAutonomos.Helpers;
using XAutonomos.Models;
using XAutonomos.ViewModels;

namespace XAutonomos.Views.ViewsAuxiliar
{
    public partial class DetallesCliente : ContentPage
    {
        HelperClienteAzure helper;
        public DetallesCliente()
        {
            InitializeComponent();
            helper = new HelperClienteAzure();
            
        }

        private bool ValidarCampos()
        {
            return !String.IsNullOrWhiteSpace(entryCif.Text)
                && !String.IsNullOrWhiteSpace(entryNombre.Text)
                && !String.IsNullOrWhiteSpace(entryDireccion.Text)
                && !String.IsNullOrWhiteSpace(entryLocalidad.Text)
                && !String.IsNullOrWhiteSpace(entryMunicipio.Text);
        }
        // inserta cliente
        private async void btnGuardarClienteNuevo_ClickedAsync(object sender, EventArgs e)
        {
            
            if (ValidarCampos())
            {
                Cliente cliente = new Cliente();
                cliente.Cif = entryCif.Text;
                cliente.Nombre = entryNombre.Text;
                cliente.Direccion = entryDireccion.Text;
                cliente.Cp = int.Parse(entryCp.Text);
                cliente.Localidad = entryLocalidad.Text;
                cliente.Municipio = entryMunicipio.Text;
                var insert =  await helper.InsertarCliente(cliente);
                if (insert)
                {
                    await DisplayAlert("Datos", "Datos insertados.", "Ok");
                    limpiarCampos();
                    ClientesViewModel refrescar = new ClientesViewModel();
                    refrescar.OnPropertyChanged("Clientes");
                }
                
            }
            else
            {
                await DisplayAlert("Aviso", "Tiene que completar todos los campos.", "Ok");
            }
            
            
            

        }

        public void limpiarCampos()
        {
            entryCif.Text = "";
            entryNombre.Text = "";
            entryDireccion.Text = "";
            entryCp.Text = "";
            entryLocalidad.Text = "";
            entryMunicipio.Text = "";

        }

        public void inabilitarBorrarEliminar()
        {
            
            btnModificarCliente.IsEnabled = false;
            btnModificarCliente.IsVisible = false;
            
        }

        public void inabilitarInsertar()
        {
            btnGuardarClienteNuevo.IsEnabled = false;
            btnGuardarClienteNuevo.IsVisible = false;
        }

        public void inabilitarStacks()
        {
            stackCif.IsEnabled = false;
            stackNombre.IsEnabled = false;
            stackCp.IsEnabled = false;
            stackDireccion.IsEnabled = false;
            stackLocalidad.IsEnabled = false;
            stackMunicio.IsEnabled = false;
            btnModificarCliente.IsEnabled = false;
            btnModificarCliente.IsVisible = false;
        }

        public void habilitarStacks()
        {
            //stackCif.IsEnabled = true;
            stackNombre.IsEnabled = true;
            stackDireccion.IsEnabled = true;
            stackCp.IsEnabled = true;
            stackLocalidad.IsEnabled = true;
            stackMunicio.IsEnabled = true;
            btnModificarCliente.IsEnabled = true;
            btnModificarCliente.IsVisible = true;
        }

        //Modifica al cliente
        public async void guardarModificaciones_Clicked(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente();
            cliente.Cif = entryCif.Text;
            cliente.Nombre = entryNombre.Text;
            cliente.Direccion = entryDireccion.Text;
            cliente.Cp = int.Parse(entryCp.Text);
            cliente.Localidad = entryLocalidad.Text;
            cliente.Municipio = entryMunicipio.Text;
            await helper.ModificarCliente(cliente);
            await DisplayAlert("Datos", "Datos modificados correctamente.", "Ok");
            await Navigation.PopAsync();
            

        }
        private void imbBtnActivarModificar_Clicked(object sender, EventArgs e)
        {
            habilitarStacks();
        }

        //Elimina cliente.
        async void seleccionaEliminarCliente(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente(entryCif.Text);
            var ans = await DisplayAlert("Eliminar?", "Desea eliminar " +cliente.Nombre + "?" , "Si", "No");
            if (ans == true)
            {
                var resultado = await helper.EliminarCliente(cliente.Cif);
                if (resultado)
                {
                    await DisplayAlert("Aviso", "Cliente Eliminado.", "Ok");
                    await Navigation.PopAsync();
                }else
                {
                    await DisplayAlert("Aviso", "No se ha podido eliminar", "Ok");
                }
                
            }
        }
    }
}
