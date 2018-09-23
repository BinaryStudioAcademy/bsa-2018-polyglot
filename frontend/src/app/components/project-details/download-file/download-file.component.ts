import { Component, OnInit, Input } from '@angular/core';
import { ProjectService } from '../../../services/project.service';
import { saveAs } from 'file-saver/FileSaver';
import { SnotifyService, SnotifyPosition, SnotifyToastConfig } from 'ng-snotify';


@Component({
    selector: 'app-download-file',
    templateUrl: './download-file.component.html',
    styleUrls: ['./download-file.component.sass']
})
export class DownloadFileComponent implements OnInit {

    constructor(private projectService: ProjectService, private snotifyService: SnotifyService) { }

    @Input() project;

    public selectedFormat: string = '.json';
    public formats: string[] = ['.json', '.resx'];
    public selectedLanguage;
    public languages = [];

    ngOnInit() {
        this.projectService.getProjectLanguages(this.project.id)
            .subscribe(langs => {
                this.languages = langs;
            });
    }

    download() {
        this.projectService.getProjectFile(this.project.id, this.selectedLanguage.id, this.selectedFormat)
            .subscribe((data) => {

                saveAs(data, `${this.project.name}(${this.selectedLanguage.code})${this.selectedFormat}`);
                this.snotifyService.success("File Downloaded", "Success!");
            }, err => {
                this.snotifyService.error("File wasn`t downloaded", "Error!");
            });
    }

    downloadFullLocal() {
        // Create new method
        this.projectService.getProjectLocal(this.project.id, this.selectedFormat)
            .subscribe((data) => {
                debugger;
                //
            });
    }
}
