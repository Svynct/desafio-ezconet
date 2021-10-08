import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UsuarioService } from 'src/app/services/usuario.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-editar-usuario',
  templateUrl: './editar-usuario.component.html',
  styleUrls: ['./editar-usuario.component.css']
})
export class EditarUsuarioComponent implements OnInit {

  usuario: any;
  formulario: FormGroup;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private usuarioService: UsuarioService
  ) { }

  ngOnInit() {
    this.formulario = this.fb.group({
      nome: ['', Validators.required],
      dataNascimento: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      senha: ['', Validators.required],
      ativo: ['', Validators.required],
      sexoId: ['', Validators.required]
    })

    const id = Number.parseInt(this.route.snapshot.paramMap.get('id'));
    this.usuarioService.porId(id).subscribe((res: any) => {
      if (res) {
        this.formulario.get('nome').setValue(res.nome);
        this.formulario.get('dataNascimento').setValue(res.dataNascimento);
        this.formulario.get('email').setValue(res.email);
        this.formulario.get('senha').setValue(res.senha);

        let ativo = "";
        if (res.ativo == true) ativo = "1";
        else if (res.ativo == false) ativo = "0";
        this.formulario.get('ativo').setValue(ativo);

        let sexo = "";
        if (res.sexoId == 1) sexo = "1";
        else if (res.sexoId == 2) sexo = "2";
        this.formulario.get('sexoId').setValue(sexo);

        this.usuario = res;
      }
    })
  }

  editar() {
    let filtro = this.montarFiltro();
    this.usuarioService.editar(filtro).subscribe((res: any) => {
      if (res) {
        Swal.fire('Usuário editado com sucesso!', '', 'success').then(() => {
          this.router.navigate(['/usuarios'])
        })
      }
    }, err => {
      Swal.fire('Houve um erro ao editar o usuário!', '', 'error')
    })
  }

  montarFiltro() {
    let form = this.formulario.value;

    let ativo = null;
    if (form.ativo == "1") ativo = true;
    else if (form.ativo == "0") ativo = false;

    const filtro = {
      usuarioId: this.usuario.usuarioId,
      nome: form.nome,
      dataNascimento: form.dataNascimento,
      email: form.email,
      senha: form.senha,
      ativo: ativo,
      sexoId: Number.parseInt(form.sexoId)
    }

    return filtro;
  }
}
