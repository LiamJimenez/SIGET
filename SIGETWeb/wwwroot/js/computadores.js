$(document).ready(function () {
    $('#computadorTabla').DataTable({ lengthChange: false, info: false });
});

function Delete(id) {
    if (confirm("¿Estás seguro de que deseas eliminar este computadores?")) {
        $.ajax({
            type: "DELETE",
            url: '/Admin/Computadores/Delete/' + id,
            success: function (response) {
                if (response.success) {
                    alert(response.message);
                    window.location.href = '/Admin/Computadores/Index';
                } else {
                    alert(response.message);
                }
            },
            error: function () {
                alert("Ha ocurrido un error al intentar eliminar el computadores.");
            }
        });
    }
}

