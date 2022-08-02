import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditQuickconsultDialogComponent } from './edit-quickconsult-dialog.component';

describe('EditQuickconsultDialogComponent', () => {
  let component: EditQuickconsultDialogComponent;
  let fixture: ComponentFixture<EditQuickconsultDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditQuickconsultDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditQuickconsultDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
