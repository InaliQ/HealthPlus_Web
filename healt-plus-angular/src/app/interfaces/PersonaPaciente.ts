export interface IPersonaPaciente {
    idPaciente?: number;
    nombre: string;
    primerApellido: string;
    segundoApellido: string;
    telefono: string;
    fechaNacimiento: string;
    calle: string;
    numero: string;
    codigoPostal: string;
    colonia: string;
    numPaciente: string;
    altura: string;
    peso: string;
    tipoSangre: string;
    ritmoMax: string;
    ritmoMin: string;
    estatus: boolean;
    idPadecimiento: number;
    nombrePadecimiento?: string;
    showInfo?: boolean;
    isExpanded?: boolean;
}
