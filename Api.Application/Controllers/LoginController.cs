using System.Runtime.InteropServices;
using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace Api.Application.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class LoginController: ControllerBase
    {
        private ILoginService _service;
        public LoginController(ILoginService service)
        {
            _service = service;  
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<object> Login([FromBody] LoginDto loginDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState); //400 bad request - solicitação inválida

            if(loginDto == null)
                return BadRequest(ModelState); //400 bad request - solicitação

            try
            {
                var result = await _service.FindByLogin(loginDto);
                if(result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, ex.Message); //500 Internal Error -- Erro interno do servidor.
            }
        }
    }
}