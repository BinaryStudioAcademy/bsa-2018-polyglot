import { Component, OnInit, Input, SimpleChanges } from '@angular/core';
import { Project } from '../../models';
import { ProjectService } from '../../services/project.service';
import { Chart } from '../../models/chart';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.sass']
})
export class ReportsComponent implements OnInit {
  @Input() project: Project;
 
  public IsLoad: boolean = true;
  public IsLangLoad: boolean = false;
  view: any[] = [350, 350];
  piView: any[] = [500, 400];

  charts: any;
  explodeSlices = false;
  doughnut = false;
  showLabels = true;

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
  colorScheme = 'vivid'

  constructor(private projectService: ProjectService, ) {
  }

  ngOnInit() {

    this.projectService.getProjectReports(this.project.id)
      .subscribe(reports => {
        this.charts = reports.charts.map(function (chart: any) {
          return {
            name: chart.name,
            data: chart.values,
          };
        });
      },
        err => {
          this.IsLoad = false;
        });
  }

}
