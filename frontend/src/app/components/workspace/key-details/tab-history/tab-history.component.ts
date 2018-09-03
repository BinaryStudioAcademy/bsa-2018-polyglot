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

  public history: Array<any>;
  public currentPage = 0;
  public elementsOnPage = 7;
  public keyId: number;
  public translationId: string;
  public previousId: string;

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
      this.keyId = keyId;
      this.translationId = translationId;
      if (!this.previousId) {
        this.previousId = this.translationId;
      }
      else {
        if (this.previousId !== this.translationId) {
          this.currentPage = 0;
          this.previousId = this.translationId
        }
        if (this.previousId === this.translationId) {
          this.currentPage = 0;
        }
      }
      this.dataProvider.getTranslationHistory(keyId, translationId, this.elementsOnPage, this.currentPage).subscribe(
        (result) => {
          this.history = result;
          this.currentPage++;
        }
      );
    } else {
      return;
    }
  }

  public onScrollDown(): void {
    this.getHistory(this.currentPage, history => {
      this.history = this.history.concat(history);
    });
  }


  getHistory(page: number = 1, saveResultsCallback: (history) => void) {
    return this.dataProvider
      .getTranslationHistory(
        this.keyId,
        this.translationId,
        this.elementsOnPage,
        this.currentPage
      )
      .subscribe((history: any) => {

        this.currentPage++;
        saveResultsCallback(history);
      });
  }
}
