using System;


namespace XAutonomos.Models
{
    
    public class Factura
    {
        public Factura() { }

        //Atributos:
        public int Num_Factura { get; set; }

        
        public String Fecha { get; set; }

       
        public String Cif_cliente { get; set; }

        
        public int Iva { get; set; }

        
        public int Irpf { get; set; }

       
        public double Total { get; set; }

       
        public byte[] Byte_factura { get; set; }

        
        public String Cif_usuario { get; set; }

        public String Nombre_Cliente { get; set; }
    }
}
