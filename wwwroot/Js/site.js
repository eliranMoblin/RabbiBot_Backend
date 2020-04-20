function TestDataTablesAdd(table) {
    $(document).ready(function () {
        var tables=$(table).DataTable();

        $('table tbody').on('click', 'tr', function () {
           
        });
    });
}


function TestDataTablesRemove(table) {
    $('table').DataTable().destroy();
    
}

function HiedRow(id) {
    console.log(id);

    $('#' + id + '').hide();

    var tables = $('table').DataTable();
    var child = tables.row(this).child;

    if (child.isShown()) {
        child.hide();
    }
    else {
        child.show();
    }
}



//function TestDataTablesRemove(table) {
//    var elem = document.querySelector(table + '_wrapper');
//    elem.parentNode.removeChild(elem);
//    $(document).ready(function () {
//        $(table).DataTable().destroy();
//    });

//    window.TestDataTablesRemove = () => {
//        alert();
//        $(document).ready(function() {

//            $(table).DataTable().destroy();
//        });
//    };
