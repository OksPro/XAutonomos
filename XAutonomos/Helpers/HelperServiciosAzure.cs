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
    class HelperServiciosAzure
    {
        // rueta de conexión con la api
        private const String DireccionApi = "https://apiautonomos.azurewebsites.net/";

        // SE CREA UNA INSTANCIA DE CONEXIÓN CON EL SERVIDOR.
        private HttpClient CrearCliente()
        {
            HttpClient clientehttp = new HttpClient();
            clientehttp.DefaultRequestHeaders.Accept.Clear();
            clientehttp.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return clientehttp;
        }

        // LISTAR TODOS LOS Servicios.
        public async Task<List<Servicio>> GetServicios()
        {
            List<Servicio> listadatos = null;
            //CREAMOS LA PETICION 
            String peticion = DireccionApi + "api/Servicios";
            var uri = new Uri(string.Format(peticion, string.Empty));
            HttpClient client = CrearCliente();
            var respuesta = await client.GetAsync(uri);
            if (respuesta.IsSuccessStatusCode) // Si la respuesta es 200 (ok) es que ya podemos leer el contenido de jason 
            {
                var contenido = await respuesta.Content.ReadAsStringAsync();
                listadatos = JsonConvert.DeserializeObject<List<Servicio>>(contenido);

            }
            return listadatos;
        }

        // BUSCAR AL Servicio POR id
        public async Task<Servicio> BuscarServicio(String id)
        {
            Servicio ser = null;
            //CREAMOS LA PETICION 
            String peticion = DireccionApi + "api/Servicios/" + id;
            var uri = new Uri(string.Format(peticion, string.Empty));
            HttpClient client = CrearCliente();
            var respuesta = await client.GetAsync(uri);
            if (respuesta.IsSuccessStatusCode)
            {
                var contenido = await respuesta.Content.ReadAsStringAsync();
                ser = JsonConvert.DeserializeObject<Servicio>(contenido);
            }
            return ser;
        }

        //INSERTAR SERVICIO
        public async Task<bool> InsertarServicio(Servicio ser)
        {
            //CONVERTIMOS EL OBJETO EN JSON 
            string jsondoctor = JsonConvert.SerializeObject(ser, Formatting.Indented);
            //PASAMOS SUS DATOS A BYTES 
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(jsondoctor);
            //CREAMOS UN CONTENIDO DE BYTES PARA ENVIAR 
            //EN LA PETICION 
            ByteArrayContent content = new ByteArrayContent(buffer);
            //INDICAMOS EN LA CABECERA EL TIPO DE CONTENIDO A ENVIAR 
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //CREAMOS EL CLIENTE 
            HttpClient client = CrearCliente();
            //CREAMOS LA PETICION 
            String peticion = DireccionApi + "api/Servicios";
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

        // MODIFICAR SERVICIO
        public async Task<bool> ModificarServicio(Servicio ser)
        {
            //CONVERTIMOS EL OBJETO EN JSON 
            string jsondoctor = JsonConvert.SerializeObject(ser, Formatting.Indented);
            //PASAMOS SUS DATOS A BYTES 
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(jsondoctor);
            //CREAMOS UN CONTENIDO DE BYTES PARA ENVIAR 
            //EN LA PETICION 
            ByteArrayContent content = new ByteArrayContent(buffer);
            //INDICAMOS EN LA CABECERA EL TIPO DE CONTENIDO A ENVIAR 
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //CREAMOS EL CLIENTE 
            HttpClient client = CrearCliente();
            //CREAMOS LA PETICION PUT 
            String peticion = DireccionApi + "api/Servicios/" + ser.Id;
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

        // ELIMINAR SERVICIO
        public async Task<bool> EliminarServicio(int id)
        {
            //CREAMOS EL CLIENTE 
            HttpClient client = CrearCliente();
            //CREAMOS LA PETICION PUT 
            String peticion = DireccionApi + "api/Servicios/" + id;
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
