using System;


namespace XAutonomos.Models
{
  
    public class Usuario
    {
        //Constructores:
        public Usuario() { }
        
        //Atributos:
        public String Email { get; set; }

        public String Password { get; set; }

        public String Cif { get; set; }

        public String NombreEntidad { get; set; }

        public String Nombre { get; set; }

        public String Apellidos { get; set; }

        public String Direccion { get; set; }
        public int CodigoPostal { get; set; }

        public String Localidad { get; set; }

        public String Municipio { get; set; }

        public String Telefono1 { get; set; }

        public String Telefono2 { get; set; }

    }
}
