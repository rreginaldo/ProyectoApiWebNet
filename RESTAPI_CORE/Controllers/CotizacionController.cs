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
    public class CotizacionController : ControllerBase
    {
        private readonly string cadenaSQL;
        public CotizacionController(IConfiguration config)
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
            List<CotizacionBuscar> lista = new List<CotizacionBuscar>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("listarBUsquedaCoti", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new CotizacionBuscar
                            {
                                CotiSofkit = Convert.ToInt32(rd["CotiSofkit"]),
                                fecha = rd["fecha"].ToString(),
                                Cliente = rd["Cliente"].ToString(),
                                CotiStarFoft = rd["CotiStarFoft"].ToString()                           
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

        [HttpGet]
        //[Route("Obtener")] // => Obtener?idProducto=13 | ampersand
        [Route("Obtener/{idcotizacion:int}")]
        public IActionResult Obtener(int idcotizacion)
        {
            List<Cotizacion> lista = new List<Cotizacion>();
            //Comparativo oComparativo = new Comparativo();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarCotizacionSoftkit", conexion);
                    cmd.Parameters.AddWithValue("idcotizacion", idcotizacion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {                    
                        while (rd.Read())
                        {
                            lista.Add(new Cotizacion
                            {
                                codcliente = rd["codcliente"].ToString(),
                                Cliente = rd["nombrecliente"].ToString(),
                                dircliente = rd["dircliente"].ToString(),
                                item = rd["item"].ToString(),
                                cant = Convert.ToInt32(rd["cant"]),
                                Cod = rd["Cod"].ToString(),
                                NomArticulo = rd["NomArticulo"].ToString(),
                                fecha = rd["fecha"].ToString(),
                                Moneda = rd["Moneda"].ToString(),
                                Precio = Convert.ToDouble(rd["Precio"]),
                                MarcaPrecio = rd["MarcaPrecio"].ToString(),

                                RUCD = rd["RUCD"].ToString(),
                                NomDealer = rd["NomDealer"].ToString(),
                                fecD = rd["fecD"].ToString(),
                                PrecioD = Convert.ToDouble(rd["PrecioD"]),
                                MarcaD = rd["MarcaD"].ToString(),
                                ObsD = rd["ObsD"].ToString(),

                                RUCP1 = rd["RUCP1"].ToString(),
                                NomP1 = rd["NomP1"].ToString(),
                                FecP1 = rd["FecP1"].ToString(),
                                PrecioP1 = Convert.ToDouble(rd["PrecioP1"]),
                                MarcaP1 = rd["MarcaP1"].ToString(),
                                ObsP1 = rd["ObsP1"].ToString(),

                                RUCP2 = rd["RUCP2"].ToString(),
                                NomP2 = rd["NomP2"].ToString(),
                                FecP2 = rd["FecP2"].ToString(),
                                PrecioP2 = Convert.ToDouble(rd["PrecioP2"]),
                                MarcaP2 = rd["MarcaP2"].ToString(),
                                ObsP2 = rd["ObsP2"].ToString(),

                                RUCP3 = rd["RUCP3"].ToString(),
                                NomP3 = rd["NomP3"].ToString(),
                                FecP3 = rd["FecP3"].ToString(),
                                PrecioP3 = Convert.ToDouble(rd["PrecioP3"]),
                                MarcaP3 = rd["MarcaP3"].ToString(),
                                ObsP3 = rd["ObsP3"].ToString(),

                                RUCP4 = rd["RUCP4"].ToString(),
                                NomP4 = rd["NomP4"].ToString(),
                                FecP4 = rd["FecP4"].ToString(),
                                PrecioP4 = Convert.ToDouble(rd["PrecioP4"]),
                                MarcaP4 = rd["MarcaP1"].ToString(),
                                ObsP4 = rd["ObsP4"].ToString()

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

        [HttpGet]
        [Route("KardexCotizacion/")]
        public IActionResult KardexCotizacion(string codigo, string codcliente)
        {
            List<KardexCoti> lista = new List<KardexCoti>();

            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("BuscarKardexCotizaArticulo", conexion);
                    cmd.Parameters.AddWithValue("codigo", codigo);
                    cmd.Parameters.AddWithValue("codcliente", codcliente);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new KardexCoti
                            {
                                FECHA = rd["FECHA"].ToString(),
                                PRECIO = Convert.ToDouble(rd["PRECIO"].ToString()),
                                CLIENTE = rd["CLIENTE"].ToString(),
                                MONEDA = rd["MONEDA"].ToString(),
                                MARCA = rd["MARCA"].ToString(),                          
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


        [HttpGet]
        [Route("KardexVenta/")]
        public IActionResult KardexVenta(string codigo, string codcliente)
        {
            List<KardexCoti> lista = new List<KardexCoti>();

            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("BuscarKardexVentaArticulo", conexion);
                    cmd.Parameters.AddWithValue("codigo", codigo);
                    cmd.Parameters.AddWithValue("codcliente", codcliente);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new KardexCoti
                            {
                                FECHA = rd["FECHA"].ToString(),
                                PRECIO = Convert.ToDouble(rd["PRECIO"].ToString()),
                                CLIENTE = rd["CLIENTE"].ToString(),
                                MONEDA = rd["MONEDA"].ToString(),
                                MARCA = rd["MARCA"].ToString(),
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

        [HttpGet]
        [Route("KardexNotaIngreso/")]
        public IActionResult KardexNotaIngreso(string codigo, string codcliente)
        {
            List<KardexCoti> lista = new List<KardexCoti>();

            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("BuscarKardexNotaIngresoArticulo", conexion);
                    cmd.Parameters.AddWithValue("codigo", codigo);
                    cmd.Parameters.AddWithValue("codcliente", codcliente);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new KardexCoti
                            {
                                FECHA = rd["FECHA"].ToString(),
                                PRECIO = Convert.ToDouble(rd["PRECIO"].ToString()),
                                CLIENTE = rd["CLIENTE"].ToString(),
                                MONEDA = rd["MONEDA"].ToString(),
                                MARCA = rd["MARCA"].ToString(),
                            });
                        }
                    }
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = codigo, response = lista });
            }
        }
    }
}
