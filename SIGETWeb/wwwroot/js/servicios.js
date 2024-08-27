$(document).ready(function () {
    $('#serviciosTabla').DataTable({ lengthChange: false, info: false });
});
function Delete(id) {
    if (confirm("¿Estás seguro de que deseas eliminar este servicio?")) {
        $.ajax({
            type: "DELETE",
            url: '/Admin/Servicios/Delete/' + id,
            success: function (response) {
                if (response.success) {
                    alert(response.message);
                    window.location.href = '/Admin/Servicios/Index';
                } else {
                    alert(response.message);
                }
            },
            error: function () {
                alert("Ha ocurrido un error al intentar eliminar el servicio.");
            }
        });
    }
}