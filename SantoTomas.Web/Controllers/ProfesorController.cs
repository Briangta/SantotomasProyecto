using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Web.Http;
using SantoTomas.Repositorio;

namespace SantoTomas.Web.Controllers
{
    public class ProfesorController : ApiController
    {
        SantoTomasEntities db = new SantoTomasEntities();
        public HttpResponseMessage Get()
        {
            var profesores = db.Profesor.Include(r => r.ProfesorMateria).
                Include(r => r.ProfesorMateria.Select(m => m.Materia)).ToList();

            return new HttpResponseMessage
            {
                Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(profesores), 
                Encoding.UTF8, "application/fhir+json"),
                StatusCode = HttpStatusCode.Accepted
            };
        }

        public HttpResponseMessage Get(int id)
        {
            Profesor profesor = db.Profesor.Where(r => r.IdProfesor == id).Include(r => r.ProfesorMateria).Include(r=>r.ProfesorMateria.Select(m=>m.Materia))
            .FirstOrDefault();

            return new HttpResponseMessage
            {
                Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(profesor), 
                Encoding.UTF8, "application/fhir+json"),
                StatusCode = HttpStatusCode.Accepted
            };
        }
    }
}