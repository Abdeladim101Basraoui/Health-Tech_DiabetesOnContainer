import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EchographieDetailsComponent } from './echographie-details.component';

describe('EchographieDetailsComponent', () => {
  let component: EchographieDetailsComponent;
  let fixture: ComponentFixture<EchographieDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EchographieDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EchographieDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
