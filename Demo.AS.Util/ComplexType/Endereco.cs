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
    public class Endereco
    {
        [MaxLength(200)]
        public string Logradouro { get; set; }

        [MaxLength(50)]
        public string Numero { get; set; }

        [MaxLength(20)]
        public string Complemento { get; set; }

        [MaxLength(10)]
        public string CEP { get; set; }

        [MaxLength(50)]
        public string Bairro { get; set; }

        [MaxLength(100)]
        public string Cidade { get; set; }

        [MaxLength(100)]
        public string Pais { get; set; }

        public EUnidadeFederativa Estado { get; set; }

        [NotMapped()]
        public string SiglaEstado
        {
            get
            {
                return Estado.ToString();
            }
        }
    }
}
