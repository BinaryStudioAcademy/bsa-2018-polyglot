import { Tag } from '../../models/tag';
import {COMMA, ENTER} from '@angular/cdk/keycodes';
import {Component, ElementRef, ViewChild, OnInit, Output, EventEmitter, Input} from '@angular/core';
import {FormControl} from '@angular/forms';
import {MatAutocompleteSelectedEvent, MatChipInputEvent} from '@angular/material';
import {Observable} from 'rxjs';
import {map, startWith} from 'rxjs/operators';

@Component({
  selector: 'app-tags',
  templateUrl: './tags.component.html',
  styleUrls: ['./tags.component.sass']
})
export class TagsComponent  {
  visible = true;
  selectable = true;
  removable = true;
  addOnBlur = false;
  separatorKeysCodes: number[] = [ENTER, COMMA];
  tagCtrl = new FormControl();
  filteredTags: Observable<Tag[]> = new Observable<Tag[]>();
  tags: Tag[] = new Array<Tag>();
  @Output() tagsEvent = new EventEmitter<Tag[]>();
  @Input() allTags: Tag[];

  @ViewChild('tagInput') tagInput: ElementRef;

  constructor() {
    this.filteredTags = this.tagCtrl.valueChanges.pipe(
        startWith(null),
        map((str: any | null) => {
          this.updateTags();
          if(str == null) {
            return this.allTags.slice();
          } else {
             return (str.name ? this._filter(str.name) : this._filter(str));
          }
        }));      
  }

  updateTags(){
    this._unique();
    this.tagsEvent.emit(this.tags);
  }

  add(event: MatChipInputEvent): void {

    const input = event.input;
    const value = event.value;

    if ((value || '').trim()) {
      this.tags.push({name: value.trim(), color: '', id: 0, projectTags:[]});
    }

    if (input) {
      input.value = '';
    }
    this.tagCtrl.setValue(null);
  }

  remove(tag: Tag): void {
    const index = this.tags.indexOf(tag);

    if (index >= 0) {
      this.tags.splice(index, 1);
      this.updateTags();
    }
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    this.tags.push({name: event.option.viewValue, color:'', id: 0, projectTags:[]});
    this.tagInput.nativeElement.value = '';
    this.tagCtrl.setValue(null);
  }

  private _filter(value: string): Tag[] {
    const filterValue = value.toLowerCase();

    return this.allTags.filter(tag => tag.name.toLowerCase().indexOf(filterValue) === 0);
  }


  private _unique(){
    for(let i = 0; i < this.tags.length;i++){
        for(let j = i;j<this.tags.length;j++){
          if((this.tags[i].name.toLowerCase() == this.tags[j].name.toLowerCase()) && i != j){
            this.tags.splice(i, 1);
          }
        }
      
    }
  }

}
