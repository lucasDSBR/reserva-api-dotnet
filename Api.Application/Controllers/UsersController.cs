using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UsersController: ControllerBase
    {
        private IUserService _service;
        public UsersController(IUserService service)
        {
            _service = service;
        }
        [Authorize("Bearer")]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState); //400 bad request - solicitação inválida
            }
            try
            {
                return Ok(await _service.GetAll()); //200 requisição bem sucedida.
                 
            }
            catch (ArgumentException ex)
            {
                
                return StatusCode((int) HttpStatusCode.InternalServerError, ex.Message); //500 Internal Error -- Erro interno do servidor.
            }
        }
        [Authorize("Bearer")]
        [HttpGet]
        [Route("{id}", Name="GetWithId")]
        public async Task<ActionResult> Get(Guid id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState); //400 bad request - solicitação inválida
            }

            try
            {
                return Ok(await _service.Get(id)); //200 requisição bem sucedida.
                 
            }
            catch (ArgumentException ex)
            {
                
                return StatusCode((int) HttpStatusCode.InternalServerError, ex.Message); //500 Internal Error -- Erro interno do servidor.
            }

        }
        [Authorize("Bearer")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserEntity user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState); //400 bad request - solicitação inválida
            }
            try
            {
                var result = await _service.Post(user);
                if(result != null){
                    return Created(new Uri(Url.Link("GetWithId", new {id = result.Id})), result); //200 requisição bem sucedida, CRIOU ALGO.
                }else{
                    return BadRequest(ModelState); //400 bad request - solicitação inválida
                }
                    
            }
            catch (ArgumentException ex)
            {
                
                return StatusCode((int) HttpStatusCode.InternalServerError, ex.Message); //500 Internal Error -- Erro interno do servidor.
            }
        }
        [Authorize("Bearer")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UserEntity user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState); //400 bad request - solicitação inválida
            }

            try
            {
                var result = await _service.Put(user);

                if(result != null)
                {
                    return Ok(result);
                }else
                {
                    return BadRequest(ModelState); //400 bad request - solicitação inválidas
                }
                    
            }
            catch (ArgumentException ex)
            {
                
                return StatusCode((int) HttpStatusCode.InternalServerError, ex.Message); //500 Internal Error -- Erro interno do servidor.
            }
            
        }
        [Authorize("Bearer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState); //400 bad request - solicitação inválida
            }

            try
            {
                return Ok(await _service.Delete(id)); //200 requisição bem sucedida.
                 
            }
            catch (ArgumentException ex)
            {
                
                return StatusCode((int) HttpStatusCode.InternalServerError, ex.Message); //500 Internal Error -- Erro interno do servidor.
            }

        }

    }
}