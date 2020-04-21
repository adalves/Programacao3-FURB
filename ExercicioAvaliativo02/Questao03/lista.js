// Ana Carolina Alves

var lista = ["Placas", "Revistas", "Guardanapo", "Cinco", "Foguete", "Abacaxi", "Informador"];

var body = document.getElementsByTagName("body")[0];
var title = document.createElement("p");
title.innerHTML = "Lista que eu peguei de um gerador de palavras aleatórias";
body.appendChild(title);

var ul = document.createElement("ul");
for (i = 0; i < lista.length; i++) {
    var li = document.createElement("li");
    li.innerHTML = lista[i];
    ul.appendChild(li);
}
body.appendChild(ul);