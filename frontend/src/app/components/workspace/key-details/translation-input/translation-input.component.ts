import { Component, OnInit, Input, Output, EventEmitter, AfterContentInit, AfterViewInit } from '@angular/core';

@Component({
  selector: 'app-translation-input',
  templateUrl: './translation-input.component.html',
  styleUrls: ['./translation-input.component.sass']
})
export class TranslationInputComponent implements OnInit, AfterViewInit {

  private divObject: HTMLElement;
  inputTextValue: string;

  @Input() index;

  @Output() inputTextChange = new EventEmitter();

  @Input()
  get inputText() {
    return this.inputTextValue;
  }

  set inputText(value) {
    this.inputTextValue = value;
    this.inputTextChange.emit(this.inputTextValue);
  }

  constructor() { 
  }

  ngOnInit() {
  }

  ngAfterViewInit() {
    this.divObject = document.getElementById(`translation${this.index}`);
  }

  onTextChanged($event) {
    this.divObject.textContent = this.inputTextValue;
    if ($event.keyCode === 32 || $event.which === 32) {
      console.log('hello');
    }
  }
}
