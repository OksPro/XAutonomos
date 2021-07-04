using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XAutonomos.Helpers;
using XAutonomos.Models;
using XAutonomos.ViewModels.Base;
using XAutonomos.Views.ViewsAuxiliar;

namespace XAutonomos.ViewModels
{
    class FacturasViewModel : ViewModelBase
    {
        HelperFacturaAzure helper;
        HelperClienteAzure helperCliente;
        private ObservableCollection<Factura> _ListaFacturas;
        private Cliente _Cliente;
        private Factura _FacturaSeleccionada;

        private Factura _Factura;

        // Constructor
        public FacturasViewModel()
        {
            helper = new HelperFacturaAzure();
            helperCliente = new HelperClienteAzure();
            Task.Run(async () =>
            {
                List<Factura> lista = await helper.GetFacturas();
                lista = ordenarL(lista);
                this.ListaFacturas = new ObservableCollection<Factura>(lista);
            });
            
        }

        private List<Factura> ordenarL(List<Factura> lista)
        {
            lista.Sort(delegate (Factura x, Factura y)
            {
                return y.Num_Factura.CompareTo(x.Num_Factura);
            });
            return lista;
            
        }

        public ObservableCollection<Factura> ListaFacturas
        {
            get { return this._ListaFacturas; }
            set
            {
                this._ListaFacturas = value;
                OnPropertyChanged("ListaFacturas"); // Actualiza los resultados.
            }
        }

        public Factura FacturaSeleccionada
        {
            get { return this._FacturaSeleccionada; }
            set
            {
                this._FacturaSeleccionada = value;
                OnPropertyChanged("FacturaSeleccionada"); // Actualiza los resultados.
            }
        }

        public Factura Factura
        {
            get { return this._Factura; }
            set
            {
                this._Factura = value;
                OnPropertyChanged("Factura");
            }
        }

        
        public Cliente Cliente
        {
            get { return this._Cliente; }
            set
            {
                this._Cliente = value;
                OnPropertyChanged("Cliente");
            }
        }
        public void BuscarCliente(String cif)
        {
            Task.Run(async () => {
                Cliente cli = await helperCliente.BuscarCliente(cif);
                this.Cliente = cli;
            });
        }
        
        public Command AbrirFactura
        {
            get
            {
                return new Command(async () =>
                {
                    if (FacturaSeleccionada != null)
                    {
                        string direccion = "https://apiautonomos.azurewebsites.net/api/Facturas/MostrarFactura/" + FacturaSeleccionada.Num_Factura;
                        Uri uri = new Uri(direccion);
                        await Browser.OpenAsync(uri);
                    }
                });
            }
        }


    }
}
