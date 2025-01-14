using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generation.Soporte
{
    public class MapeoTipo
    {
        public string TipoSQL { get; set; }
        public string TipoAngular { get; set; }
        public Type TipoNET { get; set; }

        private MapeoTipo(string TipoSQL, string TipoAngular, Type TipoNET) {
            this.TipoSQL = TipoSQL;
            this.TipoAngular = TipoAngular;
            this.TipoNET = TipoNET;
        }

        private static MapeoTipo[] mapeos = {
            new MapeoTipo("int", "number", typeof(int))
            , new MapeoTipo("nvarchar", "string", typeof(string))
            , new MapeoTipo("bit", "boolean", typeof(bool))
            , new MapeoTipo("datetime", "Date", typeof(DateTime))
        };

        public static MapeoTipo[] Mapeos { get { return mapeos; } }
    }
}
