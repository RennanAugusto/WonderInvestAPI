using System.Runtime.CompilerServices;

namespace Wonder.Service.Contracts.DTO
{
    public class PostFavoriteDTO
    {
        public int IdFavorite { get; set; }
        private string _idUser;
        public bool Active { get; set; }
        public int IdStock { get; set; }
        
        public void SetUser(string pIdUser)
        {
            this._idUser = pIdUser;
        }

        public string GetUser()
        {
            return this._idUser;
        }
    }
}