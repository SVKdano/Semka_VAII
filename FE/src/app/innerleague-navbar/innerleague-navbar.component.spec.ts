import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InnerleagueNavbarComponent } from './innerleague-navbar.component';

describe('InnerleagueNavbarComponent', () => {
  let component: InnerleagueNavbarComponent;
  let fixture: ComponentFixture<InnerleagueNavbarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InnerleagueNavbarComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InnerleagueNavbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
