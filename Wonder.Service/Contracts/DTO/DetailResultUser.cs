using System.Collections.Generic;

namespace Wonder.Service.Contracts.DTO
{
    public class DetailResultUser
    {
        public bool Success { get; set; }
        public string Detail { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IList<string> Errors { get; set; }

        public DetailResultUser()
        {
            this.Errors = new List<string>();
        }
    }
}