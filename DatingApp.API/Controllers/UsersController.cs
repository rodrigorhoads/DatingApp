using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DatingApp.API.DTO;
using System.Collections.Generic;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDatingRepository _datingRepository;
        private readonly IMapper _mapper;

        public UsersController(IDatingRepository datingRepository, IMapper mapper)
        {
            _datingRepository = datingRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _datingRepository.GetUsers();

            var ususariosParaRetornar = _mapper.Map<IEnumerable<UserParaListarDTO>>(users);

            return Ok(ususariosParaRetornar);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _datingRepository.GetUser(id);

            var ususarioParaRetornar = _mapper.Map<UserParaDetalhesDTO>(user);

            return Ok(ususarioParaRetornar);
        }
    }
}