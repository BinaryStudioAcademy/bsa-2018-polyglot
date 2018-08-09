import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, MatPaginator, MatTableDataSource } from '@angular/material';
import { FormControl, FormGroupDirective, NgForm, Validators} from '@angular/forms';
import { MatTable } from '@angular/material';
import { ErrorStateMatcher} from '@angular/material/core';
import { Observable, of } from 'rxjs';
import { Sort} from '@angular/material';
import { Translator } from '../../../models/translator';
import { UserProfile } from '../../../models/user-profile';
import { Rating } from '../../../models/rating';
import { SearchService } from '../../../services/search.service';
import { SelectionModel } from '@angular/cdk/collections';

const mockTranslators: Translator[] = 
[
  {
      id: 1,
      userProfile: {
        id: 1,
        firstName: 'Vasilij',
        lastName: 'Polietilen',
        birthDate: new Date(1992, 1,1),
        registrationDate: new Date(),
        country: 'Ukraine',
        city: 'Kiev',
        region: '',
        postalCode: '',
        address: '',
        phone: '',
        avatarUrl: ''
      },
      rating: null,
      teamTranslators: null
  },
  {
      id: 2,
      userProfile: {
        id: 1,
        firstName: 'Grigorij',
        lastName: 'Butylka',
        birthDate: new Date(1992, 1,1),
        registrationDate: new Date(),
        country: 'Ukraine',
        city: 'Kiev',
        region: '',
        postalCode: '',
        address: '',
        phone: '',
        avatarUrl: ''
      },
      rating: null,
      teamTranslators: null
  },
  {
      id: 3,
      userProfile: {
        id: 1,
        firstName: 'Alexej',
        lastName: 'Chibo',
        birthDate: new Date(1992, 1,1),
        registrationDate: new Date(),
        country: 'Ukraine',
        city: 'Kiev',
        region: '',
        postalCode: '',
        address: '',
        phone: '',
        avatarUrl: ''
      },
      rating: null,
      teamTranslators: null
  },
  {
      id: 4,
      userProfile: {
        id: 1,
        firstName: 'Andrej',
        lastName: 'Mers',
        birthDate: new Date(1992, 1,1),
        registrationDate: new Date(),
        country: 'Ukraine',
        city: 'Kiev',
        region: '',
        postalCode: '',
        address: '',
        phone: '',
        avatarUrl: ''
      },
      rating: null,
      teamTranslators: null
  },
  {
      id: 5,
      userProfile: {
        id: 1,
        firstName: 'Viktor',
        lastName: 'Rozembaum',
        birthDate: new Date(1992, 1,1),
        registrationDate: new Date(),
        country: 'Ukraine',
        city: 'Kiev',
        region: '',
        postalCode: '',
        address: '',
        phone: '',
        avatarUrl: ''
      },
      rating: null,
      teamTranslators: null
  },
  {
      id: 6,
      userProfile: {
        id: 1,
        firstName: 'Alexander',
        lastName: 'Denisov',
        birthDate: new Date(1992, 1,1),
        registrationDate: new Date(),
        country: 'Ukraine',
        city: 'Kiev',
        region: '',
        postalCode: '',
        address: '',
        phone: '',
        avatarUrl: ''
      },
      rating: null,
      teamTranslators: null
  },
  {
      id: 7,
      userProfile: {
        id: 1,
        firstName: 'Viktor',
        lastName: 'Boroda',
        birthDate: new Date(1992, 1,1),
        registrationDate: new Date(),
        country: 'Ukraine',
        city: 'Kiev',
        region: '',
        postalCode: '',
        address: '',
        phone: '',
        avatarUrl: ''
      },
      rating: null,
      teamTranslators: [{
        id: 1,
        teamId: 1,
        team: null,
        translatorId: 6,
        translator: null,
        translatorRights: null
      },
      {
        id: 2,
        teamId: 1,
        team: null,
        translatorId: 5,
        translator: null,
        translatorRights: null
      },
      {
        id: 3,
        teamId: 1,
        team: null,
        translatorId: 7,
        translator: null,
        translatorRights: [
          {
            teamTranslatorId: 3,
            teamTranslator: null,
            rightId: 1,
            right: {
              id: 1,
              definition: "key",
              translatorRights: null
            }
          },
          {
            teamTranslatorId: 3,
            teamTranslator: null,
            rightId: 2,
            right: {
              id: 2,
              definition: "lanGuAge-add",
              translatorRights: null
            }
          }
        ]
      }]
  }
  
];

@Component({
  selector: 'app-team',
  templateUrl: './team.component.html',
  styleUrls: ['./team.component.sass']
})
export class TeamComponent implements OnInit {

  id: number = 88;
  translators: Translator[] = mockTranslators;
  emailToSearch: string;
  displayedColumns: string[] = ['name', 'email', 'rights', 'options' ];
  dataSource = new MatTableDataSource(this.translators);
  emailFormControl = new FormControl('', [
    Validators.email,
  ]);
  searchResultRecieved: boolean = false;
  ckb: boolean = false;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatTable) table: MatTable<any>;

  constructor(private searchService: SearchService) {
  }

  ngOnInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }
  ngAfterViewInit() {
    // If the user changes the sort order, reset back to the first page.
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);
  }
  
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  searchTranslators() {
    this.searchService.FindTranslatorsByEmail("")
        .subscribe((data: Translator[]) => {
          this.translators = data.concat(this.translators);
          this.dataSource = new MatTableDataSource(this.translators);
          this.dataSource.paginator = this.paginator;
          this.paginator.pageIndex = 0;
        });
  }

  checkTranslatorRight(id: number, rightName: string) : boolean{
    if(id == 7){
      debugger;
    }
    let targetTranslator = this.translators.find(t => t.id === id);
    if(!targetTranslator)
      return false;
    
      if(targetTranslator.teamTranslators == null){
        return false;
      }
      else{
        let team = targetTranslator
        .teamTranslators
        .find(t => t.translatorId === id);
        if(team == null){
          return false;
        }
        else{
          return team.translatorRights
        .find(r => r.right.definition.trim().toLowerCase() === rightName.trim().toLowerCase()) != null;
        }
      } 
  }

  addRightToTranslator(id: number, rightName: string) {
    
  }
  method(e, id) : boolean{
    debugger;
    console.log(e.target.checked + "  id = " + id);
    return true;
  }
}