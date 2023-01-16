import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LigaSestComponent } from './liga-sest.component';

describe('LigaSestComponent', () => {
  let component: LigaSestComponent;
  let fixture: ComponentFixture<LigaSestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LigaSestComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LigaSestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
