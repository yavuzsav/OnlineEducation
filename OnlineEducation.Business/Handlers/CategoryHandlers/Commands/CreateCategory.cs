using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnlineEducation.Core.ErrorHelpers;
using OnlineEducation.DataAccess.Interfaces;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Handlers.CategoryHandlers.Commands
{
    public class CreateCategory
    {
        public class Command : IRequest
        {
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var categoryRepository = _unitOfWork.Repository<Category>();

                if (categoryRepository.ListAllAsync().Result.Any(x =>
                    String.Equals(x.Name, request.Name, StringComparison.CurrentCultureIgnoreCase)))
                    throw new RestException(HttpStatusCode.BadRequest, "Category name already exists");

                var category = new Category
                {
                    Name = request.Name,
                    Description = request.Description
                };

                categoryRepository.Add(category);

                var success = await _unitOfWork.CompleteAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception(ExceptionMessages.ProblemSavingChanges);
            }
        }
    }
}
