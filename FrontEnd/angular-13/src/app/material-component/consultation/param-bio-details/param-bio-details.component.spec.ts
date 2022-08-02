import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ParamBioDetailsComponent } from './param-bio-details.component';

describe('ParamBioDetailsComponent', () => {
  let component: ParamBioDetailsComponent;
  let fixture: ComponentFixture<ParamBioDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ParamBioDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ParamBioDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
