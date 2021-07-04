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
    class HelperUsuarioAzure
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

        // LISTAR TODOS LOS Usuarios.
        public async Task<List<Usuario>> GetUsuarios()
        {
            List<Usuario> listadatos = null;
            //CREAMOS LA PETICION 
            String peticion = DireccionApi + "api/Usuarios";
            var uri = new Uri(string.Format(peticion, string.Empty));
            HttpClient client = CrearCliente();
            var respuesta = await client.GetAsync(uri);
            if (respuesta.IsSuccessStatusCode) // Si la respuesta es 200 (ok) es que ya podemos leer el contenido de jason 
            {
                var contenido = await respuesta.Content.ReadAsStringAsync();
                listadatos = JsonConvert.DeserializeObject<List<Usuario>>(contenido);

            }
            return listadatos;
        }

        // BUSCAR AL Usuario POR cif
        public async Task<Usuario> BuscarUsuario(String cif)
        {
            Usuario usuario = null;
            //CREAMOS LA PETICION 
            String peticion = DireccionApi + "api/Usuarios/" + cif;
            var uri = new Uri(string.Format(peticion, string.Empty));
            HttpClient client = CrearCliente();
            var respuesta = await client.GetAsync(uri);
            if (respuesta.IsSuccessStatusCode)
            {
                var contenido = await respuesta.Content.ReadAsStringAsync();
                usuario = JsonConvert.DeserializeObject<Usuario>(contenido);
            }
            return usuario;
        }

        //INSERTAR Usuario
        public async Task<bool> InsertarUsuario(Usuario usuario)
        {
            //CONVERTIMOS EL OBJETO EN JSON 
            string jsondoctor = JsonConvert.SerializeObject(usuario, Formatting.Indented);
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
            String peticion = DireccionApi + "api/Usuarios";
            //REALIZAMOS LA LLAMADA AL API POST ENVIANDO EL CONTENIDO 
            var respuesta = await client.PostAsync(peticion, content);
            return respuesta.IsSuccessStatusCode;
        }


        // MODIFICAR Usuario
        public async Task<bool> ModificarUsuario(Usuario usuario)
        {
            //CONVERTIMOS EL OBJETO EN JSON 
            string jsondoctor = JsonConvert.SerializeObject(usuario, Formatting.Indented);
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
            String peticion = DireccionApi + "api/Usuarios/" + usuario.Cif;
            //REALIZAMOS LA LLAMADA AL API POST ENVIANDO EL CONTENIDO 
            var respuesta = await client.PutAsync(peticion, content);
            return respuesta.IsSuccessStatusCode;
           
        }

        // ELIMINAR Usuario
        public async Task<bool> EliminarUsuarios(string cif)
        {
            //CREAMOS EL CLIENTE 
            HttpClient client = CrearCliente();
            //CREAMOS LA PETICION PUT 
            String peticion = DireccionApi + "api/Usuarios/" + cif;
            //REALIZAMOS LA LLAMADA AL API 
            var respuesta = await client.DeleteAsync(peticion);
            return respuesta.IsSuccessStatusCode;
        }

    }
}
