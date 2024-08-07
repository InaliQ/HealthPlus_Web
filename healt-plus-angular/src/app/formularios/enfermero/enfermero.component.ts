import { Component } from '@angular/core';
import { IPersona } from '../../interfaces/persona';
import { NgForOfContext } from '@angular/common';
import { IEnfermero } from '../../interfaces/enfermero';
import { IHorario } from '../../interfaces/horario';
import { PersonaEnfermero } from '../../interfaces/PersonaEnfermero';
import Swal from 'sweetalert2';
import { IPersonaPaciente } from '../../interfaces/PersonaPaciente';
import { EnfermerosService } from '../../services/enfermeros.service';
import { AlertasService } from '../../services/alertas.service';
import { HorariosService } from '../../services/horarios.service';

@Component({
  selector: 'app-enfermero',
  templateUrl: './enfermero.component.html',
  styleUrl: './enfermero.component.css'
})
export class EnfermeroComponent {
  isEdit = false;
  formulario = false;

  listaPersonas: IPersona[] = [];
  listaHorarios: IHorario[] = [];
  isResultadoLoaded = false;

  //persona
  idEnfermero=0;
  nombre= '';
  apaterno= '';
  amaterno= '';
  telefono= '';
  fecha_nac='';
  calle='';
  numero='';
  cp='';
  colonia='';
  //enfermero
  titulo='';
  numEnfermero='';
  idHorarioo = 0;

  changeHorario(e: Event) {
    const target = e.target as HTMLSelectElement;
    this.idHorarioo = Number(target.value);
    console.log(this.idHorarioo, 'id');
  }

  constructor(private _servicio: EnfermerosService, private _servicioA: AlertasService,
    private _servicioH: HorariosService
  ){
    this.cargarHorarios();
  }

  ngOnInit() {
    const enfermero = this._servicio.getEnfermero();
    if (enfermero) {
      this.idEnfermero = enfermero.idEnfermero ?? 0;
      this.nombre = enfermero.nombre;
      this.apaterno = enfermero.primerApellido;
      this.amaterno = enfermero.segundoApellido;
      this.telefono = enfermero.telefono;
      this.fecha_nac = enfermero.fechaNacimiento;
      this.calle = enfermero.calle;
      this.numero = enfermero.numero;
      this.cp = enfermero.codigoPostal;
      this.colonia = enfermero.colonia;
      this.numEnfermero = enfermero.numEnfermero;
      this.titulo = enfermero.titulo;
      this.idHorarioo = enfermero.idHorario;
      this.isEdit = true;
    }
  }


  guardarOEditarEnfermero() {
    this.formulario = true;
    if (this.isEdit) {
      this._servicioA.confirmarEditar('Se guardaran los cambios').then((result) => {
        if (result.isConfirmed) {
          this.editarEnfermero();
        }
      });
    } else {
      this.agregarEnfermero();
    }
  }
  

  cargarHorarios() {
    this._servicioH.horarios().subscribe({
      next: (data) => {
        this.listaHorarios = data;
        this.isResultadoLoaded = true;
        console.log('Horarios:', this.listaHorarios);
      },
      error: (e) => {
        console.log(e);
      }
    });
  }

  agregarEnfermero() {
    const numEnfermeroGenerado = this.generarNumEnfermero(this.nombre);

    const fecha = new Date(this.fecha_nac);
    const fechaNacimiento = fecha.toISOString().split('T')[0]; // Convertir a formato yyyy-MM-dd
  
    const nuevoEnfermero: PersonaEnfermero = {
      nombre: this.nombre,
      primerApellido: this.apaterno,
      segundoApellido: this.amaterno,
      telefono: this.telefono,
      fechaNacimiento: fechaNacimiento,
      calle: this.calle,
      numero: this.numero,
      codigoPostal: this.cp,
      colonia: this.colonia,
      titulo: this.titulo,
      numEnfermero: numEnfermeroGenerado,
      idHorario: this.idHorarioo,
    };

    console.log('Datos a enviar:', nuevoEnfermero);
    this._servicio.agregarEnfermero(nuevoEnfermero).subscribe({
      next: (response) => {
        console.log(response);
        this._servicioA.success('Enfermero agregado');
        this.limpiar();
      },
      error: (e) => {
        console.error('Error al agregar enfermero:', e);
      }
    });
  }

  editarEnfermero() {
    const fecha = new Date(this.fecha_nac);
    const fechaNacimiento = fecha.toISOString().split('T')[0]; // Convertir a formato yyyy-MM-dd
  
    const enfermeroEditado: PersonaEnfermero = {
      idEnfermero: this.idEnfermero,
      nombre: this.nombre,
      primerApellido: this.apaterno,
      segundoApellido: this.amaterno,
      telefono: this.telefono,
      fechaNacimiento: fechaNacimiento,
      calle: this.calle,
      numero: this.numero,
      codigoPostal: this.cp,
      colonia: this.colonia,
      numEnfermero: this.numEnfermero,
      titulo: this.titulo,
      idHorario: this.idHorarioo
    };

    console.log('Datos a enviar para edición:', enfermeroEditado);

    this._servicio.modificarEnfermero(enfermeroEditado).subscribe({
      next: (response) => {
        console.log('Paciente editado:', response);
        this._servicioA.success('Enfermero modificado');
        this.limpiar();
      },
      error: (e) => {
        console.error('Error al editar paciente:', e);
      }
    });
  }

  generarNumEnfermero(nombre: string): string {
    const now = new Date();
    const horas = now.getHours().toString().padStart(2, '0');
    const min = now.getMinutes().toString().padStart(2, '0');
    const seg = now.getSeconds().toString().padStart(2, '0');
    const primerasDos = nombre.slice(0, 2).toUpperCase();
    const ultima = nombre.slice(-1).toUpperCase();
    
    return `E${primerasDos}${horas}${min}${seg}${ultima}`;
  }

  limpiar() {
    this.isEdit = false;
    this.formulario = false;
    this.idEnfermero=0;
    this.nombre= '';
    this.apaterno= '';
    this.amaterno= '';
    this.telefono= '';
    this.fecha_nac='';
    this.calle='';
    this.numero='';
    this.cp='';
    this.colonia='';
    //enfermero
    this.titulo='';
    this.numEnfermero='';
    this.idHorarioo = 0;
  }


  get ValidarNombre() {
    return this.formulario && this.nombre.trim().length === 0;
  }

  get ValidarpApellido() {
    return this.formulario && this.apaterno.trim().length === 0;
  }
  get ValidarsApellido() {
    return this.formulario && this.amaterno.trim().length === 0;
  }
  get ValidarFecha() {
    return this.formulario && this.fecha_nac.trim().length === 0;
  }

  validarNumeros(event: KeyboardEvent) {
    const charCode = event.charCode;
    if (charCode < 48 || charCode > 57) {
      event.preventDefault(); 
    }
  }  
  get ValidarTel() {
    return this.formulario && this.telefono.trim().length === 0;
  }
  get ValidarLongTel() {
    return this.formulario && this.telefono.length !== 10;
  }
  
  
  get ValidarCalle() {
    return this.formulario && this.calle.trim().length === 0;
  }
  get ValidarNum() {
    return this.formulario && this.numero.trim().length === 0;
  }
  get ValidarCol() {
    return this.formulario && this.colonia.trim().length === 0;
  }

  get ValidarCP() {
    return this.formulario && this.cp.trim().length === 0;
  }
  get ValidarLongCP() {
    return this.formulario && this.cp.length !== 5;
  }

  get ValidarTitulo() {
    return this.formulario && this.titulo.trim().length === 0;
  }
  
  
}


