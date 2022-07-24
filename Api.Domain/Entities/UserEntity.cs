namespace Api.Domain.Entities
{
    public class UserEntity : BaseEntity 
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Instituicao { get; set; }
        public string TipoUsuario { get; set; }
    }
}