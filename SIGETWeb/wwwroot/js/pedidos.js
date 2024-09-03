$(document).ready(function () {
    $('#pedidosTabla').DataTable({ lengthChange: false, info: false });
});
function Delete(id) {
    if (confirm("¿Estás seguro de que deseas eliminar este pedido?")) {
        $.ajax({
            type: "DELETE",
            url: '/Admin/Pedidos/Delete/' + id,
            success: function (response) {
                if (response.success) {
                    alert(response.message);
                    window.location.href = '/Admin/Pedidos/Index';
                } else {
                    alert(response.message);
                }
            },
            error: function () {
                alert("Ha ocurrido un error al intentar eliminar el pedido.");
            }
        });
    }
}