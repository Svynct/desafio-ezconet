using Microsoft.AspNetCore.Mvc;
using System;
using WebAPI.Application.Services.Interfaces;
using WebAPI.Domain.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService usuarioService;

        public UsuarioController(IUsuarioService _usuarioService)
        {
            usuarioService = _usuarioService;
        }

        [HttpGet, Route("")]
        public ActionResult RecuperarTodosUsuarios()
        {
            try
            {
                var usuarios = usuarioService.RecuperarTodosUsuarios();

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet, Route("{id}")]
        public ActionResult RecuperarUsuarioPorId(int id)
        {
            try
            {
                var usuario = usuarioService.RecuperarUsuarioPorId(id);

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet, Route("Filtro")]
        public ActionResult RecuperarUsuarioPorFiltro([FromQuery] FiltroUsuarioModel filtro)
        {
            try
            {
                var usuarios = usuarioService.RecuperarUsuariosPorFiltro(filtro);

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost, Route("")]
        public ActionResult CadastrarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                var rowsAffected = usuarioService.CadastrarUsuario(usuario);

                if (rowsAffected > 0)
                    return Ok(rowsAffected);

                return StatusCode(204, rowsAffected);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete, Route("{id}")]
        public ActionResult RemoverUsuario([FromRoute] int id)
        {
            try
            {
                var rowsAffected = usuarioService.RemoverUsuario(id);

                if (rowsAffected > 0)
                    return StatusCode(201, rowsAffected);

                return Ok(rowsAffected);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut, Route("")]
        public ActionResult EditarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                var rowsAffected = usuarioService.EditarUsuario(usuario);

                if (rowsAffected > 0)
                    return StatusCode(201, rowsAffected);

                return Ok(rowsAffected);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
