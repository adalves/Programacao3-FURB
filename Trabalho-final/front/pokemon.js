//Ana Carolina Alves

$(document).ready(function(){
    getAllPokemon();
});

function getAllPokemon() {
    $.ajax({
        dataType:"json",
        type:"GET",
        url: "https://localhost:5001/api/pokemon/",
        contentType:"application/json; charset=utf-8"
    })
    .done(function(data) {
        updateTable(data);
    })
    .fail(function(status) {
        alert("Error: " + status.status);
    });
}

function updateTable(data) {
    resetTable();
    console.log(data);
    pkmn = data
    var text = "";
    for (i = 0; i < pkmn.length; i++) {
        var editBtn = "<button type=\"button\" class=\"btn btn btn-link\" data-toggle=\"modal\" data-target=\"#modalEdit\" data-recipient="+ pkmn[i].id +">edit</button>";
        var deleteBtn = "<button type=\"button\" class=\"btn btn btn-link\" data-toggle=\"modal\" data-target=\"#modalDelete\" data-recipient="+ pkmn[i].id +">delete</button>";
        var pkmnLink = "<button type=\"button\" class=\"btn btn btn-link\" data-toggle=\"modal\" data-target=\"#modalPokemon\" data-recipient="+ pkmn[i].id +">" + pkmn[i].name + "</button>";
        var type2 = pkmn[i].type2 == "(???)" ? "-" : pkmn[i].type2;
        text += "<tr>"
            + "<td>" + pkmn[i].id + "</td>" 
            + "<td>" + pkmnLink + "</td>" 
            + "<td>" + pkmn[i].type1 + "</td>" 
            + "<td>" + type2+ "</td>" 
            + "<td>" + editBtn + " / " + deleteBtn + " </td>" 
            + "</tr>";
    }
    $(".table-rows").html(text);
}

function resetTable(){
    $(".table-rows").html("");
}

function addRow(pkmn) {
    $(".table-rows").html(function(i, origText){
        return origText + "<tr>"
        + "<td>" + pkmn.id + "</td>" 
        + "<td>" + pkmn.name + "</td>" 
        + "<td>" + pkmn.type1 + "</td>" 
        + "<td>" + pkmn.type2 + "</td>" 
        + "</tr>";
      });
}

$("#modalBtn").click(function(){
    var values = $("#addForm").serializeArray();
    var values_array = {};

    $.map(values, function(n, i){
        values_array[n['name']] = n['value'];
    });
    console.log(values_array);
    $.ajax({
        url:"https://localhost:5001/api/pokemon",
        type:"POST",
        data:JSON.stringify(values_array),
        contentType:"application/json; charset=utf-8",
        dataType:"json"
    })
    .done(function( data ) {
        console.log(data);
        getAllPokemon();
    })
    .fail(function(status) {
        alert("Error: " + status.status);
    });
});

$('#modalEdit').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget);
    var recipient = button.data('recipient');
    var modal = $(this);

    $.get("https://localhost:5001/api/pokemon/" + recipient)
    .done(function(data) {
        pkmn = data;
        modal.find("#idInput").val(pkmn.id);
        modal.find("#nameInput").val(pkmn.name);
        modal.find("#type1Input").val(pkmn.type1);
        modal.find("#type2Input").val(pkmn.type2);
    })
    .fail(function(status) {
        alert("Error: " + status.status);
    });
});

$("#modalEditBtn").click(function(){
    var values = $("#editForm").serializeArray();
    var values_array = {};

    $.map(values, function(n, i){
        values_array[n['name']] = n['value'];
    });
    console.log(values_array);
    $.ajax({
        url:"https://localhost:5001/api/pokemon/" + values_array.id,
        type:"PUT",
        data:JSON.stringify(values_array),
        contentType:"application/json; charset=utf-8",
        dataType:"json"
    })
    .done(function( data ) {
        console.log(data);
        getAllPokemon();
    })
    .fail(function(status) {
        alert("Error: " + status.status);
    });
});

$('#modalPokemon').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget);
    var recipient = button.data('recipient');
    var modal = $(this);
    getPokemon(recipient, modal);
    getMoves(recipient, modal);
    getTypeEffectiveness(recipient, modal);
});

function getPokemon(id, modal) {    
    $.ajax({
        dataType:"json",
        type:"GET",
        url: "https://localhost:5001/api/pokemon/" + id,
        contentType:"application/json; charset=utf-8"
    })
    .done(function(data) {         
        console.log(data);
        modal.find('.modal-title').text(data.name);
        var types = data.type1;
        if (data.type2 != "(???)") {
            types += " | " + data.type2
        }  
        console.log(data.name);
        modal.find('#general-info').html(data.name + "<br>#" + data.id + "<br>" + types);
    })
    .fail(function(status) {
        alert("Error: " + status.status);
    });
}

function getMoves(id, modal) {
    $.ajax({
        dataType:"json",
        type:"GET",
        url: "https://localhost:5001/api/pokemon/" + id + "/moves",
        contentType:"application/json; charset=utf-8"
    })
    .done(function(data) {      
        console.log(data);
        modal.find('#moves').html("");
        data.forEach(move => {
            modal.find('#moves').html(function(i, origText){
                return origText + "<br>"
                + "Move: " + move.name + "<br>" 
                + "Type: " + move.type + "<br>" 
                + "Category: " + move.category + "<br>" 
                + "Power: " + move.power + "<br>" 
                + "Accuracy: " + move.accuracy + "<br>"
                + "PP: " + move.pp + "<br>";
              });
        });
    })
    .fail(function(status) {
        alert("Error: " + status.status);
    });
}

function getTypeEffectiveness(id, modal) {
    $.ajax({
        dataType:"json",
        type:"GET",
        url: "https://localhost:5001/api/pokemon/" + id + "/types",
        contentType:"application/json; charset=utf-8"
    })
    .done(function(data) {      
        console.log(data);
        modal.find('#type-effectiveness').html("");
        data.forEach(type => {
            modal.find('#type-effectiveness').html(function(i, origText){
                return origText + "<br>"
                + type.type + ": " + type.effectiveness;
              });
        });
    })
    .fail(function(status) {
        alert("Error: " + status.status);
    });
}

$('#modalDelete').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget);
    var recipient = button.data('recipient');
    var modal = $(this);
    modal.find("#id").text(recipient);
});


$("#modalDeleteBtn").click(function(){
    var id = $('#id').text();
    $.ajax({
        url:"https://localhost:5001/api/pokemon/" + id,
        type:"DELETE"
    })
    .done(function( data ) {
        console.log(data);
        getAllPokemon();
    })
    .fail(function(status) {
        alert("Error: " + status.status);
    });
});