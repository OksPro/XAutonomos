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
    public class HelperClienteAzure
    {
       
        // rueta de conexión con la api
        private const String DireccionApi = "https://apiautonomos.azurewebsites.net/";

        // SE CREA UNA INSTANCIA DE CONEXIÓN CON EL SERVIDOR.
        private HttpClient CrearClienteWeb()
        {
            HttpClient clientehttp = new HttpClient();
            clientehttp.DefaultRequestHeaders.Accept.Clear();
            clientehttp.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return clientehttp;
        }

        // LISTAR TODOS LOS CLIENTES.
        public async Task<List<Cliente>> GetClientes()
        {
            List<Cliente> listadatos = null;
            //CREAMOS LA PETICION 
            String peticion = DireccionApi + "api/Clientes";
            var uri = new Uri(string.Format(peticion, string.Empty));
            HttpClient client = CrearClienteWeb();
            var respuesta = await client.GetAsync(uri);
            if (respuesta.IsSuccessStatusCode)
            {
                var contenido = await respuesta.Content.ReadAsStringAsync();
                listadatos = JsonConvert.DeserializeObject<List<Cliente>>(contenido);
                
            }
            return listadatos;
        }

        // BUSCAR AL CLIENTE POR CIF
        public async Task<Cliente> BuscarCliente(String cif)
        {
            Cliente cli = null;
            //CREAMOS LA PETICION 
            String peticion = DireccionApi + "api/Clientes/" + cif;
            var uri = new Uri(string.Format(peticion, string.Empty));
            HttpClient client = CrearClienteWeb();
            var respuesta = await client.GetAsync(uri); // establecer conexion
            if (respuesta.IsSuccessStatusCode)
            {
                var contenido = await respuesta.Content.ReadAsStringAsync(); //guardamos el json
                cli = JsonConvert.DeserializeObject<Cliente>(contenido);
            }
            return cli;
        }
        
        public async Task<bool> InsertarCliente(Cliente cli )
        {
            //CONVERTIMOS EL OBJETO EN JSON 
            string jsondoctor = JsonConvert.SerializeObject(cli, Formatting.Indented);
            //PASAMOS SUS DATOS A BYTES 
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(jsondoctor);
            //CREAMOS UN CONTENIDO DE BYTES PARA ENVIAR 
            //EN LA PETICION 
            ByteArrayContent content = new ByteArrayContent(buffer);
            //INDICAMOS EN LA CABECERA EL TIPO DE CONTENIDO A ENVIAR 
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //CREAMOS EL CLIENTE 
            HttpClient client = CrearClienteWeb();
            //CREAMOS LA PETICION 
            String peticion = DireccionApi + "api/Clientes";
            //REALIZAMOS LA LLAMADA AL API POST ENVIANDO EL CONTENIDO 
            var respuesta = await client.PostAsync(peticion, content);
            if (respuesta.IsSuccessStatusCode)
            {
                
                return true;
            }
            else
            {
                
                return false;
            }
        }
       
        
        public async Task<bool> ModificarCliente(Cliente cli)
        {
            //CONVERTIMOS EL OBJETO EN JSON 
            string jsondoctor = JsonConvert.SerializeObject(cli, Formatting.Indented);
            //PASAMOS SUS DATOS A BYTES 
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(jsondoctor);
            //CREAMOS UN CONTENIDO DE BYTES PARA ENVIAR 
            //EN LA PETICION 
            ByteArrayContent content = new ByteArrayContent(buffer);
            //INDICAMOS EN LA CABECERA EL TIPO DE CONTENIDO A ENVIAR 
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //CREAMOS EL CLIENTE 
            HttpClient client = CrearClienteWeb();
            //CREAMOS LA PETICION PUT 
            String peticion = DireccionApi + "api/Clientes/" + cli.Cif;
            //REALIZAMOS LA LLAMADA AL API POST ENVIANDO EL CONTENIDO 
            var respuesta = await client.PutAsync(peticion, content);
            if (respuesta.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
       
        public async Task<bool> EliminarCliente(String cif)
        {
            //CREAMOS EL CLIENTE 
            HttpClient client = CrearClienteWeb();
            //CREAMOS LA PETICION PUT 
            String peticion = DireccionApi + "api/Clientes/" + cif;
            //REALIZAMOS LA LLAMADA AL API 
            var respuesta = await client.DeleteAsync(peticion);
            if (respuesta.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }
}
