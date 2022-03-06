using Article.Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Article.Application.Features
{
    public abstract class Handler<T,TRequest,TResult> : IRequestHandler<TRequest,TResult>
        where TRequest : IRequest<TResult>
        where T : Entity
    {
        protected readonly IGenericRepositoryAsync<T> _repository;
        protected readonly IUnitOfWork _unitOfWork;
        internal readonly IMapper _mapper;
        public Handler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.Repository<T>();
            _mapper = mapper;
        }

        public abstract Task<TResult> Handle(TRequest request, CancellationToken cancellationToken);
        
    }
}
