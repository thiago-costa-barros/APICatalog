using System.Text.Json;

namespace APICatalog.Models
{
    public class ErrorDetails
    {
        public bool Success { get; set; } = false;
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? StackTrace { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
