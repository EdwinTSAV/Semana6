function saludar() {
    return "Hola Mundo"
}

console.log(saludar());
alert("Como estas");

//console.log //-> imprime cosas en la consola de java en google
//document me ayuda a buscar los elementos:
//console.log(document.getElementById("link"));//se usa cuando vas a apuntar a un unico elemento, sirve con "id", el cual no se repite

//console.log(document.getElementsByTagName("table")); //se usa cuando vas a apuntar al nombre de etiqueta, muchas etiquetas "table  a. . ."

//console.log(document.getElementsByClassName("link"));//se usa cuando vas a apuntar a elementos con clase "link", sirve con "class", el cual se puede repetir

//console.log(document.querySelectorAll("a.link"));// busca por un selector css, aprender eso que dejo de trabajo en clases :v

//con jQuery: console.log(jQuery) se usa jQuery o $
//console.log($('#link')[0]) // busca por id con numeral, algo mas corto xde -> el [0] es para que retorne lo mismo, porque retornaba en un formato creo xde

// se puede conbinar eventos :D
// cuando haga clink en el link o boton con id link me sale eso:
$('#link').click(function () {
    alert("Hola Mundo");
});