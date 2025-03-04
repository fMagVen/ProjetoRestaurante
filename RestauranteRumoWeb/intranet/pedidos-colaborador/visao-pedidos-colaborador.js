$(document).ready(function () {
    ListarPedidos(true);

    $(".container").on("click", ".em-preparo", function () {
        let card = $(this).closest(".card");
        let idItem = parseInt(card.find(".id-item").text());
        let idPedido = parseInt(card.find(".id-pedido").text());
        let dadosPedido = { idItem, idPedido, status: 1 };
        var rotaApi = 'pedido/status';
        var verboApi = 'POST';
        ComunicarAPI(rotaApi, verboApi, 2, dadosPedido).then(function (resultado) {
            ListarPedidos(true);
        }).catch(function (erro) {
            console.log(erro)
            Swal.fire({
                icon: "error",
                title: "Erro",
                text: "Servidor indisponível no momento!"
            });
        });
    });

    $(".container").on("click", ".pronto", function () {
        let card = $(this).closest(".card");
        let backup = card.html();
        let idItem = parseInt(card.find(".id-item").text());
        let idPedido = parseInt(card.find(".id-pedido").text());
        card.html(
            `
            <img src="../comum/spinner.gif" style="width: 20px; height: 20px" />
            <p>Alterando status...</p>
            `
        );
        let dadosPedido = { idItem, idPedido, status: 2 };
        var rotaApi = 'pedido/status';
        var verboApi = 'POST';
        ComunicarAPI(rotaApi, verboApi, 2, dadosPedido).then(function (resultado) {
            ListarPedidos(true);
        }).catch(function (erro) {
            card.html(backup);
            console.log(erro);
            Swal.fire({
                icon: "error",
                title: "Erro",
                text: "Servidor indisponível no momento!"
            });
        });
    });

    $(".container").on("click", ".entregue", function () {
        let card = $(this).closest(".card");
        let idItem = parseInt(card.find(".id-item").text());
        let idPedido = parseInt(card.find(".id-pedido").text());
        let dadosPedido = { idItem, idPedido, status: 3 };
        var rotaApi = 'pedido/status';
        var verboApi = 'POST';
        ComunicarAPI(rotaApi, verboApi, 2, dadosPedido).then(function (resultado) {
            ListarPedidos(true);
        }).catch(function (erro) {
            console.log(erro)
            Swal.fire({
                icon: "error",
                title: "Erro",
                text: "Servidor indisponível no momento!"
            });
        });
    });

});