import { Tag } from '../../models/tag';
import {COMMA, ENTER} from '@angular/cdk/keycodes';
import {Component, ElementRef, ViewChild, OnInit} from '@angular/core';
import {FormControl} from '@angular/forms';
import {MatAutocompleteSelectedEvent, MatChipInputEvent} from '@angular/material';
import {Observable} from 'rxjs';
import {map, startWith} from 'rxjs/operators';

@Component({
  selector: 'app-tags',
  templateUrl: './tags.component.html',
  styleUrls: ['./tags.component.sass']
})
export class TagsComponent implements OnInit {
  allTags: Tag[] = new Array<Tag>();
  visible = true;
  selectable = true;
  removable = true;
  tagCtrl = new FormControl();
  addOnBlur = false;
  separatorKeysCodes: number[] = [ENTER, COMMA];
  filteredTags: Observable<Tag[]>;
  tags: Tag[] = new Array<Tag>();

  constructor() { 
    this.filteredTags = this.tagCtrl.valueChanges.pipe(
      startWith(null),
      map((tag: Tag | null) => tag ? this._filter(tag.name) : this.allTags.slice()));
  }

  add(event: MatChipInputEvent): void {
    const input = event.input;
    const value = event.value;

    // Add our fruit
    if ((value || '').trim()) {
      this.tags.push({name: value, id: 0, color: "primary", projectTags:[]});
    }

    // Reset the input value
    if (input) {
      input.value = '';
    }
  }

  remove(tag: Tag): void {
    const index = this.tags.indexOf(tag);

    if (index >= 0) {
      this.tags.splice(index, 1);
    }
  }

  private _filter(value: string): Tag[] {
    const filterValue = value.toLowerCase();

    return this.tags.filter(tag => tag.name.toLowerCase().indexOf(filterValue) === 0);
  }

   ngOnInit() {
    this.allTags.push({name: "FirstTag", id: 5, color: "primary", projectTags:[]});
    this.allTags.push({name: "SecondTag", id: 5, color: "blue", projectTags:[]});
    this.allTags.push({name: "ThirdTag", id: 5, color: "accent", projectTags:[]});
    this.allTags.push({name: "ourthTag", id: 5, color: "green", projectTags:[]});
    this.allTags.push({name: "FifthTag", id: 5, color: "accen", projectTags:[]});
    this.allTags.push({name: "SixthTag", id: 5, color: "primary", projectTags:[]});
    this.allTags.push({name: "SeventhTag", id: 5, color: "white", projectTags:[]});
  }

}
