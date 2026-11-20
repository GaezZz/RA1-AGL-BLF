//El controlador lo pongo para luego poder usarlo con las aplicaciones, para poder hacer el crud y eso


using Microsoft.AspNetCore.Mvc;
using NFL.API.Models; //Usamos la clase de los modelos

namespace NFL.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //la ruta de acceso, mira por ejemplo para ver las cosas sera desde api/contratos
    public class ContratosController : ControllerBase //Con controllerBase pongo funciones para responder peticiones http
    {

        [HttpGet] //atendemos las peticiones por get
        public ActionResult<List<Contrato>> ObtenerContratos()
        {
            return Ok(contratos);  //lo usamos con GET /api/contratos
        }


        [HttpGet("{id}")] //otro metodo pero para escoger solo un elemento, con esto luego en las vistas podemos elegir al darle solo una columna y que saquemos solo los datos de ese id
        public ActionResult<Contrato> ObtenerContrato(int id)
        {
            var jugadorId = contratos.FirstOrDefault(c => c.IdContrato == id); //busco el jugador con el id pasado

           
            if (jugadorId == null)
            {
                return NotFound();
            }

            //devolver el jugador con el id que le pasamos
            return Ok(jugadorId);
        }


        //para crear nuevos
        [HttpPost]
        public ActionResult<Contrato> CrearContrato(Contrato nuevoContrato)
        {
            //ccrearel nuevo ID
            nuevoContrato.IdContrato = contratos.Max(c => c.IdContrato) + 1;

            //los nuevos datos los incluyo en la lista
            contratos.Add(nuevoContrato);

            
            return Ok(nuevoContrato);
        }

        //para modificar valores del jugador
        [HttpPut("{id}")]
        public ActionResult<Contrato> ActualizarContrato(int id, Contrato contratoActualizado)
        {
            //primero se busca que id cambiamos
            var contrato = contratos.FirstOrDefault(c => c.IdContrato == id);

            
            if (contrato == null)
            {
                return NotFound();
            }

            //cambio los cambios pasados
            contrato.Player = contratoActualizado.Player;
            contrato.Team = contratoActualizado.Team;
            contrato.Position = contratoActualizado.Position;
            contrato.IsActive = contratoActualizado.IsActive;
            contrato.YearSigned = contratoActualizado.YearSigned;
            contrato.Years = contratoActualizado.Years;
            contrato.Value = contratoActualizado.Value;
            contrato.Guaranteed = contratoActualizado.Guaranteed;
            contrato.DateOfBirth = contratoActualizado.DateOfBirth;
            contrato.Height = contratoActualizado.Height;
            contrato.Weight = contratoActualizado.Weight;
            contrato.DraftOverall = contratoActualizado.DraftOverall;

            
            return Ok(contrato);
        }

        //para borrar un jugador por id
        [HttpDelete("{id}")]
        public ActionResult EliminarContrato(int id)
        {
            var contrato = contratos.FirstOrDefault(c => c.IdContrato == id);

            if (contrato == null)
            {
                return NotFound();
            }

            contratos.Remove(contrato); //se borra de la lista temporal
            return NoContent(); //luego devuelvo un codigo de exito para decir que se ha borrado ese jugador
        }






        private static List<Contrato> contratos = new List<Contrato>  //para hacer las pruebas he creado estos registros, todavia no he metido lo del mysql ni nada
{
    new Contrato
    {
        IdContrato = 1,
        Player = "Patrick Mahomes",
        Team = "Kansas City Chiefs",
        Position = "QB",
        IsActive = true,
        YearSigned = 2020,
        Years = 10,
        Value = 450000000,
        Guaranteed = 141000000,
        DateOfBirth = new DateTime(1995, 9, 17),
        Height = 1.91m,
        Weight = 102,
        DraftOverall = 10
    },
    new Contrato
    {
        IdContrato = 2,
        Player = "Josh Allen",
        Team = "Buffalo Bills",
        Position = "QB",
        IsActive = true,
        YearSigned = 2021,
        Years = 6,
        Value = 258000000,
        Guaranteed = 150000000,
        DateOfBirth = new DateTime(1996, 5, 21),
        Height = 1.96m,
        Weight = 108,
        DraftOverall = 7
    }
};

    }
}
