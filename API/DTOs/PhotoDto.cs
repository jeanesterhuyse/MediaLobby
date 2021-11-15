namespace API.DTOs
{
    public class PhotoDto
    {
        public int id { get; set; }
        public int folderId { get; set; }
        public string url { get; set; }
        public bool isMain { get; set; }
    }
}