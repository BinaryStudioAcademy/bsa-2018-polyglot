import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';



export interface Glossary {
  lang1: string;
  lang2: string;
  locale: string;
  glossary_link: string;
  guide: string;
}

const GLOSSARIES_DATA: Glossary[] = [
  {lang1: "Asturian", lang2: "Asturianu", locale: 'ast', glossary_link: 'Glossary', guide: ''},
  {lang1: "Basque", lang2: "Euskara", locale: 'eu', glossary_link: 'Glossary', guide: ''},
  {lang1: "Bengali", lang2: "বাংলা", locale: 'bn_BD', glossary_link: 'Glossary', guide: 'Start Guide'},
  {lang1: "Belarusian", lang2: "Беларуская", locale: 'bel', glossary_link: 'Glossary', guide: ''},
  {lang1: "Czech", lang2: "Čeština‎", locale: 'cs_CZ', glossary_link: 'Glossary', guide: ''},
  {lang1: "Danish", lang2: "Dansk", locale: 'da_DK', glossary_link: 'Glossary', guide: ''},
  {lang1: "Dutch", lang2: "Nederlands", locale: 'nl_NL', glossary_link: 'Glossary', guide: 'Start Guide'},
  {lang1: "Finnish", lang2: "Suomi", locale: 'fi', glossary_link: 'Glossary', guide: 'Start Guide'},
  {lang1: "German", lang2: "Deutsch", locale: 'de_DE', glossary_link: 'Glossary', guide: ''},
  {lang1: "English (UK)", lang2: "English (UK)", locale: 'en_GB', glossary_link: 'Glossary', guide: ''},
  {lang1: "English (Canada)", lang2: "English (Canada)", locale: 'en_CA', glossary_link: 'Glossary', guide: ''},
  {lang1: "Greek", lang2: "Ελληνικά", locale: 'el', glossary_link: 'Glossary', guide: ''},
  {lang1: "Hindi", lang2: "हिन्दी", locale: 'hi_IN', glossary_link: 'Glossary', guide: 'Start Guide'},
  {lang1: "Italian", lang2: "Italiano", locale: 'it_IT', glossary_link: 'Glossary', guide: ''},
  {lang1: "Japanese", lang2: "日本語", locale: 'ja', glossary_link: 'Glossary', guide: ''},
  {lang1: "Ukrainian", lang2: "Українська", locale: 'uk', glossary_link: 'Glossary', guide: ''},
  {lang1: "Welsh", lang2: "Cymraeg", locale: 'cy', glossary_link: 'Glossary', guide: ''},

];


@Component({
  selector: 'app-glossaries',
  templateUrl: './glossaries.component.html',
  styleUrls: ['./glossaries.component.sass']
})
export class GlossariesComponent implements OnInit {
  displayedColumns: string[] = ['lang1', 'lang2', 'locale', 'glossary_link', 'guide'];
  dataSource = new MatTableDataSource(GLOSSARIES_DATA);
  
  constructor() { }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
  ngOnInit() {
  }

}
