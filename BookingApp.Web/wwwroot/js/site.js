
let createAjax = document.querySelector('#createajax');

function removeForm() {
  //  $('#createajax').html = "";
    createAjax.innerHTML = "";
}

function failCreate(response) {
    console.log(response, 'failed to create');
    createAjax.innerHTML = response.responseText;
}

function fixValidation() {
    const form = createAjax.querySelector('form');
    $.validator.unobtrusive.parse(form);
}
