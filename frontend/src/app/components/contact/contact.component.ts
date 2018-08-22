import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.sass']
})
export class ContactComponent implements OnInit {
  lat: number = 49.856991;
  lng: number = 24.020273
  zoom: number = 12;
  constructor() { }

  ngOnInit() {
  }

}
