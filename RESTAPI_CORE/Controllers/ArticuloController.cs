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
    public class ArticuloController : ControllerBase
    {
        private readonly string cadenaSQL;
        public ArticuloController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }

        //REFERENCIAS
        //MODELO
        //SQL

        [HttpGet]
        [Route("ListaArticulo")]
        public IActionResult ListaArticulo(string descripcion)
        {

            List<Articulo> listaArticulo = new List<Articulo>();

            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("listaArticulos", conexion);
                    cmd.Parameters.AddWithValue("DESCRIPCION", descripcion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {

                        while (rd.Read())
                        {

                            listaArticulo.Add(new Articulo
                            {

                                CODIGO = rd["CODIGO"].ToString(),
                                DESCRIPCION = rd["DESCRIPCION"].ToString()
                   
                            });
                        }

                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = listaArticulo });
            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = listaArticulo });

            }
        }
    }
}
