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

                                ID = Convert.ToInt32(rd["ID"].ToString()),
                                Nombre = rd["Nombre"].ToString(),
                                Dealer = Convert.ToInt32(rd["Dealer"].ToString()),
                                Alternativo = Convert.ToInt32(rd["Alternativo"].ToString()),
                                Regla1 = Convert.ToDecimal(rd["Regla"].ToString()),
                                NOExecede = Convert.ToDecimal(rd["NOExecede"].ToString()),
                                valmin = Convert.ToDecimal(rd["valmin"].ToString()),
                                valmax = Convert.ToDecimal(rd["valmax"].ToString()),

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
