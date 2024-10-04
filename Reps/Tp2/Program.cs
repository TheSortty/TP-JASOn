using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TpJson
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // URL de la API
            string url = "https://randomuser.me/api/?results=10";

            // Crear una instancia de HttpClient para realizar la solicitud
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Hacer la solicitud GET a la API y obtener la respuesta como string
                    string jsonResponse = await client.GetStringAsync(url);

                    // Deserializar el JSON en el objeto RandomUserApiResponse
                    RandomUserApiResponse apiResponse = JsonConvert.DeserializeObject<RandomUserApiResponse>(jsonResponse);

                    // Recorrer los usuarios y mostrar los atributos requeridos
                    foreach (var user in apiResponse.Results)
                    {
                        Console.WriteLine($"First Name: {user.Name.First}");
                        Console.WriteLine($"Last Name: {user.Name.Last}");
                        Console.WriteLine($"Username: {user.Login.Username}");
                        Console.WriteLine($"Password: {user.Login.Password}");
                        Console.WriteLine(new string('-', 40));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}