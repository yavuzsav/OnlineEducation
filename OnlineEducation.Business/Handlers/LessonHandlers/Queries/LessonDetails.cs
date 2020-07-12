using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnlineEducation.Business.Specifications.LessonSpecifications;
using OnlineEducation.Core.ErrorHelpers;
using OnlineEducation.DataAccess.Interfaces;
using OnlineEducation.Entities.Dtos;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Handlers.LessonHandlers.Queries
{
    public class LessonDetails
    {
        public class Query : IRequest<LessonWithChaptersDto>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, LessonWithChaptersDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<LessonWithChaptersDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var lessonRepository = _unitOfWork.Repository<Lesson>();

                var lessonWithChaptersSpecification = new LessonWithChaptersSpecification(request.Id);
                var lesson = await lessonRepository.GetEntityWithSpecificationAsync(lessonWithChaptersSpecification);

                if (lesson == null) throw new RestException(HttpStatusCode.NotFound);

                return _mapper.Map<Lesson, LessonWithChaptersDto>(lesson);
            }
        }
    }
}
