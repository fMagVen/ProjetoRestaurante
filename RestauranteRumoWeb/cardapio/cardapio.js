$(document).ready(function () {

    var htmlCards = '';

    function LimparCorpoCardapio() {
        var componente = $('#corpoCardapio');
        componente.html('');
        $(".off-canvas").css("opacity", "0");
        ListarCardapio();
    }

    function ListarCardapio() {
        var rotaApi = 'cardapio';
        var verboApi = 'GET';
        ComunicarAPI(rotaApi, verboApi, 1).then(function (resultado) {
            ContruirCards(resultado);
        }).catch(erro => console.log("Erro: ", erro));
    }

    function ContruirCards(Cardapio) {
        var tituloCategoria = 0;
        $(Cardapio).each(function (index, item) {
            if (item.categoria > tituloCategoria) {
                htmlCards += ConstruirTituloCategoria(item.categoria);
                tituloCategoria++;
            }
            htmlCards = AdicionarHtml(htmlCards, item)
        });
        $('#corpoCardapio').html(htmlCards);
    }

    LimparCorpoCardapio();

    $(".container").on("click", ".grande-botao-adicao", function () {
        let card = $(this).closest(".card");
        let idItem = parseInt(card.find(".id-item").text());
        let nome = card.find(".titulo-card").text();
        let ingredientesDesc = card.find(".descricao-card").text();
        let quantidade = 1;
        let valor = parseFloat(card.find(".valor").text().replace(',', '.'));
        $(this).replaceWith(`<p class="adicionado">Adicionado!</p>`);
        let cardTransporte = { idItem, nome, ingredientesDesc, quantidade, valor };
        let cardPedido = '';
        cardPedido = AdicionarHtml(cardPedido, cardTransporte, 'pedido');
        $(".montagem-pedido").append(cardPedido);
        AtualizarTotal();
    });

    $(".montagem-pedido").on("click", ".adicao", function () {
        let card = $(this).closest(".card");
        let quantidade = card.find(".quantidade");
        let valor = parseInt(quantidade.text());
        valor++;
        quantidade.text(valor);
        AtualizarTotal();
    });

    $(".montagem-pedido").on("click", ".subtracao", function () {
        let card = $(this).closest(".card");
        let quantidade = card.find(".quantidade");
        let valor = parseInt(quantidade.text());
        valor--;
        if (valor == 0) {
            let id = card.find(".id-item").text();
            card.remove();
            $(".card-cardapio").each(function () {
                let idItem = $(this).find(".id-item").text();
                if (id == idItem) {
                    let adicionado = $(this).find(".adicionado");
                    adicionado.replaceWith('<button class="grande-botao-adicao adicao">+</button>');
                }
            })
        }
        else {
            quantidade.text(valor);
        }
        AtualizarTotal();
    });

    $(".off-canvas").on("click", "toggle-pedido", function () {
        let containerPedido = $(".montagem-pedido");
        let isHidden = containerPedido.css("transform") === "matrix(1, 0, 0, 1, 0, 0)";
        containerPedido.css("opacity", isHidden ? "0" : "1");
    });

    $(".off-canvas").on("click", ".fazer-pedido", function (event) {
        event.preventDefault();
        var nomeValido = $('#nome').parsley().validate();
        var mesaValida = $('#mesa').parsley().validate();

        if (!Array.isArray(nomeValido) && !Array.isArray(mesaValida)) {

            $('.off-canvas').css({ 'display': 'none' });
            $('#corpoCardapio').css({ 'display': 'flex', 'flex-direction': 'column', 'align-items': 'center', 'justify-content': 'center' });
            $('#corpoCardapio').html(`<img src="../comum/pacman.gif" style="width: 200px; height: 200px" />
                <p>Realizando pedido...</p>
                `);
            //montar cliente
            let nome = $("#nome").val().trim();
            let mesa = parseInt($("#mesa").val());
            let fechado = false;
            let data = new Date();
            let cliente = { nome, mesa, fechado, data };

            //cadastrar cliente
            rotaApi = 'cliente';
            verboApi = 'POST';
            ComunicarAPI(rotaApi, verboApi, 2, cliente).then(function (resultado) {
                localStorage.setItem('nome', nome);
                localStorage.setItem('idCliente', resultado);
                EnviarPedido(resultado);
            }).catch(function (erro) {
                $('.off-canvas').css({ 'display': 'block' });
                $('#corpoCardapio').css({ 'display': 'block' });
                $('#corpoCardapio').html(htmlCards);
                console.log(erro)
                Swal.fire({
                    icon: "error",
                    title: "Erro",
                    text: "Servidor indisponível no momento!"
                });
            });

            function EnviarPedido(idCliente) {

                let pedido = [];

                //montar pedido
                $(".card-pedido").each(function () {
                    let idItem = parseInt($(this).find(".id-item").text());
                    let quantidade = parseInt($(this).find(".quantidade").text());
                    let status = 1;
                    pedido.push({ idItem, idCliente, quantidade, status });
                })

                rotaApi = 'pedido';
                verboApi = 'POST';

                //enviar pedido
                ComunicarAPI(rotaApi, verboApi, 2, pedido).then(function (resultado) {
                    console.log(resultado);
                    window.location.href = "./pedidos-cliente/visao-pedidos-cliente.html"
                }).catch(function (erro) {
                    $('.off-canvas').css({ 'display': 'block' });
                    $('#corpoCardapio').css({ 'display': 'block' });
                    $('#corpoCardapio').html(htmlCards);
                    console.log(erro)
                    Swal.fire({
                        icon: "error",
                        title: "Erro",
                        text: "Servidor indisponível no momento!"
                    });
                });
            }
        }
    })
});