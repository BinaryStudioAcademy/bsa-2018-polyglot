import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-workspace-key-details',
  templateUrl: './key-details.component.html',
  styleUrls: ['./key-details.component.sass']
})
export class KeyDetailsComponent implements OnInit, OnDestroy {

  private routeSub: Subscription;

  constructor(
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit() {
    this.routeSub = this.activatedRoute.params.subscribe((params) => {
      //making api call using service service.get(params.keyId); ....

      console.log(params.keyId);
    });
  }

  ngOnDestroy() {
    this.routeSub.unsubscribe();
  }

}
