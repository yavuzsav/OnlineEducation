namespace OnlineEducation.Core.ErrorHelpers
{
    public static class ExceptionMessages
    {
        public static string NotFound { get; set; } = "Not Fount";
        public static string ProblemSavingChanges { get; set; } = "Problem saving changes";
        public static string ProblemDeletingVideo { get; set; } = "Problem deleting the video";
        public static string VideoUploadError { get; set; } = "Video Upload Error";
        public static string VideoDeleteError { get; set; } = "Video Delete Error";
        public static string ImageUploadError { get; set; } = "Image Upload Error";
        public static string ImageDeleteError { get; set; } = "Image Delete Error";
    }
}
