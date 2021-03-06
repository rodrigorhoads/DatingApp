﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data.Repository;
using DatingApp.API.DTO;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository authRepository,
                                IConfiguration configuration,
                                IMapper mapper)
        {
            _authRepository = authRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        /// <summary>
        /// Este metodo usa um DTO data tranfer object para receber os dados da 
        /// requisição 
        /// 
        /// se nao usar o filter[ApiController]
        /// ao usar o from body é necessári usar a ModelState para
        /// checar por errors
        /// </summary>
        /// <param name="username"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(UsuarioParaRegistro model)
        {
            //TODO : criar validação

            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            model.Username = model.Username.ToLower();

            if (await _authRepository.UserExists(model.Username))
            {
                return BadRequest("Esse nome de usuário já está sendo usado");
            }

            var novoUsuario = _mapper.Map<User>(model);
            var usuarioCriado =await _authRepository.Register(novoUsuario,model.Senha);
            var userForReturn = _mapper.Map<UserParaDetalhesDTO>(usuarioCriado);

            return CreatedAtRoute("GetUser", new{ controller = "Users", id = usuarioCriado.Id },userForReturn);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UsuarioParaLogar model)
        {                
            var userExistente = await _authRepository.Login(model.Username.ToLower(),model.Senha);

            if (userExistente == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userExistente.Id.ToString()),
                new Claim(ClaimTypes.Name, userExistente.Nome)
            };

            var key = new SymmetricSecurityKey(Encoding
                .UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var user = _mapper.Map<UserParaListarDTO>(userExistente);

            return Ok(new {
                token = tokenHandler.WriteToken(token),
                user
            }); 

        }
    }
}