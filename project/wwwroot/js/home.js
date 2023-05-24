function expandirContato() {

    var contato = document.getElementById("contato");

    contato.classList.toggle("exibir");

    contato.scrollIntoView({ behavior: 'smooth' });


}

function expandirSobre() {
    var sobre = document.getElementById("sobre");

    sobre.classList.toggle("exibir");

    sobre.scrollIntoView({ behavior: 'smooth' });
}

