//Variables Declaration 

var button = $('.btnDelete');

button.on('click', function () {
    var ID = this.id; //Capturo el ID.
    var emergente = confirm("¿Está seguro de querer eliminar la orden con id " + ID + " ?");

    if (emergente) {
        DeleteOrder(ID);
    }
})


//Peticion Ajax //

function DeleteOrder(id) {
    $.ajax({
        url: '/Orders/Delete/' + id,
        type: "POST",
        success: function () {
            alert("Borrado exitoso");
            window.location.reload();
        },
        error: function (e) {
            alert("Problema al borrar!");
            console.log(e);
        },       
    });
}
