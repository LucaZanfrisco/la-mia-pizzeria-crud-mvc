// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let form = document.getElementById("nuovaPizza");

if (form !== null) {
    form.addEventListener("keyup", function () {
        let imgPath = document.getElementById("imgPath").value;
        let imgPreview = document.getElementById("imgPreview");
        imgPreview.src = imgPath;

        let nome = document.getElementById("nome").value;
        let nomePizza = document.getElementById("nomePizza");
        if (nome !== '') {
            nomePizza.innerText = nome;
        } else {
            nomePizza.innerText = 'Nome della pizza';
        }

        let ingredienti = document.getElementById("ingredienti").value;
        let listaIngredienti = document.getElementById("listaIngredienti");
        if (ingredienti !== '') {
            listaIngredienti.innerText = ingredienti;
        } else {
            listaIngredienti.innerText = 'Ingredienti';
        }

        let prezzo = document.getElementById("prezzo").value;
        let prezzoFinale = document.getElementById("prezzoFinale");
        if (prezzo !== '') {
            prezzoFinale.innerText = `${prezzo} €`;
        } else {
            prezzoFinale.innerText = 'Prezzo';
        }
    })
}


let update = document.getElementById("update");

if (update !== null) {
        let imgPath = document.getElementById("imgPath").value;
        let imgPreview = document.getElementById("imgPreview");
        imgPreview.src = imgPath;
        console.log(imgPath);

        let nome = document.getElementById("nome").value;
        let nomePizza = document.getElementById("nomePizza");

        nomePizza.innerText = nome;

        let ingredienti = document.getElementById("ingredienti").value;
        let listaIngredienti = document.getElementById("listaIngredienti");
        listaIngredienti.innerText = ingredienti;


        let prezzo = document.getElementById("prezzo").value;
        let prezzoFinale = document.getElementById("prezzoFinale");
        prezzoFinale.innerText = `${prezzo} €`;
}

