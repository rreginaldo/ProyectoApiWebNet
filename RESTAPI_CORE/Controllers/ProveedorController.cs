using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using RESTAPI_CORE.Modelos;
using System.Data;
using System.Data.SqlClient;

namespace RESTAPI_CORE.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : Controller
    {
        private readonly string cadenaSQL;
        public ProveedorController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }

        //REFERENCIAS
        //MODELO
        //SQL

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {

            List<Proveedor> lista = new List<Proveedor>();

            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("listaProveedores", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {

                        while (rd.Read())
                        {
                            lista.Add(new Proveedor
                            {
                                CODIGO = rd["CODIGO"].ToString(),
                                RAZONSOCIAL = rd["RAZONSOCIAL"].ToString(),
                                DIRECCION = rd["DIRECCION"].ToString()
                            });
                        }
                    }
                }
                var response = new Response<List<Proveedor>>(ResponseType.Success, lista);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                var response = new Response<List<Proveedor>>(ResponseType.Error, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

    }
}
