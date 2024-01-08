namespace Project_Forum.Models
{
    public class ReportDisplayContent
    {
        public int RepordId;

        public int ContentId;

        public string? Content;

        public string? ContentType;

        public string? Reason;

        public DateTime ReportDate;

        public ReportDisplayContent(int repordId, int contentId, string? content, string? contentType, string? reason, DateTime reportDate)
        {
            RepordId = repordId;
            ContentId = contentId;
            Content = content;
            ContentType = contentType;
            Reason = reason;
            ReportDate = reportDate;
        }
    }
}
