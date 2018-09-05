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
    isWarning: boolean;
    constructor() { }

    ngOnInit() {
        this.isWarning = false;
    }



    sendImage($event) {
        debugger;
        if ($event[0].size < 10000000) {
            this.isWarning = false;
            this.fileEvent.emit($event);
        }
        else {
            this.isWarning = true;
            $event.pop();
        }
    }

}
