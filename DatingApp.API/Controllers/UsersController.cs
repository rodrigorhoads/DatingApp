using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DatingApp.API.DTO;
using System.Collections.Generic;
using System.Security.Claims;
using System;
using DatingApp.API.Helpers;

namespace DatingApp.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
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

        /// <summary>
        /// correto com o video
        /// </summary>
        /// <param name="userParams"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]UserParams userParams)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userFromRepo = await _datingRepository.GetUser(currentUserId);

            var users = await _datingRepository.GetUsers(userParams);

            userParams.UserId = currentUserId;

            if (string.IsNullOrEmpty(userParams.Genero))
            {
                userParams.Genero = userFromRepo.Genero == "male" ? "female"  : "male";
            }

            var ususariosParaRetornar = _mapper.Map<IEnumerable<UserParaListarDTO>>(users);

            Response.AddPagination(users.CurrentPage,users.PageSize,users.TotalCount,users.TotalPages);

            return Ok(ususariosParaRetornar);
        }


        
        [HttpGet("{id}", Name ="GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _datingRepository.GetUser(id);

            var ususarioParaRetornar = _mapper.Map<UserParaDetalhesDTO>(user);

            return Ok(ususarioParaRetornar);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserParaUpdatesDTO userParaUpdates)
        {
            if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var userFormRepo = await _datingRepository.GetUser(id);

            _mapper.Map(userParaUpdates, userFormRepo);

            if (await _datingRepository.SaveAll())
                return NoContent();

            throw new Exception($"Atualização do usuario {id} falhou");
        }

    }
}