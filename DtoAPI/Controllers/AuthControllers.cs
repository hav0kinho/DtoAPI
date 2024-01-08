using AutoMapper;
using DtoAPI.Data;
using DtoAPI.Models;
using DtoAPI.Models.DTO;
using DtoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DtoAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthControllers : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public AuthControllers(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet("users")]
        public IActionResult GetUsersTest()
        {
            return Ok(_db.Users.ToList());
        }

        [HttpPost("register")]
        public IActionResult CreateUser(UserDTO user_enviado)
        {
            User novoUser = _mapper.Map<User>(user_enviado);
            novoUser.Roles = new List<string>() { "user" };

            _db.Users.Add(novoUser);
            _db.SaveChanges();

            return Ok(novoUser);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDTO user_enviado)
        {
            User loginUser = _mapper.Map<User>(user_enviado);
            var usuarioRequisitado = _db.Users.Where(x => x.Name == loginUser.Name).FirstOrDefault();

            if (usuarioRequisitado != null)
            {
                if(usuarioRequisitado.Password == loginUser.Password)
                {
                    var token = TokenService.Generate(usuarioRequisitado);
                    return Ok(token);
                } else
                {
                    return BadRequest("Senha Incorreta");
                }
            } 
            else
            {
                return NotFound("Usuário não encontrado");
            }
            
        }
    }
}
