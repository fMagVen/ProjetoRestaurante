

function ContruirStatusPedidos(status, modo) {
    let htmlTituloStatus = '';
    if (modo == 1) htmlTituloStatus = '<h3>';
    else htmlTituloStatus = '<p>';
    switch (status) {
        case 1:
            htmlTituloStatus += 'Em preparo'
            break;
        case 2:
            htmlTituloStatus += 'Pronto'
            break;
        case 3:
            htmlTituloStatus += 'Entregue'
            break;
    }
    if (modo == 1) htmlTituloStatus += '</h3>';
    else htmlTituloStatus += '</p>';
    return htmlTituloStatus;
}

function PreencherPedidos(colaborador, pedidos) {
    nome = localStorage.getItem('nome');
    if (colaborador) {
        setor = localStorage.getItem('setor');
        idSetor = localStorage.getItem('idSetor');
        htmlPedidos = `
            <h3>Bem vindo, ${nome}!</h3>
            <h4>Você está no setor: ${setor}</h3>
            <h5>Confira abaixo os pedidos</h5>
            `
    }
    else
        htmlPedidos = `
        <h3>Bem vindo, ${nome}!</h3>
        <h4>Esperamos que aproveite sua estadia</h3>
        <h5>Confira abaixo os status de seus pedidos</h5>
        `
    var tituloStatus = 0;
    pedidos.forEach(pedido => {
        if (pedido.status > tituloStatus) {
            htmlPedidos += ContruirStatusPedidos(pedido.status, 1);
            tituloStatus++;
        }
        htmlPedidos +=
            `
            <div class="card card-producao">
                <p class="id-item" style="display: none;">${pedido.idItem}</p>
                <p class="id-pedido" style="display: none;">${pedido.idPedido}</p>
                <div class="card-corpo">
                    <h3 class="titulo-card">${pedido.nomeItem}</h3>
                `
        htmlPedidos += ContruirStatusPedidos(pedido.status, 2);
        htmlPedidos +=
            `
                </div>
                <div class="card-corpo">
                    <p class="descricao-card">Quantidade: ${pedido.quantidade}</p>
                `
        if (colaborador) {
            if (idSetor == 2 || idSetor == 3) {
                htmlPedidos +=
                    `
                    <div class="botoes-status">
                        <button class="selecao-status em-preparo" style="color: yellow; background-color: orange;">Em preparo</button>
                        <button class="selecao-status pronto" style="color: lightskyblue; background-color: blue;">Pronto</button>
                    </div>
                    `
            } else {
                htmlPedidos +=
                    `
                <div class="botoes-status">
                    <button class="selecao-status pronto" style="color: lightskyblue; background-color: blue;">Pronto</button>
                    <button class="selecao-status entregue" style="color: green; background-color: greenyellow;">Entregue</button>
                </div>
                `
            }
        }
        htmlPedidos +=
            `   
                </div>
            </div>
            `
    });

    $('#corpo-pedidos').html(htmlPedidos);
}

function ListarPedidos(colaborador) {

    if (colaborador) {
        var id = localStorage.getItem('idSetor');
        var rotaApi = `pedido/${id}`;
    } else {
        var id = localStorage.getItem('idCliente');
        var rotaApi = `pedido/cliente/${id}`;
    }
    var verboApi = 'GET';
    ComunicarAPI(rotaApi, verboApi, 1).then(function (resultado) {
        console.log(resultado)
        PreencherPedidos(colaborador, resultado);
    }).catch(erro => console.log("Erro: ", erro));

}
