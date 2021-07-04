using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using XAutonomos.Helpers;
using XAutonomos.Models;
using XAutonomos.ViewModels.Base;
using XAutonomos.Views.ViewsAuxiliar;

namespace XAutonomos.ViewModels
{
    public class ClientesViewModel : ViewModelBase
    {
        HelperClienteAzure helper; //Traemos la instancia para poder usar los métodos.
        private ObservableCollection<Cliente> _Clientes;


        // Clientes
        public ClientesViewModel()
        {
            helper = new HelperClienteAzure();
            Task.Run(async () =>
            {
                List<Cliente> lista = await helper.GetClientes();
                lista.Sort(delegate (Cliente x, Cliente y)
                {
                    return x.Nombre.CompareTo(y.Nombre);
                });
                this.Clientes = new ObservableCollection<Cliente>(lista);
            });
        }


        public ObservableCollection<Cliente> Clientes
        {
            get { return this._Clientes; }
            set
            {
                this._Clientes = value;
                OnPropertyChanged("Clientes"); // Actualiza los resultados.
            }
        }
        // Cliente

        private Cliente _Cliente;
        public Cliente Cliente
        {
            get { return this._Cliente; }
            set { this._Cliente = value;
                OnPropertyChanged("Cliente");
            }
        }

        public void BuscarCliente(String cif)
        {
            Task.Run(async () =>{
                Cliente cli = await helper.BuscarCliente(cif);
                this.Cliente = cli;
            });
        }

        // Cliente Seleccionado.
        private Cliente _ClienteSeleccionado;
        public Cliente ClienteSeleccionado
        {
            get { return this._ClienteSeleccionado; }
            set
            {
                this._ClienteSeleccionado = value;
                OnPropertyChanged("ClienteSeleccionado");
            }
        }

        public Command DetallesCliente
        {
            get
            {
                return new Command(async () =>
                {
                    if (ClienteSeleccionado != null)
                    {
                        DetallesCliente detallesview = new DetallesCliente();
                        detallesview.inabilitarInsertar();
                        detallesview.inabilitarStacks();
                        // Cuando se selecciona al cliete 
                        ClienteViewModel viewmodelcliente = new ClienteViewModel();

                        viewmodelcliente.Cliente = this.ClienteSeleccionado;

                        detallesview.BindingContext = viewmodelcliente;
                        await Application.Current.MainPage.Navigation.PushAsync(detallesview);
                    }
                    else
                    {
                        return;
                    }
                });
            }

        }

        
    }

}