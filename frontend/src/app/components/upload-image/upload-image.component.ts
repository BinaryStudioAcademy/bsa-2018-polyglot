import { Component, OnInit } from '@angular/core';
import { DataService, RequestMethod } from '../../services/data.service';
import { ngfModule, ngf } from "angular-file"

@Component({
  selector: 'app-upload-image',
  templateUrl: './upload-image.component.html',
  styleUrls: ['./upload-image.component.sass']
})
export class UploadImageComponent implements OnInit {
  fileToUpload: File;
  validDrag;
  invalidDrag;
  constructor(private dataService: DataService) { }

  ngOnInit() {
  }



  async uploadImage(){

    const formData = new FormData();
    formData.append(this.fileToUpload.name, this.fileToUpload);
    this.dataService.sendRequest(RequestMethod.Post, "image", undefined, formData).subscribe();
  }

}
