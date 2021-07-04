using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XAutonomos.Helpers;
using XAutonomos.ViewModels;
using XAutonomos.ViewModels.Base;
using XAutonomos.Views;
using XAutonomos.Views.ViewsAuxiliar;

namespace XAutonomos.Models
{

    class GenerarFacturaViewModel : ViewModelBase
    {
        HelperClienteAzure HelperClientes;
        HelperServiciosAzure HelperServicios;
        HelperFacturaAzure HelperFactura;
        private ObservableCollection<Cliente> _ListaClientes;
        private ObservableCollection<Servicio> _MiListaServicios;
        private ObservableCollection<Factura> _MiListaFacturas;
        private List<string> _ListaIva;
        private Servicio _MiServicioSeleccionado;
        private Cliente _ClienteSeleccionado;
        private string _NumeroFactura;
        private Int16 _Irpf;
        private string _IvaSeleccionado;
        public static bool Mensaje;
        
        


        public GenerarFacturaViewModel()
        {
            
            HelperClientes = new HelperClienteAzure();
            HelperServicios = new HelperServiciosAzure();
            HelperFactura = new HelperFacturaAzure();
            Task.Run(async () =>
            {
                List<Cliente> lista = await HelperClientes.GetClientes();
                this.ListaClientes = new ObservableCollection<Cliente>(lista);
            });
            Task.Run(async () =>
            {
                List<Servicio> listaS = await HelperServicios.GetServicios();
                this.MiListaServicios = new ObservableCollection<Servicio>(listaS);
            });
            Task.Run(async () =>
            {
                List<Factura> listaF = await HelperFactura.GetFacturas();
                this.MiListaFacturas = new ObservableCollection<Factura>(listaF);
            });

            Irpf = 15;
            ListaIva = new List<string>();
            ListaIva.Add("4");
            ListaIva.Add("10");
            ListaIva.Add("21");
            IvaSeleccionado = ListaIva[2];
            NumeroFactura = "00000";
            
        }

        public ObservableCollection<Cliente> ListaClientes
        {
            get { return this._ListaClientes; }
            set
            {
                this._ListaClientes = value;
                OnPropertyChanged("ListaClientes"); // Actualiza los resultados.
            }
        }
        public Cliente ClienteSeleccionado
        {
            get { return this._ClienteSeleccionado; }
            set
            {
                this._ClienteSeleccionado = value;
                OnPropertyChanged("ClienteSeleccionado");
            }
        }

        public ObservableCollection<Servicio> MiListaServicios
        {
            get { return this._MiListaServicios; }
            set
            {
                this._MiListaServicios = value;
                OnPropertyChanged("MiListaServicios"); // Actualiza los resultados.
            }
        }
        public Servicio MiServicioSeleccionado
        {
            get { return this._MiServicioSeleccionado; }
            set
            {
                this._MiServicioSeleccionado = value;
                OnPropertyChanged("MiServicioSeleccionado");
            }
        }

        public ObservableCollection<Factura> MiListaFacturas
        {
            get { return this._MiListaFacturas; }
            set
            {
                this._MiListaFacturas = value;
                OnPropertyChanged("MiListaFacturas"); // Actualiza los resultados.
            }
        }
        public string IvaSeleccionado
        {
            get { return this._IvaSeleccionado; }
            set
            {
                this._IvaSeleccionado = value;
                OnPropertyChanged("IvaSeleccionado");
            }
        }

        public string NumeroFactura
        {
            get { return this._NumeroFactura; }
            set
            {
                this._NumeroFactura = value;
                OnPropertyChanged("NumeroFactura");
            }
        }
        public Int16 Irpf
        {
            get { return this._Irpf; }
            set
            {
                this._Irpf = value;
                OnPropertyChanged("Irpf");
            }
        }

        public List<string> ListaIva
        {
            get { return this._ListaIva; }
            set { 
                this._ListaIva = value;
                OnPropertyChanged("ListaIva");
            }
        }

   
        public void buscarNumFacturaMayor()
        {
            string numeroF;
            if (MiListaFacturas.Count != 0)
            {
                
                numeroF = MiListaFacturas[MiListaFacturas.Count - 1].Num_Factura.ToString();
                
                NumeroFactura = (int.Parse(numeroF) + 1).ToString();
            }
            else
            {
                NumeroFactura = "_____";
            }
            
        }
        private bool ValidarCampos()
        {
            return
                !String.IsNullOrWhiteSpace(NumeroFactura)
                && (ClienteSeleccionado != null)
                && !String.IsNullOrWhiteSpace(Irpf.ToString())
                && (IvaSeleccionado != null)
                && (MiServicioSeleccionado != null);

        }
        private async void GenerarFactura()
        {
            

            if  (ValidarCampos())
            {
                Factura factura = new Factura();
                factura.Num_Factura = int.Parse(NumeroFactura);
                factura.Cif_cliente = ClienteSeleccionado.Cif;
                factura.Cif_usuario = LoginPage.UsuarioAutentificado.Cif;
                factura.Iva = int.Parse(IvaSeleccionado);
                factura.Irpf = Irpf;

                FacturaServicios fc = new FacturaServicios();
                fc.Id_Servicio = MiServicioSeleccionado.Id;
                fc.Num_factura = factura.Num_Factura;
                
                Mensaje = true;
                var insertFactura = await HelperFactura.InsertarFactura(factura);
                if (insertFactura)
                {
                    //-----Si se selecciona varis servicios hay que hacer un bucle de posts.
                    var insertFS = await HelperFactura.InsertarFacturaServicios(fc);

                    if (insertFS)
                    {
                        var generarFactura = await HelperFactura.CrearFactura(factura.Num_Factura);
                    }
                }
                
            }
            else
            {
                Mensaje = false;
            }
            
        }

      
       
        public ICommand generarNumFactura => new Command(buscarNumFacturaMayor);
        public ICommand insertarFactura => new Command(GenerarFactura);

        
    }
}
