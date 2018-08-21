import { Component, OnInit, Input } from '@angular/core';
import { ProjectService } from '../../../services/project.service';

@Component({
  selector: 'app-download-file',
  templateUrl: './download-file.component.html',
  styleUrls: ['./download-file.component.sass']
})
export class DownloadFileComponent implements OnInit {

  constructor(private projectService: ProjectService) { }

  @Input() projectId: number;

  public selectedFormat: string;
  public formats: string[] = ['.json', '.resx'];
  public selectedLanguage;
  public languages = [];

  ngOnInit() {
    this.projectService.getProjectLanguages(this.projectId)
    .subscribe(langs => {
       this.languages = langs;
    });
  }

  download() {
    this.projectService.getProjectFile(this.projectId, this.selectedLanguage.id, this.selectedFormat)
    .subscribe(() => {});
    debugger;
  }
}
