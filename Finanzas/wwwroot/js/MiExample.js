/*function saludar() {
    return "Hola Mundo"
}

console.log(saludar());
alert("Como estas"); // muestra algo en la pantalla
*/
//console.log //-> imprime cosas en la consola de java en google
//document me ayuda a buscar los elementos:
//console.log(document.getElementById("link"));//se usa cuando vas a apuntar a un unico elemento, sirve con "id", el cual no se repite

//console.log(document.getElementsByTagName("table")); //se usa cuando vas a apuntar al nombre de etiqueta, muchas etiquetas "table  a. . ."

//console.log(document.getElementsByClassName("link"));//se usa cuando vas a apuntar a elementos con clase "link", sirve con "class", el cual se puede repetir

//console.log(document.querySelectorAll("a.link"));// busca por un selector css, aprender eso que dejo de trabajo en clases :v

//con jQuery: console.log(jQuery) se usa jQuery o $
//console.log($('#link')[0]) // busca por id con numeral, algo mas corto xde -> el [0] es para que retorne lo mismo, porque retornaba en un formato creo xde
// .link -> clases  a -> etiqueta
// se puede conbinar eventos :D
// cuando haga clink en el link o boton con id link me sale eso:
// esto me hace un evento y cuando doy en ok redirecciona al link
/*$('#link').click(function () {
    alert("Hola Mundo");
});*/
// evito que redireccione al link, o su comportamiento por defecto con esto:
$('.botonElim').click(function (event) {
    alert("De aca no pasas ;-;");
    event.preventDefault();
});

//otra forma de llamar a un evento asi como el de arriba, ir a html y 
// en el elemento se coloca atributo onClick, es mas facil
// pero si quieres que haga algo entonces, debes de mandar desde el html el parametro (evento)
function saludoLove(a) {
    alert("Watashi wa, anata o aishiteimasu");
    a.preventDefault();
}
/*
$('#tabla').click(function () {
    alert("Hice click en la tabla de tu mujer :'v");
});*/

//con attr se puede modificar sus elementos:
$tabla = $('#tabla')
$tabla.attr("data-id", "valor nuevo"); //se puede añadir cualquier elemento!

// Aprender ajax
//Ajax son peticiones http
// necesitan de una url y un metodo, get o post xde
// Ajax es asincrono
$('#boton').click(function (event) {
    var respv = $.ajax({
        url: "https://localhost:44315/Crud",
        type: "get",
    })

    respv.done(function (html) {
        //desde recargar una pagina hasta hacer eso del select
        //aca se coloca todo lo que queres que haga, en mi caso
        //pues aun no, en el siguiente example xde
    })

    event.preventDefault();
});
