// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var priceField = document.getElementById("Price");

// adiciona um listener para a ação de envio do formulário
document.getElementById("createProduct").addEventListener("submit", function () {
    // pega o valor do campo de preço
    var priceValue = priceField.value;
    // substitui a vírgula pelo ponto
    var formattedPriceValue = priceValue.replace(',', '.');
    // atualiza o valor do campo de preço com o valor formatado
    priceField.value = formattedPriceValue;
});