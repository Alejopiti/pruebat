using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PruebaTecnica.Models;

namespace PruebaTecnica.Controllers
{
    public class HomeController : Controller
    {
        
        ApplicationDbContext Entities = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult consultar()
        {
            return View();
        }
        public ActionResult BuscarTipoD(string term)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var resultado = db.tipodocumento.Where(x => x.nombre.Contains(term))
                    .Select(x => x.nombre).Take(5).ToList();
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult CrearContrato(contratoslaborales _contrato, string TipoD)
        {
            var mensaje = new Mensajes();
            //_contrato.idtipodocumento = Entities.tipodocumento
            if (_contrato.salario == null | _contrato.salario <= 0)
            {
                mensaje.Tipo_Confirmacion = false;
                mensaje.Mensaje = "Falta sueldo";
                return Json(mensaje, JsonRequestBehavior.AllowGet);
            }
            else
            {
                try
                {
                    Entities.contratoslaborales.Add(_contrato);
                    Entities.SaveChanges();
                    mensaje.Tipo_Confirmacion = true;
                    mensaje.Mensaje = "Exito al guardar";
                    return Json(mensaje, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {

                    mensaje.Tipo_Confirmacion = false;
                    mensaje.Mensaje = "Error" + ex;
                    return Json(mensaje, JsonRequestBehavior.AllowGet);

                }
            }
        }
        public ActionResult ConsultaD(int Documento)
        {
            var model = new Modelconsulta(); 
            var mensaje = new List<Mensajes>();

            var data = Entities.contratoslaborales.Where(d => d.numerodocumento == Documento).FirstOrDefault();
            try
            {
                if (data == null)
                {
                    mensaje.Add(new Mensajes { Mensaje = "sin resultados no ay ninguna información con el documento ingresado", Tipo_Confirmacion = false });
                    return PartialView("_Mensajes", mensaje);
                }
                model.Cargo = Entities.cargos.Find(data.idcargo).nombre;
                model.NombreCompleto = data.primernombre + " " + data.segundonombre + " " + data.primerapellido + " " + data.segundoapellido;
                model.TipoD = Entities.tipodocumento.Find(data.idtipodocumento).nombre;
                model.NumeroD = data.numerodocumento.ToString();
                model.Ncontrato = data.id.ToString();
                model.salario = data.salario.ToString();
                model.RiesgoL = data.idarl.ToString();
                model.FechaIniC = (DateTime)data.fechainicio;
                model.FechaFinC = (DateTime)data.fechafin;
                return PartialView(model);
            }
            catch (Exception ex)
            {
                mensaje.Add(new Mensajes { Mensaje = "Se genero el siguiente error: " + ex, Tipo_Confirmacion = false });
                return PartialView("_Mensajes", mensaje);
            }

        }
        public ActionResult CalcularP(int Documento)
        {
            var data = Entities.contratoslaborales.Where(d => d.numerodocumento == Documento).FirstOrDefault();
            var model = new ModelNomina();
            var mensaje = new List<Mensajes>();

            try
            {
                if (data == null)
                {
                    mensaje.Add(new Mensajes { Mensaje = "sin resultados no ay ninguna información con el documento ingresado para calcular", Tipo_Confirmacion = false });
                    return PartialView("_Mensajes", mensaje);
                }
                else
                {
                    var otrodescuento = Entities.novedadesnomina.Where(g => g.usuariocreacion == data.usuariocreacion).ToList().LastOrDefault();
                    double salario = double.Parse(data.salario.ToString());
                    double odescuentos;
                    if (otrodescuento == null)
                    {
                        model.OtrosDescuento = 0;
                        odescuentos = 0;
                    }
                    else
                    {
                        odescuentos = double.Parse(otrodescuento.descuentos.ToString());
                        model.OtrosDescuento = odescuentos;

                    }
                    model.Descuento = salario * 0.08;
                    model.Pagar = salario - model.Descuento - odescuentos;
                    model.salario = salario;
                    return PartialView(model);
                }
            }
            catch (Exception ex)
            {
                mensaje.Add(new Mensajes { Mensaje = "Se genero el siguiente error: " + ex, Tipo_Confirmacion = false });
                return PartialView("_Mensajes", mensaje);
            }

        }
        public ActionResult Novedades(int Documento, novedadesnomina _novedadesn)
        {
            var mensaje = new Mensajes();
            if (Documento <= 0 )
            {
                mensaje.Mensaje = "Cual es el documento del colaborador";
                mensaje.Tipo_Confirmacion = false;
                return Json(mensaje);
            }
            else
            {
                var data = Entities.contratoslaborales.Where(d => d.numerodocumento == Documento).FirstOrDefault();
                if (data == null)
                {
                    mensaje.Mensaje = "Error no existe la persona con el documento ingreado";
                    mensaje.Tipo_Confirmacion = false;
                    return Json(mensaje);
                }
                else
                {
                    if (_novedadesn.periodolaborado == "")
                    {
                        mensaje.Mensaje = "Cual es el periodo laborado";
                        mensaje.Tipo_Confirmacion = false;
                        return Json(mensaje);
                    }
                    else if (_novedadesn.horaslaboradas == 0)
                    {
                        mensaje.Mensaje = "Cuantas son las horas laboradas";
                        mensaje.Tipo_Confirmacion = false;
                        return Json(mensaje);
                    }
                    else
                    {
                        _novedadesn.usuariocreacion = data.usuariocreacion;
                        _novedadesn.fechacreacion = DateTime.Now;
                        Entities.novedadesnomina.Add(_novedadesn);
                        Entities.SaveChanges();
                        mensaje.Mensaje = "Se guardo correctamente las novedades de nomina";
                        mensaje.Tipo_Confirmacion = true;
                        return Json(mensaje);
                    }    
                }
            }
        }
        public ActionResult CalcularD(int Documento)
        {
            var model = new ModelCalculo();
            var mensaje = new List<Mensajes>();

            var data = Entities.contratoslaborales.Where(d => d.numerodocumento == Documento).FirstOrDefault();
            try
            {
                if (data == null)
                {
                    mensaje.Add(new Mensajes { Mensaje = "sin resultados no ay ninguna información con el documento ingresado para calcular", Tipo_Confirmacion = false });
                    return PartialView("_Mensajes", mensaje);
                }
                else
                {
                    double salario = double.Parse(data.salario.ToString());
                    model.PorempleadorSalud = salario * 0.125;
                    model.PorempleadorPension = salario * 0.16;
                    model.PortrabajadorSalud = salario * 0.04;
                    model.PortrabajadorPension = salario * 0.04;
                    return PartialView("_CalculosAportesS", model);

                }
            }
            catch (Exception ex)
            {
                mensaje.Add(new Mensajes { Mensaje = "Se genero el siguiente error: " + ex, Tipo_Confirmacion = false });
                return PartialView("_Mensajes", mensaje);
}
        }
    }
    public class Mensajes
    {
        public bool Tipo_Confirmacion { get; set; }
        public string Mensaje { get; set; }
    }
    public class ModelCalculo
    {
        public double PortrabajadorSalud { get; set; }
        public double PortrabajadorPension { get; set; }
        public double PorempleadorSalud { get; set; }
        public double PorempleadorPension { get; set; }
    }
    public class Modelconsulta
    {
       public string TipoD { get; set; }
       public string NumeroD { get; set; }
       public string NombreCompleto { get; set; }
       public string Ncontrato { get; set; }
       public string Cargo { get; set; }
       public string RiesgoL { get; set; }
       public DateTime FechaIniC{ get; set; }
       public DateTime FechaFinC { get; set; }
       public string salario { get; set; }
    }
    public class ModelNomina
    {
        public double Pagar { get; set; }
        public double Descuento { get; set; }
        public double OtrosDescuento { get; set; }
        public double salario { get; set; }
    }
}