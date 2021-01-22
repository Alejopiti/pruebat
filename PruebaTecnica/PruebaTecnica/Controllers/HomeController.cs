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
        pruebatecnicaEntities Entities = new pruebatecnicaEntities();
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
        public ActionResult CrearContrato(contratoslaborales _contrato)
        {
            var mensaje = new Mensajes();
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
            var data = Entities.contratoslaborales.Where(d => d.numerodocumento == Documento).FirstOrDefault();
            model.Cargo = Entities.cargos.Find(data.idcargo).nombre;
            model.NombreCompleto = data.primernombre +data.segundonombre + data.primerapellido + data.segundoapellido;
            return PartialView(model);
        }

    }
    public class Mensajes
    {
        public bool Tipo_Confirmacion { get; set; }
        public string Mensaje { get; set; }
    }
    public class Modelconsulta
    {
       public string TipoD { get; set; }
       public int NumeroD { get; set; }
       public string NombreCompleto { get; set; }
       public string Ncontrato { get; set; }
       public string Cargo { get; set; }
       public string RiesgoL { get; set; }
       public DateTime FechaIniC{ get; set; }
       public DateTime FechaFinC { get; set; }
       public int salario { get; set; }
    }
}