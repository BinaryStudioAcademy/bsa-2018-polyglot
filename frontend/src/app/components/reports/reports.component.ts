import { Component, OnInit, Input, SimpleChanges } from '@angular/core';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { Project } from '../../models';
import { ProjectService } from '../../services/project.service';
import { map } from '../../../../node_modules/rxjs/operators';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.sass']
})
export class ReportsComponent implements OnInit {
  @Input() project: Project;
  public langs = [];
  public strings = [];
  data = []
  public IsLoad: boolean = true;
  public IsLangLoad: boolean = false;
  view: any[] = [600, 400];

  // options
  showXAxis = true;
  showYAxis = true;
  gradient = false;
  showLegend = true;
  showXAxisLabel = false;
  xAxisLabel = '';
  showYAxisLabel = true;
  yAxisLabel = 'Translations';
  timeline = true;
  legendTitle = 'Languages'

  colorScheme = {
    domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA']
  };
  constructor(private projectService: ProjectService, ) {

  }

  ngOnInit() {
    this.projectService.getProjectLanguages(this.project.id)
      .subscribe(langs => {
        this.IsLoad = false;
        this.data = langs.map(function (lang: any) {
          return {
            name: lang.name,
            value: lang.translationsCount,
          };
        });
        console.log(this.data)
        console.log(this.project)

      },
        err => {
          this.IsLoad = false;
        });


        this.projectService.getProjectStrings(this.project.id)
      .subscribe(strings => {
        this.IsLoad = false;
        
        this.strings = strings.map(function (string: any) {
          return {
            name: string.name,
            value: string.translationsCount,
          };
        });

        console.log(this.strings)
      },
        err => {
          this.IsLoad = false;
        });
        
  }

  ngOnChanges(changes: SimpleChanges) {
    if (!changes)
      return

  }
  compareProgress(a, b) {
    if (a.progress < b.progress)
      return -1;
    if (a.progress > b.progress)
      return 1;
    return 0;
  }






}
