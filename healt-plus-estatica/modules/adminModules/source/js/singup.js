$(document).ready(function () {
    $('#singup').click(function () {
        var email = $('#email').val();
        var password = $('#password').val();
        var password_con = $('#password_con').val();

        if (email == '' || password == '' || password_con == '') {
            Swal.fire({
                title: 'Campos vacíos',
                text: 'Por favor, llene todos los campos',
                icon: 'error',
                confirmButtonText: 'Aceptar'
            });
            return;
        }



        if (!email.includes('@') || !email.includes('.')) {
            Swal.fire({
                title: 'Correo inválido',
                text: 'Por favor, ingrese un correo válido',
                icon: 'error',
                confirmButtonText: 'Aceptar'
            });
            return;
        }

        console.log(password);
        console.log(password_con);
        if (password != password_con) {
            Swal.fire({
                title: 'Las contraseñas no coinciden',
                text: 'Por favor, verifique que las contraseñas sean iguales',
                icon: 'error',
                confirmButtonText: 'Aceptar'
            });
            return;
        }

        if (email != '' && password != '') {
            $.ajax({
                url: 'http://localhost:5140/api/Healt/InsertarUsuarioCliente',
                type: 'post',
                contentType: 'application/json', // Indica que se está enviando JSON
                data: JSON.stringify({ // Convierte el objeto JavaScript a una cadena JSON
                    nombre: "CLIENTE",
                    primerApellido: "",
                    segundoApellido: "",
                    telefono: 505,
                    calle: "",
                    numero: "",
                    codigoPostal: "",
                    colonia: "",
                    usuario: email,
                    contrasenia: password,
                    estatusUsuario: true,
                    idServicio: 1,
                    estatusCliente: true
                }),
                success: function (response) {
                    console.log(response);
                    if (response.isSuccess == true) {
                        Swal.fire({
                            title: 'Registro exitoso',
                            text: 'Usuario registrado con éxito',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 3000
                        }).then(() => {
                            window.location.href = 'login.html'; // Cambia esta URL a la página a la que deseas redirigir
                        });

                    }
                },
                error: function (xhr, status, error) {
                    console.error('Error:', error);
                }
            });
        } else {
            var msg = 'All fields are required!';
            $('#message').html(msg);
        }
    });
});
