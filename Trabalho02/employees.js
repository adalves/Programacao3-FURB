//Ana Carolina Alves

$(document).ready(function(){
    getEmployees();
});

function getEmployees() {
    $.get("http://rest-api-employees.jmborges.site/api/v1/employees")
    .done(function(data) {
        updateTable(data);
    })
    .fail(function(status) {
        alert("Erro " + status.status);
    });
}

function updateTable(data) {
    resetTable();
    emp = data.data
    for (i = 0; i < emp.length; i++) {
        addRow(emp[i]);
    }
}

function resetTable(){
    $(".table-rows").html("");
}

function addRow(emp) {
    var editBtn = "<button type=\"button\" class=\"btn btn btn-link\" data-toggle=\"modal\" data-target=\"#modal\" data-type=\"edit\" data-recipient="+ emp.id +">editar</button>";
    var deleteBtn = "<button type=\"button\" class=\"btn btn btn-link\" data-toggle=\"modal\" data-target=\"#modalDelete\" data-recipient="+ emp.id +">excluir</button>";
    $(".table-rows").html(function(i, origText){
        return origText + "<tr>"
        + "<td>" + emp.id + "</td>" 
        + "<td>" + emp.employee_name + "</td>" 
        + "<td>" + emp.employee_salary + "</td>" 
        + "<td>" + emp.employee_age + "</td>" 
        + "<td>" + emp.profile_image + "</td>" 
        + "<td>" + editBtn + " / " + deleteBtn + " </td>" 
        + "</tr>";
      });
}

$('#modal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget);
    var type = button.data('type');
    var recipient = button.data('recipient');
    
    if (recipient != "none") {
        $.get("http://rest-api-employees.jmborges.site/api/v1/employee/" + recipient)
            .done(function(data) {
                emp = data.data;
                $("#idInput").val(emp.id);
                $("#nameInput").val(emp.employee_name);
                $("#salaryInput").val(emp.employee_salary);
                $("#ageInput").val(emp.employee_age);
                $("#profileImgInput").val(emp.profile_image);
                $("#modalBtn").text("Editar");
            })
            .fail(function(status) {
                alert("Erro " + status.status);
        });
    } else {
        $("form").trigger("reset");
        $("#modalBtn").text("Criar");
    }
    var modal = $(this);
    var title = "Adicionar Novo Empregado"
    if (type != "new") {
        title = "Editar Dados de um Empregado";
    }
    modal.find('.modal-title').text(title);
});

$("#modalBtn").click(function(){
    var values = $("form").serializeArray();
    var values_array = {};

    $.map(values, function(n, i){
        values_array[n['name']] = n['value'];
    });
    console.log(values_array);

    var newEmp = $("#modalBtn").text() == "Criar";
    if (newEmp) {
        $.ajax({
            url:"http://rest-api-employees.jmborges.site/api/v1/create",
            type:"POST",
            data:JSON.stringify(values_array),
            contentType:"application/json; charset=utf-8",
            dataType:"json"
        })
        .done(function( data ) {
            console.log(data);
            getEmployees();
        })
        .fail(function(status) {
            alert("Erro " + status.status);
        });
    } else {
        $.ajax({
            url:"http://rest-api-employees.jmborges.site/api/v1/update/" + values_array.id,
            type:"PUT",
            data:JSON.stringify(values_array),
            contentType:"application/json; charset=utf-8",
            dataType:"json"
        })
        .done(function( data ) {
            console.log(data);
            getEmployees();
        })
        .fail(function(status) {
            alert("Erro " + status.status);
        });
    }
    console.log(JSON.stringify(values_array));
});


$('#modalDelete').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget);
    var recipient = button.data('recipient');
    var modal = $(this);
    modal.find("#id").text(recipient);
});


$("#modalDeleteBtn").click(function(){
    var id = $('#id').text();
    $.ajax({
        url:"http://rest-api-employees.jmborges.site/api/v1/delete/" + id,
        type:"DELETE"
    })
    .done(function( data ) {
        console.log(data);
        getEmployees();
    })
    .fail(function(status) {
        alert("Erro " + status.status);
    });
});