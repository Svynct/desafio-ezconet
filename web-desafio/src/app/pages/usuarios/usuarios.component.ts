import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { UsuarioService } from 'src/app/services/usuario.service';
import { Sexos } from 'src/app/shared/Sexos';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-usuarios',
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.css']
})
export class UsuariosComponent implements OnInit {

  usuarios: any[] = [];
  sexos: any[] = Sexos;
  formulario: FormGroup;

  constructor(
    private fb: FormBuilder,
    private usuarioService: UsuarioService,
    private router: Router
  ) { }

  ngOnInit() {
    this.formulario = this.fb.group({
      nome: [''],
      ativo: ['']
    })

    const usuarioSearch = localStorage.getItem('usuarioSearch');

    if (usuarioSearch !== undefined && usuarioSearch !== null && usuarioSearch !== "") {
      console.log(usuarioSearch);
      localStorage.removeItem('usuarioSearch')
      this.formulario.get('nome').setValue(usuarioSearch);
      this.buscarPorFiltro();
    }
    else {
      this.buscar();
    }
  }

  montarFiltro() {
    let obj = this.formulario.value;
    let ativo = "";

    if (obj?.ativo === "true") ativo = "true";
    else if (obj?.ativo === "false") ativo = "false";

    let filtro = {
      nome: obj?.nome,
      ativo: ativo
    }

    return filtro;
  }

  buscar() {
    this.usuarioService.buscarTodos().subscribe((res: any) => {
      if (res) {
        this.usuarios = res;
        this.usuarios.map(u => {
          let dtNascimentoFormatada = new Date(u.dataNascimento).toLocaleDateString("pt-BR");
          u.dtNascimentoFormatada = dtNascimentoFormatada;
          return u;
        })
      }
    })
  }

  buscarPorFiltro() {
    let filtro = this.montarFiltro();

    this.usuarioService.porFiltro(filtro).subscribe((res: any) => {
      if (res) {
        this.usuarios = res;
        this.usuarios.map(u => {
          let dtNascimentoFormatada = new Date(u.dataNascimento).toLocaleDateString("pt-BR");
          u.dtNascimentoFormatada = dtNascimentoFormatada;
          return u;
        })
      }
    })
  }

  limparFiltro() {
    this.formulario.get('nome').setValue('');
    this.formulario.get('ativo').setValue('');
  }

  removerUsuario(usuario: any) {
    Swal.fire({
      title: `Deseja remover o usuário ${usuario.nome}?`,
      icon: "warning",
      showConfirmButton: true,
      confirmButtonText: "Sim",
      showDenyButton: true,
      denyButtonText: "Não"
    }).then((result) => {
      if (result.isConfirmed) {
        this.usuarioService.deletar(usuario.usuarioId).subscribe((res: any) => {
          if (res) {
            this.usuarios.splice(this.usuarios.indexOf(usuario), 1);
            Swal.fire('Usuário removido com sucesso!', '', 'success')
          }
        },
          err => {
            Swal.fire('Houve um erro ao remover o usuário!', '', 'error')
          })
      }
    })
  }

  editarUsuario(usuario: any) {
    this.router.navigate(['/editar', usuario.usuarioId]);
  }
}
