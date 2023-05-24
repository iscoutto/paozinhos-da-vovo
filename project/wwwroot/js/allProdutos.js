function aplicaFiltroConsultaAvancada() {
    var vNome = $("#nome").val();
    var vCategoria = document.getElementById('categoria').value;
    var vPreco = $('#preco').val();
    var vUnidade = $("#unidade").val();
    var vAlergia = $("#alergia").val();

    $.ajax({
        url: "/Produto/ObtemDadosConsultaAvancada",
        data: {
            nome: vNome,
            categoria: vCategoria,
            preco: vPreco,
            unidade: vUnidade,
            alergia: vAlergia
        },
        success: function (dados) {
            if (dados.erro != undefined) {
                alert(dados.msg);
            }
            else {
                document.getElementById('resultadoConsulta').innerHTML = dados;
            }
        },
    });

}
