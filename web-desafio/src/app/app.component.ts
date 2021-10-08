import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'web-desafio';

  constructor(private router: Router) { }

  navigate(route: number) {
    switch(route)
    {
      case 1:
        this.router.navigate(['/'])
        break;
      case 2:
        this.router.navigate(['/usuarios'])
        break;
      case 3:
        this.router.navigate(['/cadastrar'])
        break;
    }
  }

  buscarUsuario(event: any) {
    let usuarioSearch = event.target.value;

    localStorage.setItem('usuarioSearch', usuarioSearch);

    this.navigate(2);
  }
}
