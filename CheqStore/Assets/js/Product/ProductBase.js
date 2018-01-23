$(document).ready(function () {

    //funcion para cuando estamos creando.
    function imgInCreate() {
        document.getElementById("fileProductCreate").onchange = function (e) {
            // Creamos el objeto de la clase FileReader
            let reader = new FileReader();

            // Leemos el archivo subido y se lo pasamos a nuestro fileReader
            reader.readAsDataURL(e.target.files[0]);

            // Le decimos que cuando este listo ejecute el código interno
            reader.onload = function () {
                let preview = document.getElementById('preview'),
                        image = document.createElement('img');

                image.src = reader.result;
                image.className = "materialboxed";
                image.id = "PathPhoto";
                image.style.margin = "0 auto";

                preview.innerHTML = '';
                preview.append(image);

                $('.materialboxed').materialbox();

            };

        }
    }

    //Funcion para cuando editamos
    function imgInEdit() {
        document.getElementById("fileProductEdit").onchange = function (e) {

            // Creamos el objeto de la clase FileReader
            let reader = new FileReader();

            // Leemos el archivo subido y se lo pasamos a nuestro fileReader
            reader.readAsDataURL(e.target.files[0]);

            // Le decimos que cuando este listo ejecute el código interno
            reader.onload = function () {
                let preview = document.getElementById('preview'),
                image = document.getElementById("PathPhoto");
                image.remove();

                var imagenCreate = document.createElement("img");

                imagenCreate.src = reader.result;

                preview.innerHTML = '';
                preview.append(imagenCreate);

                imagenCreate.className = "materialboxed";
                imagenCreate.id = "PathPhoto";
                imagenCreate.style.margin = "0 auto";


                $('.materialboxed').materialbox();

            };

        }
    }

    if ($("#fileProductCreate").length) { //Detecto si estoy en create.
        imgInCreate();
        $('textarea#textarea1').characterCounter();
    }

    if($("#fileProductEdit").length){ //Detecto si estoy en Edit.

        imgInEdit();
        $('textarea#textarea1').characterCounter();
    }
});