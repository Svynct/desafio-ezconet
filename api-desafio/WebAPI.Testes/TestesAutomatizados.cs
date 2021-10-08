using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using WebAPI.Application.Services.Implementation;
using WebAPI.Domain.Models;
using WebAPI.Repository.Implementation;
using Xunit;

namespace WebAPI.Testes
{
    public class TestesAutomatizados
    {
        private readonly UsuarioService usuarioService;

        public TestesAutomatizados()
        {
            var repository = new MockRepository(MockBehavior.Strict) { DefaultValue = DefaultValue.Mock };
            var mockFoo = repository.Create<UsuarioRepository>();
            usuarioService = repository.Create<UsuarioService>(mockFoo.Object).Object;
        }

        [Fact]
        public void CriarUsuarioDeveRetornarMaiorQueZeroENaoDeveEstourarExcecao()
        {
            Exception exception = null;

            var usuario = new Usuario
            {
                Nome = "Teste",
                Ativo = true,
                DataNascimento = DateTime.Now,
                Email = "teste@teste.com",
                Senha = "1234",
                SexoId = 1,
            };

            int rowsAffected = 0;

            try
            {
                rowsAffected = usuarioService.CadastrarUsuario(usuario);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            exception.ShouldBeNull();
            rowsAffected.ShouldBeGreaterThan(0);
        }

        [Fact]
        public void RecuperarUsuariosDeveRecuperarUsuariosENaoDeveEstourarExcecao()
        {
            Exception exception = null;
            List<UsuarioViewModel> usuarios = new List<UsuarioViewModel>();

            var usuario = new Usuario
            {
                Nome = "Teste",
                Ativo = true,
                DataNascimento = DateTime.Now,
                Email = "teste@teste.com",
                Senha = "1234",
                SexoId = 1,
            };

            usuarioService.CadastrarUsuario(usuario);

            try
            {
                usuarios = usuarioService.RecuperarTodosUsuarios();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            exception.ShouldBeNull();
            usuarios.ShouldNotBeEmpty();
        }

        [Fact]
        public void DeletarUsuarioDeveRetornarMaiorQueZeroENaoDeveEstourarExcecao()
        {
            Exception exception = null;
            int rowsAffected = 0;

            var usuario = new Usuario
            {
                Nome = "Teste",
                Ativo = true,
                DataNascimento = DateTime.Now,
                Email = "teste@teste.com",
                Senha = "1234",
                SexoId = 1,
            };

            usuarioService.CadastrarUsuario(usuario);

            try
            {
                var u = usuarioService.RecuperarTodosUsuarios()[0];
                rowsAffected = usuarioService.RemoverUsuario(u.UsuarioId);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            exception.ShouldBeNull();
            rowsAffected.ShouldBeGreaterThan(0);
        }
    }
}
