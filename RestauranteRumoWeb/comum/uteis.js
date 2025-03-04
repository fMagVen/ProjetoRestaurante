
function ComunicarAPI(rota, verbo, tipo, corpo = null, requerToken = false) {
    var urlBaseApi = "https://localhost:32768/"
    var requisicao = {
        url: urlBaseApi + rota,
        method: verbo,
    }
    if (tipo == 1) requisicao.dataType = "json";
    else requisicao.contentType = "application/json";
    if (corpo != null) requisicao.data = JSON.stringify(corpo);
    if (requerToken) requisicao.beforeSend = function (xhr) {
        xhr.setRequestHeader('Authorization', `Bearer ${localStorage.getItem('bearerToken')}`)
    }
    $.ajaxSetup({
        headers: { 'Authorization': `Bearer ${localStorage.getItem('bearerToken')}` },
        error: function (jqXHR, textStatus, errorThrown) {
            if (jqXHR.status != 200 || jqXHR.status != 201 || jqXHR.status != 202) console.log(jqXHR.responseText);
        }
    })
    return $.ajax(requisicao);
}

function AdicionarHtml(string, item, contexto) {
    if (contexto == 'pedido') string += '<div class="card card-pedido">'
    else string += '<div class="card card-cardapio">'
    string +=
        `
    <p class="id-item" style="display: none;">${item.idItem}</p>
    <div class="card-corpo">
    <h3 class="titulo-card">${item.nome}</h3>
    <p class="valor">${parseFloat(item.valor).toFixed(2).replace('.', ',')}</p>
    </div>
    <div class="card-corpo">
    <p class="descricao-card">${item.ingredientesDesc}</p>
                    <div class="selecao-item">
                    `
    if (contexto == 'pedido')
        string +=
            `
    <button class="botao-selecao subtracao">-</button>
    <p class="quantidade">${item.quantidade}</p>
    <button class="botao-selecao adicao">+</button>
    `
    else
        string +=
            `
    <button class="grande-botao-adicao adicao">+</button>
    `

    string +=
        `
    </div>
    </div>
            </div>
            `
    return string;
}

function AtualizarTotal() {
    let total = 0;
    $(".card-pedido").each(function () {
        let valor = parseFloat($(this).find(".valor").text().replace(',', '.'));
        let quantidade = parseInt($(this).find(".quantidade").text());
        total += valor * quantidade;
    });
    $(".total").text(`Total: $${total.toFixed(2)}`);
    if ($(".card-pedido").length > 0) {
        $(".off-canvas").css("opacity", "1");
    } else {
        $(".off-canvas").css("opacity", "0");
    }
}

function ConstruirTituloCategoria(categoria) {
    let htmlTituloCategoria = '<h1>';
    switch (categoria) {
        case 1:
            htmlTituloCategoria += 'Entradas'
            break;
        case 2:
            htmlTituloCategoria += 'Executivos'
            break;
        case 3:
            htmlTituloCategoria += 'Burgers'
            break;
        case 4:
            htmlTituloCategoria += 'Asiaticos'
            break;
        case 5:
            htmlTituloCategoria += 'Massas'
            break;
        case 6:
            htmlTituloCategoria += 'Sobremesas'
            break;
        case 7:
            htmlTituloCategoria += 'Bebidas, sem alcool'
            break;
        case 8:
            htmlTituloCategoria += 'Bebidas, com alcool'
            break;
    }
    htmlTituloCategoria += '</h1>'
    return htmlTituloCategoria;
}

