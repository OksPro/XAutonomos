using System;
using Xamarin.Forms;
using XAutonomos.Helpers;
using XAutonomos.Models;
using XAutonomos.ViewModels.Base;

namespace XAutonomos.ViewModels
{
    public class ClienteViewModel : ViewModelBase
    {
        private HelperClienteAzure helper;
        private Cliente _Cliente;
        public ClienteViewModel()
        {
            this.helper = new HelperClienteAzure();
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
     

        public Command InsertarCliente
        {
            get
            {
                return new Command(async () => {
                    await helper.InsertarCliente(this.Cliente);
                });
            }
        }

        public Command ModificarCliente
        {
            get
            {
                return new Command(async () => {
                    await helper.ModificarCliente(this.Cliente);
                    OnPropertyChanged("Cliente");
                });
            }
        }

        public Command EliminarCliente
        {
            get
            {
                return new Command(async () => {
                    await helper.EliminarCliente(this.Cliente.Cif);
                });
            }
        }
    }
}
