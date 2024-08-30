$(document).ready(function () {
    $('#componenteTabla').DataTable({ lengthChange: false, info: false });
});

function Delete(id) {
    if (confirm("¿Estás seguro de que deseas eliminar este componente?")) {
        $.ajax({
            type: "DELETE",
            url: '/Admin/ComponentesFisicos/Delete/' + id,
            success: function (response) {
                if (response.success) {
                    alert(response.message);
                    window.location.href = '/Admin/ComponentesFisicos/Index';
                } else {
                    alert(response.message);
                }
            },
            error: function () {
                alert("Error, este componente actualmente esta siendo utilizado en un servicio.");
            }
        });
    }
}