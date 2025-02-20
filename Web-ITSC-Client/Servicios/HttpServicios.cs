using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Web_ITSC_BD.Data.Entity;
using static System.Net.WebRequestMethods;

namespace Web_ITSC_Client.Servicios
{
    public class HttpServicios : IHttpServicios
    {
        private readonly HttpClient http;

        public HttpServicios(HttpClient http)
        {
            this.http = http;
        }
        public async Task<HttpRespuesta<O>> Get<O>(string url)
        {
            var response = await http.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var respuesta = await DesSerealizar<O>(response);
                return new HttpRespuesta<O>(respuesta, false, response);
            }
            else
            {
                return new HttpRespuesta<O>(default, true, response);
            }
        }

        public async Task<HttpRespuesta<object>> GetNotasByTurno(string url)
        {
            var respuesta = await http.GetAsync(url);
            return new HttpRespuesta<object>(null, !respuesta.IsSuccessStatusCode, respuesta);
        }

        public async Task<HttpRespuesta<object>> Post<O>(string url, O entidad)  // "O" lo que se envia, "object" lo que recibo.
        {
            var EntSerializada = JsonSerializer.Serialize(entidad);
            var EnviarJSON = new StringContent(EntSerializada, Encoding.UTF8, "application/json");
            var response = await http.PostAsync(url, EnviarJSON);

            if (response.IsSuccessStatusCode)
            {
                var respuesta = await DesSerealizar<object>(response);
                return new HttpRespuesta<object>(respuesta, false, response);
            }
            else
            {
                return new HttpRespuesta<object>(default, true, response);
            }
        }

        public async Task<HttpRespuesta<object>> Put<O>(string url, O entidad)
        {
            var EntSerializada = JsonSerializer.Serialize(entidad);
            var EnviarJSON = new StringContent(EntSerializada, Encoding.UTF8, "application/json");
            var response = await http.PutAsync(url, EnviarJSON);
            if (response.IsSuccessStatusCode)
            {
                return new HttpRespuesta<object>(null, false, response);
            }
            else
            {
                return new HttpRespuesta<object>(default, true, response);
            }
        }
        private async Task<O?> DesSerealizar<O>(HttpResponseMessage response)
        {
            var respuesta = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<O>(respuesta, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        //metodos pais, prov, dpto y localidad
        // Obtener todos los países
        public async Task<List<Pais>> SelectPaisesAsync()
        {
            var response = await http.GetAsync("api/Paises");
            if (response.IsSuccessStatusCode)
            {
                var paises = await response.Content.ReadFromJsonAsync<List<Pais>>();
                return paises ?? new List<Pais>();
            }
            else
            {
                // Manejar el error
                return new List<Pais>();
            }
        }

        // Obtener provincias por país
        // Este es el método que obtienes provincias por el país
        public async Task<List<Provincia>> SelectProvinciasPorPaisAsync(int PaisId)
        {
            var response = await http.GetAsync($"api/Provincias/porPais/{PaisId}");

            if (response.IsSuccessStatusCode)
            {
                var provincias = await response.Content.ReadFromJsonAsync<List<Provincia>>();
                return provincias ?? new List<Provincia>();
            }
            else
            {
                // Manejar el error
                Console.WriteLine($"Error al obtener provincias: {response.StatusCode}");
                return new List<Provincia>();
            }
        }

        // Obtener departamentos por provincia
        public async Task<List<Departamento>> SelectDepartamentosPorProvinciaAsync(int ProvinciaId)
        {
            var response = await http.GetAsync($"api/Departamentos/porProvincia/{ProvinciaId}");
            if (response.IsSuccessStatusCode)
            {
                var departamentos = await response.Content.ReadFromJsonAsync<List<Departamento>>();
                return departamentos ?? new List<Departamento>();
            }
            else
            {
                // Manejar el error
                return new List<Departamento>();
            }
        }

        // Obtener localidades por departamento
        public async Task<List<Localidad>> SelectLocalidadesPorDepartamentoAsync(int DepartamentoId)
        {
            var response = await http.GetAsync($"api/Localidades/porDepartamento/{DepartamentoId}");
            if (response.IsSuccessStatusCode)
            {
                var localidades = await response.Content.ReadFromJsonAsync<List<Localidad>>();
                return localidades ?? new List<Localidad>();
            }
            else
            {
                // Manejar el error
                return new List<Localidad>();
            }
        }

        public async Task<HttpRespuesta<object>> Delete(string url)
        {
            var response = await http.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return new HttpRespuesta<object>(null, false, response);
            }
            else
            {
                return new HttpRespuesta<object>(default, true, response);
            }
        }
    }
}
