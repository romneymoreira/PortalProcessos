using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalProcessos.Api.PortalContext
{
    public abstract class DomainObject
    {
        /// <summary>
        /// Método para validação de objetos do Domínio.
        /// Implementa a Classe DomainValidator
        /// </summary>
        /// <param name="field">Campo a ser validado</param>
        /// <param name="value">Valor a ser validado</param>
        /// <returns></returns>
        protected DomainValidator Check(string field, object value)
        {
            return new DomainValidator(field, value);
        }

        /// <summary>
        /// Método virtual para validação da entidade (estado válido)
        /// </summary>
        public virtual void Validate() { }
    }
}
