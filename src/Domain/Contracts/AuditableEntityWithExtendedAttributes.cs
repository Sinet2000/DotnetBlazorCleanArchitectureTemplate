using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPAAgentura.Domain.Contracts;

public abstract class AuditableEntityWithExtendedAttributes<TId, TEntityId, TEntity, TExtendedAttribute> 
    : AuditableEntity<TEntityId>, IEntityWithExtendedAttributes<TExtendedAttribute>
    where TEntity : IEntity<TEntityId>
{
    public virtual ICollection<TExtendedAttribute> ExtendedAttributes { get; set; }

    public AuditableEntityWithExtendedAttributes()
    {
        ExtendedAttributes = new HashSet<TExtendedAttribute>();
    }
}
