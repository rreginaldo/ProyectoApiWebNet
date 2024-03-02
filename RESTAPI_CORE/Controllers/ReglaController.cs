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
    public class ReglaController : ControllerBase

    {
       
        private readonly string cadenaSQL;
        public ReglaController(IConfiguration config)
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
            List<Regla> lista = new List<Regla>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("listaReglas", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Regla
                            {

                                idR = Convert.ToInt32(rd["idR"].ToString()),
                                Nombre = rd["Nombre"].ToString(),
                                signoA = rd["signoA"].ToString(),
                                ValorUnit = Convert.ToInt32(rd["ValorUnit"].ToString()),
                                signoB = rd["signoB"].ToString(),
                                cant = Convert.ToInt32(rd["cant"].ToString()),
                                factor = Convert.ToDecimal(rd["factor"].ToString()),


                            });
                        } 
                    }
                }
                var response = new Response<List<Regla>>(ResponseType.Success, lista);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                var response = new Response<List<Regla>>(ResponseType.Error, ex.Message); 
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
