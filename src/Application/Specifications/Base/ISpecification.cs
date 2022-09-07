using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FPAAgentura.Domain.Contracts;

namespace FPAAgentura.Application.Specifications.Base;

public interface ISpecification<T> where T : class, IEntity
{
    Expression<Func<T, bool>> Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    List<string> IncludeStrings { get; }
}
