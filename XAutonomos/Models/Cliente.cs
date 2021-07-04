using System;


namespace XAutonomos.Models
{
    
    public class Cliente
    {  

        public Cliente() { }

        public Cliente(String cif) {
            this.Cif = cif;
        }

        // Los atributos

        public String Cif { get; set; }

       
        public String Nombre { get; set; }

        
        public String Direccion { get; set; }

       
        public int Cp { get; set; }

        
        public String Localidad { get; set; }

        
        public String Municipio { get; set; }

        
    }
}
