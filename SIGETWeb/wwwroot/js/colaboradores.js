function Delete(id) {
    if (confirm("¿Estás seguro de que deseas eliminar este colaborador?")) {
        $.ajax({
            type: "DELETE",
            url: '/Admin/Colaboradores/Delete/' + id,
            success: function (response) {
                if (response.success) {
                    alert(response.message);
                    window.location.href = '/Admin/Colaboradores/Index';
                } else {
                    alert(response.message);
                }
            },
            error: function () {
                alert("Ha ocurrido un error al intentar eliminar el colaborador.");
            }
        });
    }
}