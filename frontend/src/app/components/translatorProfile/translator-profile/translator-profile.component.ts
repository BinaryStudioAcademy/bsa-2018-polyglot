import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-translator-profile',
  templateUrl: './translator-profile.component.html',
  styleUrls: ['./translator-profile.component.sass']
})
export class TranslatorProfileComponent implements OnInit{
  ngOnInit(): void {
    this.Name = "Sasha Pushkin"
    this.Rating = 4.6;
  }
Name : string
Rating : number

}
