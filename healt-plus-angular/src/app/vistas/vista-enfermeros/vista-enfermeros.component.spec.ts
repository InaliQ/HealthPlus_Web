import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VistaEnfermerosComponent } from './vista-enfermeros.component';

describe('VistaEnfermerosComponent', () => {
  let component: VistaEnfermerosComponent;
  let fixture: ComponentFixture<VistaEnfermerosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [VistaEnfermerosComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VistaEnfermerosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
