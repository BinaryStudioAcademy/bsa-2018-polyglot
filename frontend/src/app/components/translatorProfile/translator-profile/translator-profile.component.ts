import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-translator-profile',
  templateUrl: './translator-profile.component.html',
  styleUrls: ['./translator-profile.component.sass']
})
export class TranslatorProfileComponent implements OnInit{
  ngOnInit(): void {
    this.Name = "Sasha Pushkin"
    this.Rating = 8.6;
    this.DateComment = "11:11:2341"
    this.Avatar = "http://static-29.sinclairstoryline.com/resources/media/2d9080f1-46ec-47b0-3874-d5190c1b02e7-2d9080f146ec47b03874d5190c1b02e7rendition_1_scottthuman5x7bluegradient.jpg?1519078303490"
  }
Name : string
Rating : number
Avatar : string
DateComment : string
}
