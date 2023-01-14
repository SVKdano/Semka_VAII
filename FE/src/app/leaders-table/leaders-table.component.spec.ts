import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeadersTableComponent } from './leaders-table.component';

describe('LeadersTableComponent', () => {
  let component: LeadersTableComponent;
  let fixture: ComponentFixture<LeadersTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LeadersTableComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LeadersTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
