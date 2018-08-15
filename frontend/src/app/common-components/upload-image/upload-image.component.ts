import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { HttpService, RequestMethod } from '../../services/http.service';
import { ngfModule, ngf } from "angular-file"

@Component({
  selector: 'app-upload-image',
  templateUrl: './upload-image.component.html',
  styleUrls: ['./upload-image.component.sass']
})
export class UploadImageComponent implements OnInit {
  @Output() fileEvent = new EventEmitter<File>();
  fileToUpload: File;

  validDrag;
  invalidDrag;
  constructor(private dataService: HttpService) { }

  ngOnInit() {
  }



  sendImage($event){
    this.fileEvent.emit($event);
  }

}
