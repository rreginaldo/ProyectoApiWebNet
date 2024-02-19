﻿using Microsoft.AspNetCore.Cors;
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

                var response = new Response<List<CotizacionBuscar>>(ResponseType.Success, lista);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                var response = new Response<List<CotizacionBuscar>>(ResponseType.Error, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
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
                                ID = Convert.ToInt32(rd["ID"]),
                                idcotizacion = Convert.ToInt32(rd["idcotizacion"]),
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
                                ObsP4 = rd["ObsP4"].ToString(),

                                KVVenta = Convert.ToDouble(rd["KVVenta"]),
                                KCVenta = rd["KCVenta"].ToString(),
                                KFVenta = rd["KFVenta"].ToString(),

                                KVCotizacion = Convert.ToDouble(rd["KVCotizacion"]),
                                KCCotizacion = rd["KCCotizacion"].ToString(),
                                KFCotizacion = rd["KFCotizacion"].ToString(),

                                KVNotaIngreso = Convert.ToDouble(rd["KVNotaIngreso"]),
                                KCNotaIngreso = rd["KCNotaIngreso"].ToString(),
                                KFNotaIngreso = rd["KFNotaIngreso"].ToString()

                            });
                        }

                    }
                }

                var response = new Response<List<Cotizacion>>(ResponseType.Success, lista);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                var response = new Response<List<Cotizacion>>(ResponseType.Error, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
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

                var response = new Response<List<KardexCoti>>(ResponseType.Success, lista);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                var response = new Response<List<KardexCoti>>(ResponseType.Error, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
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
                var response = new Response<List<KardexCoti>>(ResponseType.Success, lista);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                var response = new Response<List<KardexCoti>>(ResponseType.Error, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
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
                var response = new Response<List<KardexCoti>>(ResponseType.Success, lista);
                return StatusCode(StatusCodes.Status200OK, response);

            }
            catch (Exception ex)
            {
                var response = new Response<List<KardexCoti>>(ResponseType.Error, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }


        [HttpGet]
        [Route("SecuenciaCoti")]
        public IActionResult SecuenciaCoti(int codigo)
        {
            int Secuencia = 0;

            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("TraerPreCoti", conexion);
                    cmd.Parameters.AddWithValue("CODIGO", codigo);

                    cmd.CommandType = CommandType.StoredProcedure;

                    Secuencia = int.Parse(cmd.ExecuteScalar().ToString());
                 
                }
        
                var response = new Response<int>(ResponseType.Success, Secuencia);
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
        public IActionResult Guardar([FromBody] Cotizacion objeto)
        {
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("Comparativo_CotizacionInsert", conexion);
                    cmd.Parameters.AddWithValue("IDcotizacion", objeto.idcotizacion);
                    cmd.Parameters.AddWithValue("codcliente", objeto.codcliente);
                    cmd.Parameters.AddWithValue("nombrecliente", objeto.Cliente);
                    cmd.Parameters.AddWithValue("dircliente", objeto.dircliente);
                    cmd.Parameters.AddWithValue("Moneda", objeto.Moneda);
                    cmd.Parameters.AddWithValue("item", objeto.item);
                    cmd.Parameters.AddWithValue("cant", objeto.cant);
                    cmd.Parameters.AddWithValue("Cod", objeto.Cod);
                    cmd.Parameters.AddWithValue("NomArticulo", objeto.NomArticulo);
                    cmd.Parameters.AddWithValue("fecha", objeto.fecha);
                    cmd.Parameters.AddWithValue("Precio", objeto.Precio);
                    cmd.Parameters.AddWithValue("MarcaPrecio", objeto.MarcaPrecio);

                    cmd.Parameters.AddWithValue("RUCD", objeto.RUCD); 
                    cmd.Parameters.AddWithValue("NomDealer", objeto.NomDealer);
                    cmd.Parameters.AddWithValue("FecD", objeto.fecD);
                    cmd.Parameters.AddWithValue("PrecioD", objeto.PrecioD);
                    cmd.Parameters.AddWithValue("MarcaD", objeto.MarcaD);
                    cmd.Parameters.AddWithValue("ObsD", objeto.ObsD);

                    cmd.Parameters.AddWithValue("RUCP1", objeto.RUCP1);
                    cmd.Parameters.AddWithValue("NomP1", objeto.NomP1);
                    cmd.Parameters.AddWithValue("FecP1", objeto.FecP1);
                    cmd.Parameters.AddWithValue("PrecioP1", objeto.PrecioP1);
                    cmd.Parameters.AddWithValue("MarcaP1", objeto.MarcaP1);
                    cmd.Parameters.AddWithValue("ObsP1", objeto.ObsP1);

                    cmd.Parameters.AddWithValue("RUCP2", objeto.RUCP2);
                    cmd.Parameters.AddWithValue("NomP2", objeto.NomP2);
                    cmd.Parameters.AddWithValue("FecP2 ", objeto.FecP2);
                    cmd.Parameters.AddWithValue("PrecioP2", objeto.PrecioP2);
                    cmd.Parameters.AddWithValue("MarcaP2", objeto.MarcaP2);
                    cmd.Parameters.AddWithValue("ObsP2", objeto.ObsP2);

                    cmd.Parameters.AddWithValue("RUCP3", objeto.RUCP3);
                    cmd.Parameters.AddWithValue("NomP3", objeto.NomP3);
                    cmd.Parameters.AddWithValue("FecP3 ", objeto.FecP3);
                    cmd.Parameters.AddWithValue("PrecioP3", objeto.PrecioP3);
                    cmd.Parameters.AddWithValue("MarcaP3", objeto.MarcaP3);
                    cmd.Parameters.AddWithValue("ObsP3", objeto.ObsP3);

                    cmd.Parameters.AddWithValue("RUCP4", objeto.RUCP4);
                    cmd.Parameters.AddWithValue("NomP4", objeto.NomP4);
                    cmd.Parameters.AddWithValue("FecP4 ", objeto.FecP4);
                    cmd.Parameters.AddWithValue("PrecioP4", objeto.PrecioP4);
                    cmd.Parameters.AddWithValue("MarcaP4", objeto.MarcaP4);
                    cmd.Parameters.AddWithValue("ObsP4", objeto.ObsP4);

                    cmd.Parameters.AddWithValue("usuario", objeto.usuario);

                    cmd.Parameters.AddWithValue("valorKVenta ", objeto.KVVenta);
                    cmd.Parameters.AddWithValue("clienteKVenta", objeto.KCVenta);
                    cmd.Parameters.AddWithValue("fecKVenta", objeto.KFVenta);

                    cmd.Parameters.AddWithValue("valorKCotizacion ", objeto.KVCotizacion);
                    cmd.Parameters.AddWithValue("clienteKCotizacion", objeto.KCCotizacion);
                    cmd.Parameters.AddWithValue("fecKCotizacion", objeto.KFCotizacion);

                    cmd.Parameters.AddWithValue("valorKNotaIngreso ", objeto.KVNotaIngreso);
                    cmd.Parameters.AddWithValue("clienteKNotaIngreso", objeto.KCNotaIngreso);
                    cmd.Parameters.AddWithValue("fecKNotaIngreso", objeto.KFNotaIngreso);



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
    }
}
