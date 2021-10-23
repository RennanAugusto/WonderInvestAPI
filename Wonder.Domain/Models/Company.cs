using Wonder.Domain.Shared;

namespace Wonder.Domain.Models
{
    public class Company: Base
    {
        public string Name { get; set; }
        public Acting Acting { get; set; }
        
        public string LogoBase64 { get; set; }
    }
}