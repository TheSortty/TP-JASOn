using MySqlConnector;
using Newtonsoft.Json;

public class Program
{
    public static void Main()
    {
        string connectionString = "Server=bemseqjmdfzpnhjhnnkd-mysql.services.clever-cloud.com;Port=3306;Database=bemseqjmdfzpnhjhnnkd;Uid=uvymgx1zcwunagvy;Pwd=ajiCSofHy6DEhNMXOoRl;SslMode=none;";

        List<Escritor> escritores = new List<Escritor>();

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            Console.WriteLine("Conexión exitosa a la base de datos.");

            string query = @"
                SELECT e.id, e.apellido, e.nombre, e.dni, l.nombre AS nombreLibro, l.anioPublicacion, l.editorial
                FROM Escritor e
                INNER JOIN Libro l ON e.id = l.idEscritor"
            ;

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idEscritor = reader.GetInt32(0);

                        var escritor = escritores.Find(e => e.Id == idEscritor);
                        if (escritor == null)
                        {
                            escritor = new Escritor
                            {
                                Id = idEscritor,
                                Apellido = reader.GetString(1),
                                Nombre = reader.GetString(2),
                                Dni = reader.GetString(3),
                                Libros = new List<Libro>()
                            };
                            escritores.Add(escritor);
                        }

                        escritor.Libros.Add(new Libro
                        {
                            Nombre = reader.GetString(4),
                            AnioPublicacion = reader.GetInt32(5),
                            Editorial = reader.GetString(6)
                        });
                    }
                }
            }
        }

        // Serialización a JSON
        string json = JsonConvert.SerializeObject(escritores, Formatting.Indented);

        // Escritura en archivo JSON
        File.WriteAllText("escritores.json", json);

        Console.WriteLine("Datos escritos correctamente en escritores.json");
    }
}