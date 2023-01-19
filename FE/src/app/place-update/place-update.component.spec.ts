import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlaceUpdateComponent } from './place-update.component';

describe('PlaceUpdateComponent', () => {
  let component: PlaceUpdateComponent;
  let fixture: ComponentFixture<PlaceUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PlaceUpdateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PlaceUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
