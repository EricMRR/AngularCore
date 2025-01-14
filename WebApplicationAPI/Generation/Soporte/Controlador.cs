using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generation.Soporte
{
    public class Controlador
    {
        private Tabla tabla;

        public string nombre { get; set; }
        public string namespaceAPI { get; set; }
        public string namespaceTransporte { get; set; }
        public string namespaceWrapperAPI { get; set; }
        public string namespaceAccesoDatos { get; set; }
        public Controlador(Tabla tabla, string namespaceAPI, string namespaceTransporte, string namespaceWrapperAPI, string namespaceAccesoDatos) {
            this.tabla = tabla;
            this.nombre = tabla.name;
            this.namespaceAPI = namespaceAPI;
            this.namespaceTransporte = namespaceTransporte;
            this.namespaceWrapperAPI = namespaceWrapperAPI;
            this.namespaceAccesoDatos = namespaceAccesoDatos;
        }

        //hay que generar cadenas de texto que signifiquen los ids que se estan actualizando o borrando o lo que sea

        private static Columna GetColumna(Columna[] columnas, Indice indice) {
            foreach (Columna c in columnas) {
                if (c.COLUMN_NAME.Equals(indice.ColumnName)) return c;
            }
            return null;
        }

        private string firmaMetodoGetAll() {
            StringBuilder sb = new StringBuilder();
            foreach(Indice indice in tabla.Indices) {
                Columna columna = GetColumna(tabla.Columnas, indice);
                sb.Append(columna.Tipo.TipoNET.Name + " " + columna.COLUMN_NAME + ", ");
            }
            String res = sb.ToString();
            return res.Length > 0 ? res.Substring(0, res.Length - 2) : res;
        }

        private string whereMetodoGetAll(bool metodo) {
            StringBuilder sb = new StringBuilder();
            sb.Append("x => ");
            foreach (Indice indice in tabla.Indices) {
                Columna columna = GetColumna(tabla.Columnas, indice);
                sb.Append("x." + columna.COLUMN_NAME + " == " + (metodo ? "" : "x.") + columna.COLUMN_NAME + " && ");
            }
            String res = sb.ToString();
            return (res.Length > 0) ? res.Substring(0, res.Length - 4) : res;
        }

        private string whereMetodoDelete() {
            StringBuilder sb = new StringBuilder();
            foreach (Indice indice in tabla.Indices) {
                Columna columna = GetColumna(tabla.Columnas, indice);
                sb.Append(columna.COLUMN_NAME + " = " + columna.COLUMN_NAME + ", ");
            }
            String res = sb.ToString();
            return (res.Length > 0) ? res.Substring(0, res.Length - 2) : res;
        }

        private string whereMetodoUpdate() {
            StringBuilder sb = new StringBuilder();
            foreach (Columna columna in tabla.Columnas) {
                sb.Append(columna.COLUMN_NAME + " = peticionAPI.Data." + columna.COLUMN_NAME + ", ");
            }
            String res = sb.ToString();
            return (res.Length > 0) ? res.Substring(0, res.Length - 2) : res;
        }

        override public string ToString() {
            return @"
using " + namespaceWrapperAPI + @";
using " + namespaceTransporte + @";
using " + namespaceAccesoDatos + @";
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using System.Text.Json.Serialization;

namespace " + namespaceAPI + @"
{
    [ApiController]
    [Route(""[controller]"")]
    [JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.Unspecified)]
    public class " + nombre + @"Controller : ControllerBase
    {
        private readonly ILogger<" + nombre + @"Controller> logger;
        private readonly Contexto contexto;

        public " + nombre + @"Controller(ILogger<" + nombre  + @"Controller> logger, Contexto contexto)
        {
            this.logger = logger;
            this.contexto = contexto;
        }

        [EnableCors]
        [HttpPost]
        [NoPropertyNamingPolicy]
        public RespuestaAPI<" + nombre + @"> Agregar(PeticionAPI<" + nombre + @"> peticionAPI) {
            RespuestaAPI<" + nombre  + @"> r = new RespuestaAPI<" + nombre + @">();
            try
            {
                //Obtencion de datos de sesion... me falta cachar esa parte
                " + nombre + @" d = new " + nombre + @"() { " + whereMetodoUpdate() + @" };
                contexto." + nombre + @"s.Add(d);
                contexto.SaveChangesAsync();

                r.Data = d;
                r.Code = 0;
                r.Mensaje = ""OK"";
            }
            catch (Exception ee)
            {
                r.Code = 100;
                r.Mensaje = ee.Message;
            }
            return r;
        }

        [EnableCors]
        [HttpPut]
        [NoPropertyNamingPolicy]
        public RespuestaAPI<" + nombre + @"> Actualizar(PeticionAPI<" + nombre + @"> peticionAPI)
        {
            RespuestaAPI<" + nombre + @"> r = new RespuestaAPI<" + nombre + @">();
            try
            {
                //Obtencion de datos de sesion... me falta cachar esa parte
                " + nombre + @" d = new " + nombre + @"() { " + whereMetodoUpdate() + @" };
                contexto." + nombre + @"s.Update(d);
                contexto.SaveChangesAsync();

                r.Data = d;
                r.Code = 0;
                r.Mensaje = ""OK"";
            }
            catch (Exception ee)
            {
                r.Code = 100;
                r.Mensaje = ee.Message;
            }
            return r;
        }

        [EnableCors]
        [HttpDelete(""{Id}"")]
        [NoPropertyNamingPolicy]
        public RespuestaAPI<bool> Borrar(" + firmaMetodoGetAll() + @")
        {
            RespuestaAPI<bool> r = new RespuestaAPI<bool>();
            try
            {
                //Obtencion de datos de sesion... me falta cachar esa parte
                " + nombre + @" d = new " + nombre + @"() { " + whereMetodoDelete() + @" };
                contexto." + nombre + @"s.Remove(d);
                contexto.SaveChangesAsync();

                r.Data = true;
                r.Code = 0;
                r.Mensaje = ""OK"";
            }
            catch (Exception ee)
            {
                r.Code = 100;
                r.Mensaje = ee.Message;
            }
            return r;
        }

        [EnableCors]
        [HttpGet(""{Id}"")]
        [NoPropertyNamingPolicy]
        public RespuestaAPI<" + nombre + @"> Obtener(" + firmaMetodoGetAll() + @")
        {
            RespuestaAPI<" + nombre + @"> r = new RespuestaAPI<" + nombre + @">();
            try
            {
                //Obtencion de datos de sesion... me falta cachar esa parte
                r.Data = contexto." + nombre + @"s.Where(" + whereMetodoGetAll(true) + @").FirstOrDefault();
                r.Code = 0;
                r.Mensaje = ""OK"";
            }
            catch (Exception ee)
            {
                r.Code = 100;
                r.Mensaje = ee.Message;
            }
            return r;
        }

        [EnableCors]
        [HttpGet]
        [NoPropertyNamingPolicy]
        public RespuestaAPI<IEnumerable<" + nombre + @">> Listar()
        {
            RespuestaAPI<IEnumerable<" + nombre + @">> r = new RespuestaAPI<IEnumerable<" + nombre + @">>();
            try
            {
                //Obtencion de datos de sesion... me falta cachar esa parte
                r.Data = contexto." + nombre + @"s.Where(" + whereMetodoGetAll(false) + @").ToList();
                r.Code = 0;
                r.Mensaje = ""OK"";
            }
            catch (Exception ee)
            {
                r.Code = 100;
                r.Mensaje = ee.Message;
            }
            return r;
        }
    }
}
";
        }
    }
}
