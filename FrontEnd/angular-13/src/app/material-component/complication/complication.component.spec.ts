import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComplicationComponent } from './complication.component';

describe('ComplicationComponent', () => {
  let component: ComplicationComponent;
  let fixture: ComponentFixture<ComplicationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ComplicationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ComplicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
