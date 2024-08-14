import { Component } from '@angular/core';
import { IPersonaPaciente } from '../../interfaces/PersonaPaciente';
import Swal from 'sweetalert2';
import { IPadecimientos } from '../../interfaces/padecimientos';
import { PacientesService } from '../../services/pacientes.service';
import { PadecimientosService } from '../../services/padecimientos.service';
import { AlertasService } from '../../services/alertas.service';

@Component({
  selector: 'app-paciente',
  templateUrl: './paciente.component.html',
  styleUrl: './paciente.component.css'
})
export class PacienteComponent {
  isEdit = false;
  formulario = false;

  //persona
  idPaciente= 0;
  nombre= '';
  apaterno= '';
  amaterno= '';
  telefono= '';
  fecha_nac='';
  calle='';
  numero='';
  cp='';
  colonia='';
  
  //paciente
  num_paciente='';
  altura='';
  peso='';
  tipo_sangre='';
  rit_min = '';
  rit_max = '';
  padecimientos=0;

  listaPadecimientos: IPadecimientos[] = [];

  constructor(private _servicio: PacientesService, private _serviceP: PadecimientosService,
    private _servicioA: AlertasService
  ){
    this.cargarPadecimientos();
  }

  generarNumPaciente(nombre: string): string {
    const now = new Date();
    const horas = now.getHours().toString().padStart(2, '0');
    const min = now.getMinutes().toString().padStart(2, '0');
    const seg = now.getSeconds().toString().padStart(2, '0');
    const primerasDos = nombre.slice(0, 2).toUpperCase();
    const ultima = nombre.slice(-1).toUpperCase();
    
    return `P${primerasDos}${horas}${min}${seg}${ultima}`;
  }

  ngOnInit() {
    const paciente = this._servicio.getPaciente();
    if (paciente) {
      this.idPaciente = paciente.idPaciente ?? 0;
      this.nombre = paciente.nombre;
      this.apaterno = paciente.primerApellido;
      this.amaterno = paciente.segundoApellido;
      this.telefono = paciente.telefono;
      this.fecha_nac = paciente.fechaNacimiento;
      this.calle = paciente.calle;
      this.numero = paciente.numero;
      this.cp = paciente.codigoPostal;
      this.colonia = paciente.colonia;
      this.num_paciente = paciente.numPaciente;
      this.altura = paciente.altura;
      this.peso = paciente.peso;
      this.tipo_sangre = paciente.tipoSangre;
      this.rit_min = paciente.ritmoMin;
      this.rit_max = paciente.ritmoMax;
      this.padecimientos = paciente.idPadecimiento;
      this.isEdit = true;
    }
  }

  cargarPadecimientos() {
    this._serviceP.getPadecimientos().subscribe({
      next: (data) => {
        this.listaPadecimientos = data;
      },
      error: (e) => {
        console.log(e);
      }
    });
  }

  guardarOEditarPaciente() {
    this.formulario = true;
    if (this.isEdit) {
      this.editarPaciente();
    } else {
      this.agregarPaciente();
    }
  }


  agregarPaciente() {
    console.log('Ritmo Min:', this.rit_min, 'Ritmo Max:', this.rit_max);
    const numPacienteGenerado = this.generarNumPaciente(this.nombre);
    const fecha = new Date(this.fecha_nac);
    const fechaNacimiento = fecha.toISOString().split('T')[0]; // Convertir a formato yyyy-MM-dd
  
    const nuevoPaciente: IPersonaPaciente = {
      nombre: this.nombre,
      primerApellido: this.apaterno,
      segundoApellido: this.amaterno,
      telefono: this.telefono,
      fechaNacimiento: fechaNacimiento,
      calle: this.calle,
      numero: this.numero,
      codigoPostal: this.cp,
      colonia: this.colonia,
      numPaciente: numPacienteGenerado,
      altura: this.altura,
      peso: this.peso,
      tipoSangre: this.tipo_sangre,
      ritmoMax: this.rit_max,
      ritmoMin: this.rit_min,
      estatus: true,
      idPadecimiento: this.padecimientos
    };
  
    console.log('Datos a enviar:', nuevoPaciente);
  
    this._servicio.agregarPaciente(nuevoPaciente).subscribe({
      next: (response) => {
        console.log('Paciente agregado:', response);
        this._servicioA.success('Paciente agregado');
        this.limpiar();
      },
      error: (e) => {
        console.error('Error al agregar paciente:', e);
      }
    });
  }
  
  editarPaciente() {
    console.log('Ritmo Min:', this.rit_min, 'Ritmo Max:', this.rit_max);
    const fecha = new Date(this.fecha_nac);
    const fechaNacimiento = fecha.toISOString().split('T')[0]; 

    const pacienteEditado: IPersonaPaciente = {
      idPaciente: this.idPaciente,
      nombre: this.nombre,
      primerApellido: this.apaterno,
      segundoApellido: this.amaterno,
      telefono: this.telefono,
      fechaNacimiento: fechaNacimiento,
      calle: this.calle,
      numero: this.numero,
      codigoPostal: this.cp,
      colonia: this.colonia,
      numPaciente: this.num_paciente, 
      altura: this.altura,
      peso: this.peso,
      tipoSangre: this.tipo_sangre,
      ritmoMax: this.rit_max,
      ritmoMin: this.rit_min,
      estatus: true,
      idPadecimiento: this.padecimientos
    };

    console.log('Datos a enviar para edición:', pacienteEditado);

    this._servicio.modificarPaciente(pacienteEditado).subscribe({
      next: (response) => {
        console.log('Paciente editado:', response);
        this._servicioA.success('Paciente modificado');
        this.limpiar();
      },
      error: (e) => {
        console.error('Error al editar paciente:', e);
      }
    });
  }

  limpiar() {
    this.isEdit = false;
    this.idPaciente=0;
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
    this.num_paciente='';
    this.altura='';
    this.peso = '';
    this.tipo_sangre = '';
    this.padecimientos = 0;
    this.rit_max = '';
    this.rit_min = '';
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

  get ValidarAltura() {
    return this.formulario && this.altura.trim().length === 0;
  }
  get ValidarPeso() {
    return this.formulario && this.peso.trim().length === 0;
  }
  get ValidarTipoS() {
    return this.formulario && this.tipo_sangre.trim().length === 0;
  }
 

}


