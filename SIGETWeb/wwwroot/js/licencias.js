$(document).ready(function () {
    $('#licenciaTabla').DataTable({ lengthChange: false, info: false });
});
function Delete(id) {
    if (confirm("¿Estás seguro de que deseas eliminar esta licencia?")) {
        $.ajax({
            type: "DELETE",
            url: '/Admin/Licencias/Delete/' + id,
            success: function (response) {
                if (response.success) {
                    alert(response.message);
                    window.location.href = '/Admin/Licencias/Index';
                } else {
                    alert(response.message);
                }
            },
            error: function () {
                alert("Ha ocurrido un error al intentar eliminar la licencia.");
            }
        });
    }
}