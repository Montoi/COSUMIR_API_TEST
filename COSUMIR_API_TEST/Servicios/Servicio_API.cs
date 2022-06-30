using COSUMIR_API_TEST.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace COSUMIR_API_TEST.Servicios
{
    public class Servicio_API : IServicio_API
    {
      
        private static string _baseUrl;
       

        public Servicio_API() {

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

           
            _baseUrl = builder.GetSection("ApiSetting:baseUrl").Value;
        }

        //USAR REFERENCIAS 
        

        public async Task<List<Book>> Lista() { 
            List<Book> lista = new List<Book>();

           


            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            var response = await cliente.GetAsync("api/Producto/Lista");

            if (response.IsSuccessStatusCode) {

                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_respuesta);
                lista = resultado.lista;
            
            }
          

            return lista;
        }

        public async Task<Book> Obtener(int id)
        {
            Book objeto = new Book();

           


            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            var response = await cliente.GetAsync($"api/v1/Books/{id}");

            if (response.IsSuccessStatusCode)
            {

                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_respuesta);
                objeto = resultado.objeto;
            }

            return objeto;
        }

        public async Task<bool> Guardar(Book objeto)
        {
            bool respuesta = false;

          


            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
          

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("/api/v1/Books/", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> Editar(Book objeto)
        {
            bool respuesta = false;

      


            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);          

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync("api/Producto/Editar/", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool respuesta = false;

           


            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
       


            var response = await cliente.DeleteAsync($"/api/v1/Books/{id}");

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

    }
}
