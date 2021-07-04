using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XAutonomos.Helpers;
using XAutonomos.Models;
using XAutonomos.ViewModels.Base;

namespace XAutonomos.ViewModels
{
    class ServiciosViewModel : ViewModelBase
    {
        //  ATRIBUTOS
        private ObservableCollection<Servicio> _ListaServicios;

        public ObservableCollection<Servicio> ListaServicios
        {
            get { return this._ListaServicios; }
            set
            {
                this._ListaServicios = value;
                OnPropertyChanged("ListaServicios");
            }
        }

        private Servicio _ServicioSelecionado;

        public Servicio ServicioSelecionado
        {
            get { return this._ServicioSelecionado; }
            set
            {
                this._ServicioSelecionado = value;
                OnPropertyChanged("ServicioSelecionado");
            }
        }

        //  INSTANCIAS:
        HelperServiciosAzure helper; //Traemos la instancia para poder usar los métodos.

        //  CONSTRUCTOR
        public ServiciosViewModel()
        {
            helper = new HelperServiciosAzure();
            // Cargar la lista nada más empezar arrancar la vista de servicios.
            Task task = iniciarListaServiciosAsync();


        }

        //  FUNCCIONES

        public async Task iniciarListaServiciosAsync()
        {
            List<Servicio> lista = await helper.GetServicios();
            lista.Sort(delegate (Servicio x, Servicio y)
            {
                return y.Id.CompareTo(x.Id);
            });
            ListaServicios = new ObservableCollection<Servicio>(lista);
        }



        public async void eliminarServicioSeleccionado()
        {
            if (ServicioSelecionado != null)
            {
                await helper.EliminarServicio(ServicioSelecionado.Id);
            }
        }


        //  COMMAND 
        public ICommand EliminarServiciosCommand => new Command(eliminarServicioSeleccionado);

    }
}
