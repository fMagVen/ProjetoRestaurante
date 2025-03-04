$(document).ready(function () {

    $("#form-login").submit(function (event) {

        event.preventDefault();

        var loginValido = $('#login').parsley().validate();
        var senhaValida = $('#senha').parsley().validate();

        if (!Array.isArray(loginValido) && !Array.isArray(senhaValida)) {

            let login = {};
            login.idColaborador = parseInt($("#login").val());
            login.senha = $("#senha").val();
            login.Descriptografado = true;
            let rotaApi = 'colaborador/login';
            let verboApi = 'POST';
            ComunicarAPI(rotaApi, verboApi, 2, login).then(function (resultado) {
                console.log(resultado);
                localStorage.setItem('bearerToken', resultado.stringToken);
                localStorage.setItem('nome', resultado.nome);
                localStorage.setItem('setor', resultado.nomeSetor);
                localStorage.setItem('idSetor', resultado.idSetor);
                if (resultado.idSetor == 1) window.location.href = "./visao-admin/visao-admin.html"
                else window.location.href = "./pedidos-colaborador/visao-pedidos-colaborador.html"
            }).catch(erro => console.log("Erro: ", erro));
        }

    });

});