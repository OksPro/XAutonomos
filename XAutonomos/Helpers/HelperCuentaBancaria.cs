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
    class HelperCuentaBancaria
    {
       
        private const String DireccionApi = "https://apiautonomos.azurewebsites.net/";

        private HttpClient CrearCliente()
        {
            HttpClient clientehttp = new HttpClient();
            clientehttp.DefaultRequestHeaders.Accept.Clear();
            clientehttp.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return clientehttp;
        }

        public async Task<List<CuentaBancaria>> GetCuentas()
        {
            List<CuentaBancaria> listadatos = null;
            //CREAMOS LA PETICION 
            String peticion = DireccionApi + "api/CuentaBancaria";
            var uri = new Uri(string.Format(peticion, string.Empty));
            HttpClient client = CrearCliente();
            var respuesta = await client.GetAsync(uri);
            if (respuesta.IsSuccessStatusCode) // Si la respuesta es 200 (ok) es que ya podemos leer el contenido de jason 
            {
                var contenido = await respuesta.Content.ReadAsStringAsync();
                listadatos = JsonConvert.DeserializeObject<List<CuentaBancaria>>(contenido);

            }
            return listadatos;
        }

        
        public async Task<CuentaBancaria> BuscarCuenta(String cif)
        {
            CuentaBancaria cuenta = null;
            //CREAMOS LA PETICION 
            String peticion = DireccionApi + "api/CuentaBancaria/" + cif;
            var uri = new Uri(string.Format(peticion, string.Empty));
            HttpClient client = CrearCliente();
            var respuesta = await client.GetAsync(uri);
            if (respuesta.IsSuccessStatusCode)
            {
                var contenido = await respuesta.Content.ReadAsStringAsync();
                cuenta = JsonConvert.DeserializeObject<CuentaBancaria>(contenido);
            }
            return cuenta;
        }

        public async Task<bool> InsertarCuenta(CuentaBancaria cuenta)
        {
            string jsondoctor = JsonConvert.SerializeObject(cuenta, Formatting.Indented);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(jsondoctor);
            ByteArrayContent content = new ByteArrayContent(buffer);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient client = CrearCliente();
            String peticion = DireccionApi + "api/CuentaBancaria";
            var respuesta = await client.PostAsync(peticion, content);
            return respuesta.IsSuccessStatusCode;
        }

        public async Task<bool> ModificarCuenta(CuentaBancaria cuenta)
        {
            string jsondoctor = JsonConvert.SerializeObject(cuenta, Formatting.Indented);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(jsondoctor);
            ByteArrayContent content = new ByteArrayContent(buffer);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient client = CrearCliente();
            String peticion = DireccionApi + "api/CuentaBancaria/" + cuenta.Cif_Usuario;
            var respuesta = await client.PutAsync(peticion, content);
            return respuesta.IsSuccessStatusCode;

        }
        public async Task<bool> EliminarCuenta(string cif)
        {
            HttpClient client = CrearCliente();
            String peticion = DireccionApi + "api/CuentaBancaria/" + cif;
            var respuesta = await client.DeleteAsync(peticion);
            return respuesta.IsSuccessStatusCode;
        }
    }
}
