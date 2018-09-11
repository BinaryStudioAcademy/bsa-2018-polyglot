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
  @Input() projectId: number;

  public isLoad: boolean = false;

  view: any[] = [350, 350];
  pieView: any[] = [850, 400];

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

    this.projectService.getProjectReports(this.projectId)
      .subscribe(reports => {
        this.charts = reports.charts.map(function (chart: any) {
            return {
              name: chart.name,
              data: chart.values,
            };

        });
        console.log(this.charts);
      },
        err => {
          this.isLoad = false;
        },
        () =>
        {
          this.isLoad = true;
        }

      );
  }

}
