using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineEducation.DataAccess.Concrete.EntityFramework;
using OnlineEducation.Entities.Entities;
using Bogus;
using OnlineEducation.Entities.Enums;

namespace OnlineEducation.DataAccess.Concrete.SeedData
{
    public class Seed
    {
        public static async Task SeedDataAsync(OnlineEducationContext context)
        {
            var categories = new List<Category>();
            if (!context.Categories.Any())
            {
                categories.AddRange(new List<Category>
                {
                    new Category {Id = Guid.NewGuid(), Name = "9.Sınıf", Description = "9.Sınıf"},
                    new Category {Id = Guid.NewGuid(), Name = "10.Sınıf", Description = "10.Sınıf"},
                    new Category {Id = Guid.NewGuid(), Name = "11.Sınıf", Description = "11.Sınıf"},
                    new Category {Id = Guid.NewGuid(), Name = "12.Sınıf", Description = "12.Sınıf"}
                });
                await context.Categories.AddRangeAsync(categories);
            }

            var lessons = new List<Lesson>();
            if (!context.Lessons.Any())
            {
                lessons.AddRange(new List<Lesson>
                {
                    new Lesson
                    {
                        Id = Guid.NewGuid(), Name = "Matematik", Description = "9.sınıf matematik",
                        Category = categories[0]
                    },
                    new Lesson
                    {
                        Id = Guid.NewGuid(), Name = "Edebiyat", Description = "9.sınıf edebiyat",
                        Category = categories[0]
                    },
                    new Lesson
                    {
                        Id = Guid.NewGuid(), Name = "Coğrafya", Description = "9.sınıf coğrafya",
                        Category = categories[0]
                    },
                    new Lesson
                    {
                        Id = Guid.NewGuid(), Name = "Geometri", Description = "9.sınıf geometri",
                        Category = categories[0]
                    },
                    new Lesson
                    {
                        Id = Guid.NewGuid(), Name = "Fizik", Description = "9.sınıf fizik", Category = categories[0]
                    }
                });

                await context.Lessons.AddRangeAsync(lessons);
            }

            var chapters = new List<Chapter>();
            if (!context.Chapters.Any())
            {
                chapters.AddRange(new List<Chapter>
                {
                    new Chapter
                    {
                        Lesson = lessons[0], Name = "1.ünite", Content = "1.content",
                        Description = "9.sınıf matematik 1.ünite",
                        ChapterVideos = new List<ChapterVideo>
                        {
                            new ChapterVideo
                            {
                                Url =
                                    "https://res.cloudinary.com/drszt4hzs/video/upload/v1594398595/OnlineEducation/ChapterVideos/Istanbul_City_in_4K_si5vwj.mp4",
                                PublicId = "Istanbul_City_in_4K_si5vwj"
                            }
                        }
                    },
                    new Chapter
                    {
                        Lesson = lessons[0], Name = "2.ünite", Content = "2.content",
                        Description = "9.sınıf matematik 2.ünite",
                        ChapterVideos = new List<ChapterVideo>
                        {
                            new ChapterVideo
                            {
                                Url =
                                    "https://res.cloudinary.com/drszt4hzs/video/upload/v1594398595/OnlineEducation/ChapterVideos/Istanbul_City_in_4K_si5vwj.mp4",
                                PublicId = "Istanbul_City_in_4K_si5vwj"
                            }
                        }
                    },
                    new Chapter
                    {
                        Lesson = lessons[0], Name = "3.ünite", Content = "3.content",
                        Description = "9.sınıf matematik 3.ünite",
                        ChapterVideos = new List<ChapterVideo>
                        {
                            new ChapterVideo
                            {
                                Url =
                                    "https://res.cloudinary.com/drszt4hzs/video/upload/v1594398595/OnlineEducation/ChapterVideos/Istanbul_City_in_4K_si5vwj.mp4",
                                PublicId = "Istanbul_City_in_4K_si5vwj"
                            }
                        }
                    }
                });

                await context.Chapters.AddRangeAsync(chapters);
            }

            if (!context.ExamQuestions.Any())
            {
                var faker = new Faker<ExamQuestion>()
                    .RuleFor(x => x.Content, (f, q) => f.Lorem.Paragraphs(1, 4))
                    .RuleFor(x => x.Option1, (f, q) => f.Lorem.Lines(1))
                    .RuleFor(x => x.Option2, (f, q) => f.Lorem.Lines(1))
                    .RuleFor(x => x.Option3, (f, q) => f.Lorem.Lines(1))
                    .RuleFor(x => x.Option4, (f, q) => f.Lorem.Lines(1))
                    .RuleFor(x => x.CorrectAnswer, (f, q) => f.PickRandom<CorrectAnswer>())
                    .RuleFor(x => x.ChapterId, (f, q) => f.PickRandom(chapters).Id)
                    .RuleFor(x => x.VideoAnswerForExamQuestion, (f, q) => new VideoAnswerForExamQuestion
                    {
                        Url = f.Image.PicsumUrl(),
                        PublicId = "fake data",
                    });

                await context.ExamQuestions.AddRangeAsync(faker.Generate(500));
            }

            if (!context.Questions.Any())
            {
                var faker = new Faker<Question>()
                    .RuleFor(x => x.Message, (f, question) => f.Lorem.Lines(1))
                    .RuleFor(x => x.CreatedAt, (f, question) => f.Date.Past(0))
                    .RuleFor(x => x.IsAnswerVideo, (f, question) => f.Random.Bool())
                    .RuleFor(x => x.LessonId, (f, question) => f.PickRandom(context.Lessons?.Select(x => x.Id).ToList()))
                    .RuleFor(x => x.OwnerId, (f, question) => f.PickRandom(context.Users?.Select(x => x.Id).ToList()))
                    .RuleFor(x => x.QuestionImage, (f, question) => new QuestionImage
                    {
                        Url = f.Image.PicsumUrl(),
                        PublicId = "fake data"
                    })
                    .RuleFor(x => x.Answer, (f, question) => new Answer
                    {
                        AnswerImages = new List<AnswerImage>
                        {
                            new AnswerImage
                            {
                                Url = f.Image.PicsumUrl(),
                                PublicId = "fake data",
                                CreatedAt = DateTime.Now,
                            }
                        }
                    });

                await context.Questions.AddRangeAsync(faker.Generate(20));
            }

            await context.SaveChangesAsync();
        }
    }
}
