$(document).ready(function () {
    $('#login').click(function () {
        var user = $('#user').val();
        var password = $('#password').val();
        console.log(user);
        console.log(password);
        if (user != '' && password != '') {
            $.ajax({
                url: 'http://localhost:5140/api/Healt/loginService',
                type: 'post',
                contentType: 'application/json', // Indica que se está enviando JSON
                data: JSON.stringify({ // Convierte el objeto JavaScript a una cadena JSON
                    user: user,
                    contrasenia: password
                }),
                success: function (response) {
                    console.log(response);
                    if (response.isSuccess == true) {
                        if (response.rol == 'CA') {
                            Swal.fire({
                                title: 'Bienvenido',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 2000
                            }).then(() => {
                                window.location.href = 'modules/home_ac/'; // Cambia esta URL a la página a la que deseas redirigir
                                localStorage.setItem('user', user);
                            });
                        }
                        else if (response.rol == 'AS') {
                            Swal.fire({
                                title: 'Bienvenido',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 2000
                            }).then(() => {
                                window.location.href = 'modules/home_as/'; // Cambia esta URL a la página a la que deseas redirigir
                                localStorage.setItem('user', user);
                            });
                        }
                    } else {
                        Swal.fire({
                            title: 'Error',
                            text: 'Usuario o contraseña incorrectos',
                            icon: 'error',
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
