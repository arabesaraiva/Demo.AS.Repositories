using Demo.AS.Repositories.Core;
using Demo.AS.Util.ComplexType;
using Demo.AS.Util.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.AS.Repositories.Entities
{
    public class Usuario : IPrimaryKey
    {
        public Usuario()
        {
            Telefone= new Telefone();;
            Endereco = new Endereco();
            ContaBancaria= new DadosBancarios();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        
        [MaxLength(11)]
        public string Cpf { get; set; }
        
        [Required]
        public DateTime DataInclusao { get; set; }

        [Required]
        public EStatusUsuario Status { get; set; }

        public Telefone Telefone { get; set; }

        public Endereco Endereco { get; set; }

        public DadosBancarios ContaBancaria{ get; set; }
        
    }
}
