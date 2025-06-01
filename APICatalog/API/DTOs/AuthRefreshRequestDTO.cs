namespace APICatalog.API.DTOs
{
    public class AuthRefreshRequestDTO
    {
        public string? RefreshToken { get; set; }
        public Guid Identifier { get; set; }
    }
}