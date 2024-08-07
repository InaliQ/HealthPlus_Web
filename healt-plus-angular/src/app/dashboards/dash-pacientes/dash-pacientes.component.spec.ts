import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DashPacientesComponent } from './dash-pacientes.component';

describe('DashPacientesComponent', () => {
  let component: DashPacientesComponent;
  let fixture: ComponentFixture<DashPacientesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DashPacientesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DashPacientesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
