import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.sass']
})
export class HomeComponent implements OnInit {
  title = "Polyglot"
  constructor() { ;}

  ngOnInit() {
    document.body.classList.add('bg-image');
  }

}
