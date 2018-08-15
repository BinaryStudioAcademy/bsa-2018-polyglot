import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-languages',
  templateUrl: './languages.component.html',
  styleUrls: ['./languages.component.sass']
})
export class LanguagesComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  public langs = [
    {
      Lang: "Russian",
      Progress: 33
    },
    {
      Lang: "French",
      Progress: 96
    },
    {
      Lang: "English",
      Progress: 43
    },
    {
      Lang: "Spanish",
      Progress: 14
    },
    {
      Lang: "Deutch",
      Progress: 75
    }
  ]
}
