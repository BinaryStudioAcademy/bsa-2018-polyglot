import { Component, OnInit, Input } from '@angular/core';
import { UserService } from '../../../../services/user.service';
import { ActivatedRoute } from '../../../../../../node_modules/@angular/router';
import { ComplexStringService } from '../../../../services/complex-string.service';

@Component({
  selector: 'app-tab-history',
  templateUrl: './tab-history.component.html',
  styleUrls: ['./tab-history.component.sass']
})

export class TabHistoryComponent implements OnInit {

  private history: Array<any>;

  constructor(
    private dataProvider: ComplexStringService,
    private userService: UserService,
    private route: ActivatedRoute
  ) {
  }

  ngOnInit() {
  }

  showHistory(keyId: number, translationId: string) {
    this.history = [];

    if (keyId && translationId) {
      this.dataProvider.getTranslationHistory(keyId, translationId).subscribe(
        (result) => {
          this.history = result;
        }
      );
    } else {
      return;
    }
  }
}
