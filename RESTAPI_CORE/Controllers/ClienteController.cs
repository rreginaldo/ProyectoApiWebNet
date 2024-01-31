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
                var response = new Response<List<Cliente>>(ResponseType.Success, lista);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            { 
                var response = new Response<List<Cliente>>(ResponseType.Error, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
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
                var response = new Response<string>(ResponseType.Success, "agregado");
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                var response = new Response<string>(ResponseType.Error, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
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

                var response = new Response<string>(ResponseType.Success, "editado");
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                var response = new Response<string>(ResponseType.Error, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            } 
        }
    }
}
