import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { DataService, RequestMethod } from '../../services/data.service';
import { ngfModule, ngf } from "angular-file"

@Component({
  selector: 'app-upload-image',
  templateUrl: './upload-image.component.html',
  styleUrls: ['./upload-image.component.sass']
})
export class UploadImageComponent implements OnInit {
  @Output() fileEvent = new EventEmitter<File>();
  fileToUpload: File = new File(["empty"], "empty");

  validDrag;
  invalidDrag;
  constructor(private dataService: DataService) { }

  ngOnInit() {
    console.log(this.fileToUpload.name);
  }



  sendImage(){
    this.fileEvent.emit(this.fileToUpload);
  }

}
