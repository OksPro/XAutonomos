using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XAutonomos.Models;

namespace XAutonomos.Helpers
{
    //Tambien es helper para FACTURASERVICIO
    class HelperFacturaAzure
    {
        private const String DireccionApi = "https://apiautonomos.azurewebsites.net/";

        private HttpClient CrearCliente()
        {
            HttpClient clientehttp = new HttpClient();
            clientehttp.DefaultRequestHeaders.Accept.Clear();
            clientehttp.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return clientehttp;
        }

        public async Task<List<Factura>> GetFacturas()
        {
            List<Factura> listadatos = null;
            //CREAMOS LA PETICION 
            String peticion = DireccionApi + "api/Facturas";
            var uri = new Uri(string.Format(peticion, string.Empty));
            HttpClient client = CrearCliente();
            var respuesta = await client.GetAsync(uri);
            if (respuesta.IsSuccessStatusCode) // Si la respuesta es 200 (ok) es que ya podemos leer el contenido de jason 
            {
                var contenido = await respuesta.Content.ReadAsStringAsync();
                listadatos = JsonConvert.DeserializeObject<List<Factura>>(contenido);

            }
            return listadatos;
        }


        public async Task<Factura> BuscarFactura(String numFactura)
        {
            Factura dato = null;
            //CREAMOS LA PETICION 
            String peticion = DireccionApi + "api/Facturas/" + numFactura;
            var uri = new Uri(string.Format(peticion, string.Empty));
            HttpClient client = CrearCliente();
            var respuesta = await client.GetAsync(uri);
            if (respuesta.IsSuccessStatusCode)
            {
                var contenido = await respuesta.Content.ReadAsStringAsync();
                dato = JsonConvert.DeserializeObject<Factura>(contenido);
            }
            return dato;
        }

        public async Task<Factura> MostrarFactura(String numFactura)
        {
            Factura dato = null;
            //CREAMOS LA PETICION 
            String peticion = DireccionApi + "api/Facturas/MostrarFactura" + numFactura;
            var uri = new Uri(string.Format(peticion, string.Empty));
            HttpClient client = CrearCliente();
            var respuesta = await client.GetAsync(uri);
            if (respuesta.IsSuccessStatusCode)
            {
                var contenido = await respuesta.Content.ReadAsStringAsync();
                dato = JsonConvert.DeserializeObject<Factura>(contenido);
            }
            return dato;
        }

        public async Task<bool> InsertarFactura(Factura factura)
        {
            string jsondoctor = JsonConvert.SerializeObject(factura, Formatting.Indented);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(jsondoctor);
            ByteArrayContent content = new ByteArrayContent(buffer);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient client = CrearCliente();
            String peticion = DireccionApi + "api/Facturas";
            var respuesta = await client.PostAsync(peticion, content);
            return respuesta.IsSuccessStatusCode;
        }
        public async Task<bool> CrearFactura(int numFactura)
        {
            string jsondoctor = JsonConvert.SerializeObject(numFactura, Formatting.Indented);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(jsondoctor);
            ByteArrayContent content = new ByteArrayContent(buffer);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient client = CrearCliente();
            String peticion = DireccionApi + "api/Facturas/CrearFactura/" + numFactura;
            var respuesta = await client.PostAsync(peticion, content);
            return respuesta.IsSuccessStatusCode;
        }

        //Implrmrntar Modificar factura y eliminar factura
        //public async Task<bool> ModificarCuenta(CuentaBancaria cuenta)
        //{
        //    string jsondoctor = JsonConvert.SerializeObject(cuenta, Formatting.Indented);
        //    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(jsondoctor);
        //    ByteArrayContent content = new ByteArrayContent(buffer);
        //    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //    HttpClient client = CrearCliente();
        //    String peticion = DireccionApi + "api/CuentaBancaria/" + cuenta.Cif_Usuario;
        //    var respuesta = await client.PutAsync(peticion, content);
        //    return respuesta.IsSuccessStatusCode;

        //}
        //public async Task<bool> EliminarCuenta(string cif)
        //{
        //    HttpClient client = CrearCliente();
        //    String peticion = DireccionApi + "api/CuentaBancaria/" + cif;
        //    var respuesta = await client.DeleteAsync(peticion);
        //    return respuesta.IsSuccessStatusCode;
        //}

        // ****************FacturaServicios
        public async Task<bool> InsertarFacturaServicios(FacturaServicios facturaServicio)
        {
            string jsondoctor = JsonConvert.SerializeObject(facturaServicio, Formatting.Indented);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(jsondoctor);
            ByteArrayContent content = new ByteArrayContent(buffer);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient client = CrearCliente();
            String peticion = DireccionApi + "api/FacturaServicios";
            var respuesta = await client.PostAsync(peticion, content);
            return respuesta.IsSuccessStatusCode;
        }
    }
}
