using Demo.AS.Util.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.AS.Util.ComplexType
{
    [ComplexType()]
    public class Telefone
    {
        public int DDI { get; set; }

        public int DDD { get; set; }

        [MaxLength(10)]
        public string Numero { get; set; }

        [MaxLength(10)]
        public string Ramal { get; set; }

        public ETipoTelefone Tipo { get; set; }
    }
}
