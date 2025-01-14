
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Transporte
{
    [Table("Dispositivo")]
    public class Dispositivo
    {

        [Key] public Int32? Id { get; set; } 
        public Int32? CuentaId { get; set; } 
        public Boolean? Eliminado { get; set; } 
        public DateTime? Modificacion { get; set; } 
        public String? Nombre { get; set; } 

    }
}
