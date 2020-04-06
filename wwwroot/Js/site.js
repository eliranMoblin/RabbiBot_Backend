function TestDataTablesAdd(table) {
    $(document).ready(function () {
        $(table).DataTable();
    });
}


function TestDataTablesRemove(table) {
    $('table').DataTable().destroy();
    
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
