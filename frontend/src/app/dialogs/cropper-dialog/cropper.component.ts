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
  @ViewChild('cropper', undefined) 
  cropper: ImageCropperComponent;
  constructor(
    public dialogRef: MatDialogRef<CropperComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.cropperSettings = new CropperSettings();
    this.cropperSettings.width = 100;
    this.cropperSettings.height = 100;
    this.cropperSettings.croppedWidth = 50;
    this.cropperSettings.croppedHeight = 50;
    this.cropperSettings.canvasWidth = 400;
    this.cropperSettings.canvasHeight = 300;
    this.imageData = {}
   }

  ngOnInit() {
    var image: HTMLImageElement = new Image();
    image.src = this.data.imageUrl;
    image.crossOrigin = "anonymous";
    image.onload = ()=> {
      this.cropper.setImage(image);
    };
  }

  uploadImage() {
    console.log("SAVED!");
  }
}