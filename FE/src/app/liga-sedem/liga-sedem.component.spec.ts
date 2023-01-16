import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LigaSedemComponent } from './liga-sedem.component';

describe('LigaSedemComponent', () => {
  let component: LigaSedemComponent;
  let fixture: ComponentFixture<LigaSedemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LigaSedemComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LigaSedemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
