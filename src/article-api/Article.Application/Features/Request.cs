using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Application.Features
{
    public abstract class Request<TResult> : IRequest<TResult>
    {
    }
}
