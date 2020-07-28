using System;
using System.Collections.Generic;

namespace OnlineEducation.Entities.Dtos
{
    public class AnswerDto
    {
        public Guid Id { get; set; }
        public AnswerVideoDto AnswerVideo { get; set; }
        public ICollection<AnswerImageDto> AnswerImages { get; set; }
    }
}