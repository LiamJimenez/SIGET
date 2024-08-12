document.addEventListener('DOMContentLoaded', function () {
    
    table = $('#MiTabla').DataTable({
        paging: false
    });

    table.destroy();

    table = $('#MiTabla').DataTable({
        searching: false
    });