using Orbital.API.DTOs;

public interface IAuthService
{
    Task<object?> Login(UsuarioLoginDto dto);
    Task<object> Register(UsuarioCreateDto dto);
}