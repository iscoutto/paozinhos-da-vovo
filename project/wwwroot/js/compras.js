// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// seleciona o elemento do textarea
const textarea = document.querySelector('#nota');

// define o atributo readonly
textarea.setAttribute('readonly', true);

function buscaProduto() {

    var produto = $("#produto").val();

    var buscaAPI = '/compras/BuscaProdutoAjax?id=' + produto;
    $.ajax({
        url: buscaAPI,

        success: function (dados) {
            document.getElementById("nome_produto").value = dados.nome;
            document.getElementById("ingrediente_produto").value = dados.info_nutricional;
            document.getElementById("alergia_produto").value = dados.alergia;
            document.getElementById("preco_produto").value = "R$ " + dados.preco;
            document.getElementById("unidade_produto").value = dados.unidade;
            document.getElementById("categoria_produto").value = dados.categoria;
        }

    })
}

function adicionarCarrinho() {

    var idProduto = document.getElementById("produto");
    var nota = document.getElementById("nota");

    var produto = document.querySelector('input[name="nome_produto"]').value;
    var preco = document.querySelector('input[name="preco_produto"]').value;
    var unidade = document.querySelector('input[name="unidade_produto"]').value;   

    idProduto.value = "";
    nota.value += produto + '\n' + preco + ' ' + unidade + '\n\n';
}

//Hélix

function apagaqrcode() {
    //console.log("Entrei na função apagarqrcode")
    var api = "http://172.20.10.10/v2/entities/urn:ngsi-ld:Espcam:002/attrs";
    var data = "{\"qrcode\": {\"type\": \"text\",\"value\": \"\"}}";
    $.ajax({
        type: 'POST',
        url: api,
        dataType: 'json',
        data: data,
        headers:
        {
            "Content-Type": "application/json",
            "fiware-service": "helixiot",
            "fiware-servicepath": "/",
        },
        success: function (dados) {
            //console.log("QR Code apagado");
        }
    })
}

function enviaqrcode() {
    if (executando) {
        return;
    }
    try {
        executando = true;
        var qrcode = "";
        //console.log(opera);
        function loop() {
            console.log("Funcionou");
            qrcode = pegaqrcode();
            if (qrcode === "")
                console.log("vazio");
            else
                console.log(qrcode);
            //qrcode = result[0].qrcode.value;
            if (qrcode === "") {
                setTimeout(loop, 2000); // Espera 2 segundos antes de chamar o loop novamente
            } else {
                apagaqrcode();
                document.getElementById("produto").value = qrcode;
                executando = false;
                enviaqrcode();
            }
        }
 // Chama o loop pela primeira vez
    } catch (error) {
        executando = false;
        console.error(error);
    }
}

var executando = false;

function makeSyncGetRequest(url, headers) {
    var xhr = new XMLHttpRequest();
    xhr.open("GET", url, false); // Configurando a solicitação para ser síncrona
    //xhr.setRequestHeader("Content-Type", "application/json");

    // Definindo os cabeçalhos personalizados
    if (headers) {
        for (var header in headers) {
            xhr.setRequestHeader(header, headers[header]);
        }
    }

    xhr.send();
    if (xhr.status === 200) {
        //console.log("Resposta:", xhr.responseText);
        return JSON.parse(xhr.responseText); // Convertendo a resposta em JSON
    } else {
        throw new Error("Erro na solicitação: " + xhr.status);
    }
}

// Uso do método makeSyncGetRequest
var helix = "http://172.20.10.10";
var link = helix + ":1026/v2/entities/";

var headershelix = {
    Accept: "application/json",
    "fiware-service": "helixiot",
    "fiware-servicepath": "/"
};


function pegaqrcode() {
    try {
        var response = makeSyncGetRequest(link, headershelix)[2].qrcode.value;
        return response;
        //console.log(response);
    } catch (error) {
        console.error(error);
    }
}
function loop() {
    qrcode = pegaqrcode();
    if (qrcode === "")
        console.log("vazio");
    else
        console.log(qrcode);
    //qrcode = result[0].qrcode.value;
    if (qrcode === "") {
        setTimeout(loop, 2000); // Espera 2 segundos antes de chamar o loop novamente
    } else {
        apagaqrcode();
        document.getElementById("produto").value = qrcode;
        executando = false;
        enviaqrcode();
    }
   
}