import { Component, OnInit, Output } from '@angular/core';
import { IString } from '../../models/string';
import { Tag } from '../../models/tag';
import { MAT_DIALOG_DATA } from '@angular/material';
import { Inject } from '@angular/core';
import { ComplexStringService } from '../../services/complex-string.service';
import { MatDialogRef } from '@angular/material';
import { SnotifyService } from 'ng-snotify';
import { EventEmitter } from '@angular/core';
import { CompressFileService } from '../../services/compress-file.service';

@Component({
    selector: 'app-string-dialog',
    templateUrl: './string-dialog.component.html',
    styleUrls: ['./string-dialog.component.sass']
})

export class StringDialogComponent implements OnInit {

    @Output() onAddString = new EventEmitter<IString>(true);
    @Output() onEditString = new EventEmitter<IString>(true);
    public str: IString;
    public image: File;

    public projectId: number;

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: any,
        private complexStringService: ComplexStringService,
        public dialogRef: MatDialogRef<StringDialogComponent>,
        private snotifyService: SnotifyService,
        private compressService: CompressFileService) { }


    ngOnInit() {
        this.str = this.data.string;
        this.image = undefined;
    }

    receiveImage($event) {
        this.compressService.compress($event[0], { quality: 0.6 }).then((result) => {
            this.image = new File([result], $event[0].name)
        });
    }

    receiveTags($event) {
        this.str.tags = [];
        let tags: Tag[] = $event;
        for (let i = 0; i < tags.length; i++) {
            this.str.tags.push(tags[i].name);
        }
    }

    getAllTags(): Tag[] {
        let tags: Tag[] = [
            { name: 'FirstTag', color: '', id: 0, projectTags: [] },
            { name: 'SecondTag', color: '', id: 0, projectTags: [] },
            { name: 'ThirdTag', color: '', id: 0, projectTags: [] }
        ];
        return tags;
    }

    onSubmit() {

        let formData = new FormData();
        if (this.image)
            formData.append("image", this.image);
        formData.append("str", JSON.stringify(this.str));
        if(this.str.id === 0){
            this.complexStringService.create(formData)
            .subscribe(
                (d) => {
                    if (d) {
                        this.onAddString.emit(d);
                        this.snotifyService.success("ComplexString created", "Success!");
                        this.dialogRef.close();
                    }
                    else {
                        this.snotifyService.success("ComplexString wasn`t created", "Error!");
                        this.dialogRef.close();
                    }

                },
                err => {
                    console.log('err', err);
                    this.snotifyService.success("ComplexString wasn`t created", "Error!");
                    this.dialogRef.close();
                });
        } else {
            this.complexStringService.update(formData, this.str.id)
            .subscribe(
                (d) => {
                    if (d) {
                        this.onEditString.emit(d);
                        this.snotifyService.success("ComplexString edited", "Success!");
                        this.dialogRef.close();
                    }
                    else {
                        this.snotifyService.success("ComplexString wasn`t edited", "Error!");
                        this.dialogRef.close();
                    }

                },
                err => {
                    console.log('err', err);
                    this.snotifyService.success("ComplexString wasn`t edited", "Error!");
                    this.dialogRef.close();
                });
        }
    }
}


