namespace APICatalog.API.DTOs
{
    public class TokenReponseDTO
    {
        public string? Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
