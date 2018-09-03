import { Component, OnInit, Input } from '@angular/core';
import { ProjectService } from '../../../services/project.service';
import { Glossary, Project } from '../../../models';
import { GlossaryService } from '../../../services/glossary.service';
import { MatTableDataSource } from '@angular/material';
import { forkJoin } from 'rxjs';

@Component({
    selector: 'app-assign-glossaries',
    templateUrl: './assign-glossaries.component.html',
    styleUrls: ['./assign-glossaries.component.sass']
})
export class AssignGlossariesComponent implements OnInit {

    @Input() projectId: number;
    public AssignedGlossaries: Glossary[] = [];
    public AllGlossaries: Glossary[] = [];
    displayedColumns: string[] = ['name', 'originLanguage', 'action_btn'];
    assignedDataSource: MatTableDataSource<Glossary>;
    allDataSource: MatTableDataSource<Glossary>;

    constructor(
        private projectService: ProjectService,
        private glossariesService: GlossaryService
    ) { }

    ngOnInit() {
        this.refresh();
    }

    refresh() {

        this.projectService.getAssignedGlossaries(this.projectId).subscribe(
            (data) => {
                this.AssignedGlossaries = data;
                this.assignedDataSource = new MatTableDataSource(this.AssignedGlossaries);
                this.glossariesService.getAll().subscribe(data => {
                    this.AllGlossaries = data;
                    var glossaries = this.AllGlossaries.filter(x => {
                        this.AssignedGlossaries.forEach(function (element) {
                            if (x.id === element.id)
                                return true;
                        });
                        return false;
                    });
                    //!this.AssignedGlossaries.includes(x));
                    this.allDataSource = new MatTableDataSource(glossaries);
                });
            },
            (err) => {
                this.glossariesService.getAll().subscribe(data => {
                    this.AllGlossaries = data;
                    this.allDataSource = new MatTableDataSource(this.AllGlossaries);
                });
            }
        );
    }

    public isAssigned(item: Glossary): boolean {
        let res = false;
        this.AssignedGlossaries.forEach(i => {
            if (i.id === item.id) {
                res = true;
            }
        });
        return res;
    }

    onAction(item: Glossary) {
        if (this.isAssigned(item)) {
            this.AssignedGlossaries.splice(this.AssignedGlossaries.indexOf(item), 1);
            this.projectService.dismissProjectGlossary(this.projectId, item.id).subscribe(() => {
                this.refresh();
            });
        } else {
            this.AssignedGlossaries.push(item);
            this.projectService.assignGlossariesToProject(this.projectId, [item.id]).subscribe(() => {
                this.refresh();
            });
        }

    }

}
