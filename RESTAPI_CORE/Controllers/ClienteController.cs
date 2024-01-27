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
    public class ClienteController : ControllerBase
    {
        private readonly string cadenaSQL;
        public ClienteController(IConfiguration config)
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

            List<Cliente> lista = new List<Cliente>();

            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("listaClientes", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {

                        while (rd.Read())
                        {

                            lista.Add(new Cliente
                            {

                                CODIGO = rd["CODIGO"].ToString(),
                                RAZONSOCIAL = rd["RAZONSOCIAL"].ToString(),
                                DIRECCION = rd["DIRECCION"].ToString()


                            });
                        }

                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = lista });

            }
        }


        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Cliente objeto)
        {
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("insertMAECLI", conexion);
                    cmd.Parameters.AddWithValue("ccodcli", objeto.CODIGO);
                    cmd.Parameters.AddWithValue("cnomcli", objeto.RAZONSOCIAL);
                    cmd.Parameters.AddWithValue("cdircli", objeto.DIRECCION);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "agregado" });
            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });

            }
        }


        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Cliente objeto)
        {
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("updateMAECLI", conexion);
                    cmd.Parameters.AddWithValue("ccodcli", objeto.CODIGO is null ? DBNull.Value : objeto.CODIGO);
                    cmd.Parameters.AddWithValue("cnomcli", objeto.RAZONSOCIAL is null ? DBNull.Value : objeto.RAZONSOCIAL);
                    cmd.Parameters.AddWithValue("cdircli", objeto.DIRECCION is null ? DBNull.Value : objeto.DIRECCION);              
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "editado" });
            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });

            }
        }
    }
}
