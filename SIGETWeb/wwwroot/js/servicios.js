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

document.getElementById('componentType').addEventListener('change', function () {
    var selectedValue = this.value;
    var componentesFisicos = document.getElementById('componentesFisicos');
    var licencias = document.getElementById('licencias');

    if (selectedValue === 'fisico') {
        componentesFisicos.classList.remove('d-none');
        licencias.classList.add('d-none');
        document.getElementById('licencias').querySelector('select').value = '';
    } else if (selectedValue === 'licencia') {
        licencias.classList.remove('d-none');
        componentesFisicos.classList.add('d-none');
        document.getElementById('componentesFisicos').querySelector('select').value = '';
    } else {
        componentesFisicos.classList.add('d-none');
        licencias.classList.add('d-none');
    }
});




