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
    public class DadosBancarios
    {
        [MaxLength(5)]
        public string CodigoBanco { get; set; }

        [MaxLength(30)]
        public string NomeBanco { get; set; }

        [MaxLength(10)]
        public string NumeroAgencia { get; set; }

        [MaxLength(20)]
        public string NumeroConta { get; set; }

        public ETipoContaBancaria TipoConta { get; set; }
    }
}
