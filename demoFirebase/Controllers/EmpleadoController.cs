using Microsoft.AspNetCore.Mvc;
using Firebase.Auth;
using Firebase.Storage;
using System.Data.SqlClient;
using System.Data;
using demoFirebase.Models;

namespace demoFirebase.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly string  conexion;

        #region Constructor
        public EmpleadoController(IConfiguration config)
        {
            conexion = config.GetConnectionString("cadenaSQL");
        }
        #endregion




        #region Index 
        public IActionResult Index()
        {
            var listaEmpleados = new List<Empleado>();

            try
            {
                using (var con = new SqlConnection(conexion))
                {
                    con.Open();
                    var cmd = new SqlCommand("Listar", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listaEmpleados.Add(new Empleado()
                            {
                                IdEmpleado = (int)dr["IdEmpleado"],
                                Nombre = dr["Nombre"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                URLimagen = dr["URLimagen"].ToString()

                            });
                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

            return View(listaEmpleados);
        }
        #endregion




        #region Crear, redireccionar y crear
        public IActionResult Crear()
        {
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> Crear(Empleado OEmpleado, IFormFile imagen)
        {
            try
            {
                string nombreArchivo = string.Empty;
                if (imagen != null)
                {
                    Stream image = imagen.OpenReadStream();
                    OEmpleado.URLimagen = await FirebaseStore.SubirStorage(image, imagen.FileName);
                }
                else
                {
                    nombreArchivo = OEmpleado.NombreImagen;
                }


                using (var con = new SqlConnection(conexion))
                {
                    con.Open();
                    var cmd = new SqlCommand("Guardar", con);
                    cmd.Parameters.AddWithValue("Nombre", OEmpleado.Nombre);
                    cmd.Parameters.AddWithValue("Telefono", OEmpleado.Telefono);
                    cmd.Parameters.AddWithValue("URLimagen", OEmpleado.URLimagen);
                    cmd.Parameters.AddWithValue("NombreImagen", nombreArchivo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return RedirectToAction("Index");
        }
        #endregion




        #region Editar redirecioanr y editar
        public IActionResult Editar(int id)
        {
            var empleado = new Empleado();
            try
            {
                using (var con = new SqlConnection(conexion))
                {
                    con.Open();
                    var cmd = new SqlCommand("Select * from EMPLEADO WHERE IdEmpleado = @id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            empleado.IdEmpleado = (int)dr["IdEmpleado"];
                            empleado.Nombre = dr["Nombre"].ToString();
                            empleado.Telefono = dr["Telefono"].ToString();
                            empleado.URLimagen = dr["URLimagen"].ToString();
                            empleado.NombreImagen = dr["NombreImagen"].ToString();
                        };
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

            return View(empleado);
        }




        [HttpPost]
        public async Task<IActionResult> Editar(Empleado empleado, IFormFile imagen)
        {
            string nombreArchivo = string.Empty;
            try
            {
                if (imagen != null)
                {
                    FirebaseStore.EliminarStorage(empleado.NombreImagen);
                    Stream imgStream = imagen.OpenReadStream();
                    empleado.URLimagen = await FirebaseStore.SubirStorage(imgStream, imagen.FileName);
                    nombreArchivo = imagen.FileName;
                }
                else
                {
                    nombreArchivo = empleado.NombreImagen;
                }

                using (var con = new SqlConnection(conexion))
                {
                    con.Open();
                    var cmd = new SqlCommand("Actualizar", con);
                    cmd.Parameters.AddWithValue("Id", empleado.IdEmpleado);
                    cmd.Parameters.AddWithValue("Nombre", empleado.Nombre);
                    cmd.Parameters.AddWithValue("Telefono", empleado.Telefono);
                    cmd.Parameters.AddWithValue("URLimagen", empleado.URLimagen);
                    cmd.Parameters.AddWithValue("NombreImagen", nombreArchivo);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction("Index");


        }
        #endregion




        #region eliminar redireccionar y eliminar
        public IActionResult Eliminar(int id)
        {

            var empleado = new Empleado();
            try
            {
                using (var con = new SqlConnection(conexion))
                {
                    con.Open();
                    var cmd = new SqlCommand("Select * from EMPLEADO WHERE IdEmpleado = @id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            empleado.IdEmpleado = (int)dr["IdEmpleado"];
                            empleado.Nombre = dr["Nombre"].ToString();
                            empleado.Telefono = dr["Telefono"].ToString();
                            empleado.URLimagen = dr["URLimagen"].ToString();
                            empleado.NombreImagen = dr["NombreImagen"].ToString();
                        };
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

            return View(empleado);
        }




        [HttpPost]
        public IActionResult EliminarDefinitivo(int id, string nombreImagen)
        {
            try
            {
                using (var con = new SqlConnection(conexion))
                {
                    FirebaseStore.EliminarStorage(nombreImagen);

                    con.Open();
                    var cmd = new SqlCommand("DELETE FROM EMPLEADO WHERE IdEmpleado = @id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction("index");
        }
        #endregion

    }
}
