$(document).ready(function () {

    

    $('#submit-button').click(function (event) {
        event.preventDefault(); // Prevenir el comportamiento por defecto del botón

        // Capturar los datos del formulario de Cliente
        var nombre = $('#nombre').val();
        var primerApellido = $('#primerApellido').val();
        var segundoApellido = $('#segundoApellido').val();
        var telefono = $('#telefono').val();
        var calle = $('#calle').val();
        var numero = $('#numero').val();
        var codigoPostal = $('#codigoPostal').val();
        var colonia = $('#colonia').val();
        var usuario = $('#usuario').val();
        console.log(usuario);
        var contrasenia = $('#contrasenia').val();
        var estatusUsuario = $('#estatusUsuario').val() === 'true';

        // Capturar los datos del formulario de Servicio
        var fechaInicio = $('#fechaInicio').val();
        var fechaFin = $('#fechaFin').val();
        var precio = parseFloat($('#precio').val());
        var fechaPago = $('#fechaPago').val();
        var estatusServicio = $('#estatusServicio').val() === 'true';

        // Capturar los datos del formulario de Lote de Producto
        var cantidad = parseInt($('#cantidad').val());
        var modelo = $('#modelo').val();
        var precioLote = parseFloat($('#precioLote').val());

        // Formar el objeto de datos en el formato esperado por la API
        var data = {
            nombre: nombre,
            primerApellido: primerApellido,
            segundoApellido: segundoApellido,
            telefono: telefono,
            calle: calle,
            numero: numero,
            codigoPostal: codigoPostal,
            colonia: colonia,
            usuario: usuario,
            contrasenia: contrasenia,
            estatusUsuario: estatusUsuario,
            fechaInicio: fechaInicio,
            fechaFin: fechaFin,
            precio: precio,
            fechaPago: fechaPago,
            estatusServicio: estatusServicio,
            precioLote: precioLote,
            cantidad: cantidad,
            modelo: modelo
        };

        // Enviar los datos mediante AJAX
        $.ajax({
            url: 'http://localhost:5140/api/Healt/CotizadorCliente',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            success: function (response) {
                console.log('Respuesta del servidor:', response);
                if (response.isSuccess) {
                    Swal.fire({
                        title: 'Cliente registrado exitosamente',
                        icon: 'success',
                        showConfirmButton: false,
                        timer: 2000
                    }).then(() => {
                    });
                } else {
                    Swal.fire({
                        title: 'Error',
                        text: 'Ocurrió un error al registrar el cliente',
                        icon: 'error',
                        showConfirmButton: false,
                        timer: 3000
                    });
                }
            },
            error: function (error) {
                // Manejar los errores
                console.error('Error al enviar los datos:', error);
            }
        });
    });

    $.ajax({
        url: 'http://localhost:5140/api/Healt/ObtenerInformacionServicio',
        type: 'GET',
        success: function(response) {
            console.log('Respuesta del servidor:', response); // Muestra la respuesta en la consola
            var services = response.$values; // Accede al array de servicios en $values
            var tableBody = $('#servicesTableBody');
            tableBody.empty(); // Vacía la tabla antes de agregar nuevas filas
            
            services.forEach(function (service) {
                var editButton = service.estatusServicio
                    ? `<button class="button is-primary is-small" onclick="editService(${service.idServicio})">Editar</button>`
                    : `<button class="button is-primary is-small is-disabled" disabled>Editar</button>`;
    
                var deleteButton = service.estatusServicio
                    ? `<button class="button is-danger is-small" onclick="deleteService(${service.idServicio})">Eliminar</button>`
                    : `<button class="button is-danger is-small is-disabled" disabled>Eliminar</button>`;
    
                var row = `<tr>
                    <td>${service.idServicio}</td>
                    <td>${service.fechaInicio}</td>
                    <td>${service.fechaFin}</td>
                    <td>${service.precioServicio}</td>
                    <td>${service.fechaPago}</td>
                    <td>${service.estatusServicio ? "Activo" : "Inactivo"}</td>
                    <td>
                        ${deleteButton}
                    </td>
                </tr>`;
                tableBody.append(row);
            });
        },
        error: function (error) {
            console.error('Error al obtener los servicios:', error);
        }
    });
    
    
});

function deleteService(idServicio) {
    $.ajax({
        url: 'http://localhost:5140/api/Healt/CambiarEstatusServicio',
        type: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify({
            idServicio: idServicio,
            nuevoEstatus: false
        }),
        success: function(response) {
            console.log('Respuesta del servidor:', response);
            // Opcional: Actualiza la tabla o realiza otras acciones después de la eliminación
            $('#servicesTableBody').empty(); // Vacía la tabla antes de volver a cargar los datos
            $.ajax({
                url: 'http://localhost:5140/api/Healt/ObtenerInformacionServicio',
                type: 'GET',
                success: function(response) {
                    console.log('Respuesta del servidor:', response); // Muestra la respuesta en la consola
                    var services = response.$values; // Accede al array de servicios en $values
                    var tableBody = $('#servicesTableBody');
                    tableBody.empty(); // Vacía la tabla antes de agregar nuevas filas
                    
                    services.forEach(function (service) {
                        var editButton = service.estatusServicio
                            ? `<button class="button is-primary is-small" onclick="editService(${service.idServicio})">Editar</button>`
                            : `<button class="button is-primary is-small is-disabled" disabled>Editar</button>`;
            
                        var deleteButton = service.estatusServicio
                            ? `<button class="button is-danger is-small" onclick="deleteService(${service.idServicio})">Eliminar</button>`
                            : `<button class="button is-danger is-small is-disabled" disabled>Eliminar</button>`;
            
                        var row = `<tr>
                            <td>${service.idServicio}</td>
                            <td>${service.fechaInicio}</td>
                            <td>${service.fechaFin}</td>
                            <td>${service.precioServicio}</td>
                            <td>${service.fechaPago}</td>
                            <td>${service.estatusServicio ? "Activo" : "Inactivo"}</td>
                            <td>
                                ${deleteButton}
                            </td>
                        </tr>`;
                        tableBody.append(row);
                    });
                },
                error: function (error) {
                    console.error('Error al obtener los servicios:', error);
                }
            });
        },
        error: function(error) {
            console.error('Error al eliminar el servicio:', error);
        }
    });
}

function editService(id) {
    console.log('Editando servicio con Id:', id);
    // Lógica para editar el servicio
}