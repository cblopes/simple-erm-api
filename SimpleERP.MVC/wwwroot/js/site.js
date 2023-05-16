// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var priceField = document.getElementById("Price");

document.getElementById("createProduct").addEventListener("submit", function () {
    var priceValue = priceField.value;
    
    var formattedPriceValue = priceValue.replace(',', '.');
    
    priceField.value = formattedPriceValue;
});

(() => {
    'use strict'

    const forms = document.querySelectorAll('.needs-validation')

    Array.from(forms).forEach(form => {
        form.addEventListener('submit', event => {
            if (!form.checkValidity()) {
                event.preventDefault()
                event.stopPropagation()
            }

            form.classList.add('was-validated')
        }, false)
    })
})()