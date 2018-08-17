import { Component, OnInit } from '@angular/core';
import { ContainerComponent, DraggableComponent, IDropResult } from 'ngx-smooth-dnd';
import { applyDrag, generateItems } from './utils';

@Component({
  selector: 'app-project-team',
  templateUrl: './project-team.component.html',
  styleUrls: ['./project-team.component.sass']
})
export class ProjectTeamComponent implements OnInit {

  items = generateItems(50, i => ({ data: 'Draggable ' + i }))

  onDrop(dropResult: IDropResult) {
    // update item list according to the @dropResult
    this.items = applyDrag(this.items, dropResult);
  }
  
  constructor() { }

  ngOnInit() {
  }

}
