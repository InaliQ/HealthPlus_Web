

export interface PersonaEnfermero {
    idEnfermero?: number;
    nombre: string;
    primerApellido: string;
    segundoApellido: string;
    telefono: string;
    fechaNacimiento: string;
    calle: string;
    numero: string;
    codigoPostal: string;
    colonia: string;
    titulo: string;
    numEnfermero: string;
    idHorario: number;
    horaInicio?: Date;
    horaFin?: Date;
    showInfo?: boolean;
    isExpanded?: boolean;
}
