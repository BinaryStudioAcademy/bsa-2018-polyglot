import { Component, OnInit } from '@angular/core';
import { DataService, RequestMethod } from '../../services/data.service';

@Component({
  selector: 'app-upload-image',
  templateUrl: './upload-image.component.html',
  styleUrls: ['./upload-image.component.sass']
})
export class UploadImageComponent implements OnInit {
  ImageUrl: string = "/assets/images/default.jpg";
  fileToUpload: File;
  constructor(private dataService: DataService) { }

  ngOnInit() {
  }

  handleFileInput(files: FileList){
    this.fileToUpload = files.item(0);

    let reader = new FileReader();
    reader.onload = (event: any) =>{
      this.ImageUrl = event.target.result;
    }
    reader.readAsDataURL(this.fileToUpload);
  }

  async uploadImage(){

    const formData = new FormData();
    formData.append(this.fileToUpload.name, this.fileToUpload);
    (await this.dataService.sendRequest(RequestMethod.Post, "image", undefined, formData)).subscribe();
  }

}
