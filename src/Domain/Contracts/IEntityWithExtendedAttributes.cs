using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPAAgentura.Domain.Contracts;

public interface IEntityWithExtendedAttributes<TExtendedAttribute>
{
    public ICollection<TExtendedAttribute> ExtendedAttributes { get; set; }
}
