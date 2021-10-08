import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UsuarioService } from 'src/app/services/usuario.service';

@Component({
  selector: 'app-cadastrar-usuario',
  templateUrl: './cadastrar-usuario.component.html',
  styleUrls: ['./cadastrar-usuario.component.css']
})
export class CadastrarUsuarioComponent implements OnInit {

  formulario: FormGroup;

  constructor(
    private fb: FormBuilder,
    private usuarioService: UsuarioService
  ) { }

  ngOnInit() {
    this.formulario = this.fb.group({
      nome: ['', Validators.required],
      dataNascimento: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      senha: [''],
      sexoId: ['', Validators.required]
    })
  }

  montarUsuario() {
    let filtro = this.formulario.value;

    let usuario = {
      nome: filtro.nome,
      dataNascimento: new Date(filtro.dataNascimento),
      email: filtro.email,
      senha: filtro.senha,
      sexoId: filtro.sexoId,
      ativo: true
    }

    return usuario;
  }

  cadastrar() {
    let usuario = this.montarUsuario();

    this.usuarioService.cadastrar(usuario).subscribe((res: any) => {
      console.log(res);
    })
  }
}
