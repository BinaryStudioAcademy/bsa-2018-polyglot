import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, MatPaginator, MatTableDataSource } from '@angular/material';

import { Translator } from '../../models/translator';
import { UserProfile } from '../../models/user-profile';
import { Rating } from '../../models/rating';

const translators: Translator[] = 
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
      teamTranslators: null
  }
  
];

@Component({
  selector: 'app-team',
  templateUrl: './team.component.html',
  styleUrls: ['./team.component.sass']
})
export class TeamComponent implements OnInit {

  id: number = 88;
  displayedColumns: string[] = ['id', 'email', 'rights', 'options' ];
  dataSource = new MatTableDataSource(translators);

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  

}
