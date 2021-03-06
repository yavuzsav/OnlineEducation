﻿using System;
using OnlineEducation.DataAccess.Specifications;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Specifications.ChapterSpecifications
{
    public class ChapterWithChapterVideosAndLessonSpecification : BaseSpecification<Chapter>
    {
        public ChapterWithChapterVideosAndLessonSpecification(Guid chapterId) : base(
            x => x.Id == chapterId)
        {
            AddInclude(x => x.ChapterVideos);
            AddInclude(x => x.Lesson);
        }
    }
}
