$(document).ready(function () {
    var rotaApi = 'setores';
    var verboApi = 'GET';
    ComunicarAPI(rotaApi, verboApi, 1).then(function (resultado) {
        let dropdown = $('#setor-colaborador-cadastro');
        dropdown.empty();
        let htmlDropdown = ''
        $(resultado).each(function (index, item) {
            htmlDropdown += `<option value="${item.idSetor}" value="">${item.nome}</option>`;
        })
        dropdown.html(htmlDropdown);
    }).catch(function (erro) {
        console.log(erro)
        Swal.fire({
            icon: "error",
            title: "Erro",
            text: "Servidor indisponível no momento!"
        });
    });

    $(".container").on("click", "#form-cadastro-colaborador", function (event) {
        event.preventDefault();
        var nomeValido = $('#nome-colaborador-cadastro').parsley().validate();
        var senhaValida = $('#senha-colaborador-cadastro').parsley().validate();

        if (!Array.isArray(nomeValido) && !Array.isArray(senhaValida)) {
            var nome = $('#nome-colaborador-cadastro').val();
            var idSetor = $('#setor-colaborador-cadastro').val();
            var senha = $('#senha-colaborador-cadastro').val();
            var form = $("#form-cadastro-colaborador");
            var backup = form.html();
            form.html(
                `
            <img src="../../comum/spinner.gif" style="width: 50px; height: 50px" />
            <p>Cadastrando...</p>
            `
            );
            var dadosColaborador = { nome, senha, idSetor };
            var rotaApi = 'colaborador/cadastro';
            var verboApi = 'POST';
            ComunicarAPI(rotaApi, verboApi, 2, dadosColaborador).then(function (resultado) {
                console.log(resultado)
                Swal.fire({
                    icon: "success",
                    text: `Cadastro concluido com sucesso! ID do colaborador: ${resultado}`
                });
                form.html(backup);
            }).catch(function (erro) {
                card.html(backup);
                console.log(erro);
                Swal.fire({
                    icon: "error",
                    title: "Erro",
                    text: "Servidor indisponível no momento!"
                });
                form.html(backup);
            });

        }

    });
});