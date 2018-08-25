import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { ImageCropperComponent, CropperSettings } from "ngx-img-cropper";
import { MatDialogRef } from '@angular/material';
import {MAT_DIALOG_DATA} from '@angular/material';

@Component({
  selector: 'app-cropper',
  templateUrl: './cropper.component.html',
  styleUrls: ['./cropper.component.sass']
})
export class CropperComponent implements OnInit{

  cropperSettings: CropperSettings;
  imageData: any;
  public selectedImage: File;
  image: File;
  @ViewChild('cropper', undefined) 
  cropper: ImageCropperComponent;
  constructor(
    public dialogRef: MatDialogRef<CropperComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.cropperSettings = new CropperSettings();
    this.cropperSettings.width = 200;
    this.cropperSettings.height = 200;
    this.cropperSettings.keepAspect = false;

    this.cropperSettings.canvasWidth = 500;
    this.cropperSettings.canvasHeight = 300;
    this.cropperSettings.minWidth = 100;
    this.cropperSettings.minHeight = 100;
    this.cropperSettings.rounded = true;
    this.cropperSettings.minWithRelativeToResolution = false;
    this.cropperSettings.cropperDrawSettings.strokeColor = 'rgba(255,255,255,1)';
    this.cropperSettings.cropperDrawSettings.strokeWidth = 2;
    this.cropperSettings.noFileInput = true;
    this.imageData = {}
   }

  ngOnInit() {
    var image: HTMLImageElement = new Image();
    image.src = this.data.imageUrl;
    image.crossOrigin = "Anonymous";
    image.onload = ()=> {
      this.cropper.setImage(image);
    };
  }

  fileChangeListener($event) {
    var image:any = new Image();
    this.image = $event.target.files[0];
    var myReader:FileReader = new FileReader();
    var that = this;
    myReader.onloadend = function (loadEvent:any) {
      image.src = loadEvent.target.result;
      that.cropper.setImage(image);
     };

    myReader.readAsDataURL(this.image);
  }

  uploadImage() {
     this.selectedImage = this.image;
     this.dialogRef.close();
  }
}