import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeagueUpdateComponent } from './league-update.component';

describe('LeagueUpdateComponent', () => {
  let component: LeagueUpdateComponent;
  let fixture: ComponentFixture<LeagueUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LeagueUpdateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LeagueUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
