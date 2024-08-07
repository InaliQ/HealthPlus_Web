import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PadecimientoComponent } from './padecimiento.component';

describe('PadecimientoComponent', () => {
  let component: PadecimientoComponent;
  let fixture: ComponentFixture<PadecimientoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PadecimientoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PadecimientoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
