

using Newtonsoft.Json.Linq;

internal class Program
{
    static readonly HttpClient client = new HttpClient();

    static async Task Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n=== CONTRATOS NFL ===");
            Console.WriteLine("1. Ver todos los contratos");
            Console.WriteLine("0. Salir");
            Console.Write("Elige una opcion: ");

            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    await ObtenerContratos();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opcion no valida.");
                    break;
            }
        }
    }

    static async Task ObtenerContratos()
    {
        try
        {
            var response = await client.GetAsync("http://localhost:5143/api/contratos");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var contratos = JArray.Parse(json);

                foreach (var contrato in contratos)
                {
                    Console.WriteLine($"ID: {contrato["idContrato"]}");
                    Console.WriteLine($"Jugador: {contrato["player"]}");
                    Console.WriteLine($"Equipo: {contrato["team"]}");
                    Console.WriteLine("----------------------");
                }
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error de conexion: " + ex.Message);
        }
    }
}
