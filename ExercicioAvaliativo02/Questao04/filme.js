// Ana Carolina Alves

var filmes = [new Filme("Joker", 2019, "Drama / Suspense Psicológico"),
        new Filme("The Man from Earth", 2007, "Drama / Ficção Científica"),
        new Filme("Spirited Away", 2001, "Aventura / Animação")];

var filmesHTML = document.getElementsByClassName("filme");

for (i = 0; i < filmesHTML.length; i++) {
    printFilme(filmes[i], filmesHTML[i]);
}

function Filme (titulo, ano, genero) {
    this.titulo = titulo;
    this.ano = ano;
    this.genero = genero;
}

function printFilme(filme, filmeHTML) {
    filmeHTML.getElementsByTagName("h1")[0].innerHTML = filme.titulo;
    filmeHTML.getElementsByTagName("h2")[0].innerHTML  = filme.ano;
    filmeHTML.getElementsByTagName("h2")[1].innerHTML = filme.genero;
}

