import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { HttpService, RequestMethod } from '../../services/http.service';
import { ngfModule, ngf } from 'angular-file';
import { ProjectService } from '../../services/project.service';
import { SnotifyService, SnotifyPosition, SnotifyToastConfig } from 'ng-snotify';
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-upload-file',
  templateUrl: './upload-file.component.html',
  styleUrls: ['./upload-file.component.sass']
})
export class UploadFileComponent implements OnInit {

  @Output() fileEvent = new EventEmitter<File>();
  fileToUpload: File;
  formData: FormData;
  projectId: number;
  private routeSub: Subscription;

  validDrag;
  invalidDrag;


  constructor(private service: ProjectService,
              private snotifyService: SnotifyService,
              private activatedRoute: ActivatedRoute) {

    this.formData = new FormData();
   }

   ngOnInit() {
    this.routeSub = this.activatedRoute.params.subscribe((params) => {
      this.projectId = params.projectId;
    });
  }

  ConfirmFile($event) {
    this.fileEvent.emit($event);
    this.fileToUpload = $event[$event.length - 1];
  }

  Upload() {
    this.formData.set("file", this.fileToUpload);
    // this.projectId = 1;
    this.service.postFile(this.projectId, this.formData)
    .subscribe(
      (d) => { 
          this.snotifyService.success("File Uploaded", "Success!");
        },
        err => {
          this.snotifyService.error("File wasn`t uploaded", "Error!");
        }
      );
  }

}
