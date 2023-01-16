import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LigaPatComponent } from './liga-pat.component';

describe('LigaPatComponent', () => {
  let component: LigaPatComponent;
  let fixture: ComponentFixture<LigaPatComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LigaPatComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LigaPatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
