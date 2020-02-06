$(document).ready(function () {

    $("#registrar").click(function () {

        $("#cont ").fadeIn(function () {

            window.setTimeout(function () {
                $(".contenedor2").addClass('window-container-visible');
            }, 100);

        });
    });
        $("#login").click(function () {

            $("#cont1b ").fadeIn(function () {

                window.setTimeout(function () {
                    $(".contenedor2b").addClass('window-container-visible');
                }, 100);

            });
    });

  //VALIDACION DE LAS CONTRASEÑAS
    var cod1 = $('[name=contrasena]');
    var cod2 = $('[name=codigo]');
    var confirmacion = "Las contraseñas si coinciden";
    var negacion = "No coinciden las contraseñas";
    var vacio = "La contraseña no puede estar vacía";
    //oculto por defecto el elemento span
    var span = $('<span id="sp"></span>').insertAfter(cod2);
    span.hide();
 
        //función que comprueba las dos contraseñas
        function coincidePassword() {
            var valor1 = cod1.val();
            var valor2 = cod2.val();
            //muestro el span
            span.show().removeClass();
            //condiciones dentro de la función
            if (valor1 != valor2) {
                span.text(negacion).addClass('negacion');
            }
            if (valor1.length == 0 || valor1 == "") {
                span.text(vacio).addClass('negacion');
            }
            
            if (valor1.length != 0 && valor1 == valor2) {
                span.text(confirmacion).removeClass("negacion").addClass('confirmacion');
            }
        }
    //ejecuto la función al soltar la tecla
        cod2.keyup(function () {
            coincidePassword();

    });

    $("#enviar").click(function () {
        
       $('#cont ').fadeOut(2000).end().find('#contenedor2').removeClass('window-container-visible');
          
        });

});

	