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
    public NotAssignedGlossaries: Glossary[] = [];
    displayedColumns: string[] = ['name', 'originLanguage', 'action_btn'];
    assignedDataSource: MatTableDataSource<Glossary>;
    notAssignedSource: MatTableDataSource<Glossary>;

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
            });
        this.projectService.getNotAssignedGlossaries(this.projectId).subscribe(
            (data) => {
                this.NotAssignedGlossaries = data;
                this.notAssignedSource = new MatTableDataSource(this.NotAssignedGlossaries);
            });

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
            this.projectService.dismissProjectGlossary(this.projectId, item.id).subscribe(() => {
                this.refresh();
            });
        } else {
            this.projectService.assignGlossariesToProject(this.projectId, [item.id]).subscribe(() => {
                this.refresh();
            });
        }

    }

}
