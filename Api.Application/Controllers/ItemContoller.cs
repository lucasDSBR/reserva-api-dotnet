using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.Item;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Application.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private IItemService _service;
        public ItemController(IItemService service)
        {
            _service = service;
        }
        //[Authorize("Bearer")]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //400 bad request - solicitação inválida
            }
            try
            {
                return Ok(await _service.GetAll()); //200 requisição bem sucedida.

            }
            catch (ArgumentException ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); //500 Internal Error -- Erro interno do servidor.
            }
        }
        [Authorize("Bearer")]
        [HttpGet]
        [Route("{id}", Name = "GetItemWithId")]
        public async Task<ActionResult> Get(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //400 bad request - solicitação inválida
            }

            try
            {
                return Ok(await _service.Get(id)); //200 requisição bem sucedida.

            }
            catch (ArgumentException ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); //500 Internal Error -- Erro interno do servidor.
            }

        }
        //[Authorize("Bearer")]
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] IFormFile photo, [FromForm] string Item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //400 bad request - solicitação inválida
            }
            try
            {

                if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\temp\\wb\\itens\\images\\"))
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\temp\\wb\\itens\\images\\");

                var ItemOk = JsonConvert.DeserializeObject<ItemEntity>(Item);
                var result = await _service.Post(ItemOk);
                if (result != null)
                {
                    var type = photo.ContentType.Split('/')[1];
                    var filename = photo.FileName.Split('.')[0];
                    var path = "\\temp\\wb\\itens\\images\\" + result.Id + "." + type;
                    var pathUpdate = "/temp/wb/itens/images/" + result.Id + "." + type;
                    using (FileStream filestream = System.IO.File.Create(Directory.GetCurrentDirectory() + path))
                    {
                        await photo.CopyToAsync(filestream);
                        filestream.Flush();
                    }
                    result.PathPhoto = pathUpdate;
                    await _service.Put(ItemOk);

                    return Created(new Uri(Url.Link("GetWithId", new { id = result.Id })), result); //200 requisição bem sucedida, CRIOU ALGO.
                }
                else
                {
                    return BadRequest(ModelState); //400 bad request - solicitação inválida
                }

            }
            catch (ArgumentException ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); //500 Internal Error -- Erro interno do servidor.
            }
        }
        [Authorize("Bearer")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] ItemEntity item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //400 bad request - solicitação inválida
            }

            try
            {
                var result = await _service.Put(item);

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(ModelState); //400 bad request - solicitação inválidas
                }

            }
            catch (ArgumentException ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); //500 Internal Error -- Erro interno do servidor.
            }

        }
        [Authorize("Bearer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //400 bad request - solicitação inválida
            }

            try
            {
                return Ok(await _service.Delete(id)); //200 requisição bem sucedida.

            }
            catch (ArgumentException ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); //500 Internal Error -- Erro interno do servidor.
            }

        }

    }
}