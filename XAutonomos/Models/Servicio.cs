using System;


namespace XAutonomos.Models
{
    
    public class Servicio
    {
        //Constructores:
        public Servicio() { }
        public Servicio(String nombre, double precio)
        {
            this.Descripcion = nombre;
            this.Precio = precio;
        }

        //Atributos:
        public int Id { get; set; }

        
        public String Descripcion { get; set; }

        
        public double Precio { get; set; }

        
    }
}
