
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

internal class Program
{
    static readonly HttpClient client = new HttpClient();

    static async Task Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n=== CONTRATOS NFL ===");
            Console.WriteLine("1. Obtener todos los contratos");
            Console.WriteLine("2. Obtener un contrato por ID");
            Console.WriteLine("0. Salir");
            Console.Write("Elige una opcion: ");

            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    await ObtenerContratos();
                    break;
                case "2":
                    Console.Write("Introduce el ID del contrato: ");
                    var id = Console.ReadLine() ?? "";
                    await ObtenerContratoPorId(id);
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
            HttpResponseMessage response = await client.GetAsync("http://localhost:5143/api/contratos");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var contratos = JsonConvert.DeserializeObject<JArray>(responseBody);

                if (contratos == null) return;

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
                Console.WriteLine("Error al obtener los contratos.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static async Task ObtenerContratoPorId(string id)
    {
        if (!int.TryParse(id, out _))
        {
            Console.WriteLine("El ID debe ser un numero.");
            return;
        }

        try
        {
            HttpResponseMessage response = await client.GetAsync($"http://localhost:5143/api/contratos/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var contrato = JsonConvert.DeserializeObject<JObject>(responseBody);

                if (contrato == null) return;

                Console.WriteLine($"ID: {contrato["idContrato"]}");
                Console.WriteLine($"Jugador: {contrato["player"]}");
                Console.WriteLine($"Equipo: {contrato["team"]}");
                Console.WriteLine($"Posicion: {contrato["position"]}");
                Console.WriteLine($"Activo: {contrato["isActive"]}");
                Console.WriteLine($"Año de firma: {contrato["yearSigned"]}");
                Console.WriteLine($"Años de contrato: {contrato["years"]}");
                Console.WriteLine($"Valor: {contrato["value"]}");
                Console.WriteLine($"Garantizado: {contrato["guaranteed"]}");
                Console.WriteLine($"Fecha de nacimiento: {contrato["dateOfBirth"]}");
                Console.WriteLine($"Altura: {contrato["height"]}");
                Console.WriteLine($"Peso: {contrato["weight"]}");
                Console.WriteLine($"Draft: {contrato["draftOverall"]}");
            }
            else
            {
                Console.WriteLine($"Error al obtener el contrato con ID {id}. Codigo de estado: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
