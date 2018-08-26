import { Component, OnInit, OnChanges, Input } from "@angular/core";
import { GlossaryTerm } from "../../../../models/glossaryTerm";
import { GlossaryService } from "../../../../services/glossary.service";

@Component({
    selector: "app-tab-glossary",
    templateUrl: "./tab-glossary.component.html",
    styleUrls: ["./tab-glossary.component.sass"]
})
export class TabGlossaryComponent implements OnInit, OnChanges {
    glossary: GlossaryTerm[] = [];

    @Input()
    translation: string;
    @Input()
    keyDetails: any;

    constructor(private glossaryService: GlossaryService) {}

    ngOnInit() {}

    ngOnChanges() {
        this.glossary = [];
        if (this.keyDetails) {
            if (!this.translation) {
                this.glossary = this.glossaryService.fakeGlossaryParse(
                    this.keyDetails.base,
                    ""
                );
            } else {
                this.glossary = this.glossaryService.fakeGlossaryParse(
                    this.keyDetails.base,
                    this.translation
                );
            }
        }
    }
}
