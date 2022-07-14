import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GestionPatientComponent } from './gestion-patient.component';

describe('GestionPatientComponent', () => {
  let component: GestionPatientComponent;
  let fixture: ComponentFixture<GestionPatientComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GestionPatientComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GestionPatientComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
