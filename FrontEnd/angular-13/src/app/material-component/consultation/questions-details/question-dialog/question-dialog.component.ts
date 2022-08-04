import { Component, ElementRef, OnInit, ViewChild } from "@angular/core";
import { COMMA, ENTER } from "@angular/cdk/keycodes";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Observable } from "rxjs";
import { map, startWith } from "rxjs/operators";
import { MatChipInputEvent } from "@angular/material/chips";
import { MatAutocompleteSelectedEvent } from "@angular/material/autocomplete";
import { validateBasis } from "@angular/flex-layout";

@Component({
  selector: "app-question-dialog",
  templateUrl: "./question-dialog.component.html",
  styleUrls: ["./question-dialog.component.css"],
})
export class QuestionDialogComponent implements OnInit {
  separatorKeysCodes: number[] = [ENTER, COMMA];
  resCtrl = new FormControl("");
  filteredRes!: Observable<string[]>;
  responses: string[] = ["true"];
  allResponses: string[] = ['yes','no'];

  @ViewChild("fruitInput") responseInput!: ElementRef<HTMLInputElement>;

  questionForm!: FormGroup;

  question = new FormControl("", Validators.required);
  notes = new FormControl("", Validators.required);

  confirmBtnstate:string = 'save';

  constructor() {
    this.filteredRes = this.resCtrl.valueChanges.pipe(
      startWith(null),
      map((fruit: string | null) =>
        fruit ? this._filter(fruit) : this.allResponses.slice()
      )
    );
  }

  add(event: MatChipInputEvent): void {
    const value = (event.value || "").trim();

    // Add our fruit
    if (value) {
      this.responses.push(value);
    }

    // Clear the input value
    event.chipInput!.clear();

    this.resCtrl.setValue(null);
  }

  remove(fruit: string): void {
    const index = this.responses.indexOf(fruit);

    if (index >= 0) {
      this.responses.splice(index, 1);
    }
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    this.responses.push(event.option.viewValue);
    this.responseInput.nativeElement.value = "";
    this.resCtrl.setValue(null);
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.allResponses.filter((res) =>
      res.toLowerCase().includes(filterValue)
    );
  }

  PostQuestion()
  {
console.log('send data');

  }


  ngOnInit(): void {}

  getErrorMessage() {
    if (this.question.hasError("required")) {
      return "You must enter a value";
    }
    return this.question.hasError("nompres") ? "Not a valid nompres" : "";
  }
}
