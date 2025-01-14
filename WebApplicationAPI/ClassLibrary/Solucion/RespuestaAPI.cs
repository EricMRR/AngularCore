using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Solucion
{
    public class RespuestaAPI<T>
    {
        public int? Code { get; set; }
        public T? Data { get; set; }
        public string? Mensaje { get; set; }


        public RespuestaAPI(){
        }

        public RespuestaAPI(T Data) {
            this.Data = Data;
        }

        public RespuestaAPI(int Code, string Mensaje) {
            this.Code = Code;
            this.Mensaje = Mensaje;
        }

        public RespuestaAPI(T Data, int Code, string Mensaje)
        {
            this.Data = Data;
            this.Code = Code;
            this.Mensaje = Mensaje;
        }

        public RespuestaAPI(Exception ee)
        {

            //hacemos el log del error

            this.Code = ee.HResult;
            this.Mensaje = ee.Message;
        }
    }
}
