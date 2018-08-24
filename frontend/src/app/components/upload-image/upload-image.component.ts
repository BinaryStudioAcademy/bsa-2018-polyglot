import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'app-upload-image',
  templateUrl: './upload-image.component.html',
  styleUrls: ['./upload-image.component.sass']
})
export class UploadImageComponent implements OnInit {
  @Input() imageUrl: String;
  @Output() fileEvent = new EventEmitter<File>();
  fileToUpload: File;

  validDrag: boolean;
  invalidDrag: boolean;
  constructor() { }

  ngOnInit() {
    
  }



  sendImage($event){
    this.fileEvent.emit($event);
  }

}
