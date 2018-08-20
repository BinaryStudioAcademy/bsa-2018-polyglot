import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.sass']
})
export class ContactComponent implements OnInit {
  lat: number = 49.8559897;
  lng: number = 24.0197202;
  constructor() { }

  ngOnInit() {
  }

}
