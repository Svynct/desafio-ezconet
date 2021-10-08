import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {
  }

  navigate(n: number) {
    switch (n) {
      case 1:
        this.router.navigate(['/usuarios']);
        break;
      case 2:
        this.router.navigate(['/cadastrar']);
        break;
    }
  }
}
