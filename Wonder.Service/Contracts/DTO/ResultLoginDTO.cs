using System.Collections.Generic;

namespace Wonder.Service.Contracts.DTO
{
    public class ResultLoginDTO
    {
        public bool Success { get; set; }
        public string Detail { get; set; }
        
        public string Token { get; set; }
        
        public string Id { get; set; }
        
        public string FullName { get; set; }
        
        public string Email { get; set; }
        
        public string Phone { get; set; }
        public IList<string> Errors { get; set; }

        public ResultLoginDTO()
        {
            this.Errors = new List<string>();
        }
    }
}