/*
 * Translated default messages for the jQuery validation plugin.
 * Locale: PT (Portuguese; português)
 * Region: BR (Brazil)
 */
(function ($) {
    $.extend($.validator.messages, {
        required: "Este campo é obrigatório.",
        remote: "Por favor, corrija este campo.",
        email: "Por favor, forneça um endereço eletrônico válido.",
        url: "Por favor, forneça uma URL válida.",
        date: "Por favor, forneça uma data válida.",
        dateISO: "Por favor, forneça uma data válida (ISO).",
        number: "Por favor, forneça um n&uacute;mero válido.",
        digits: "Por favor, forneça somente d&iacute;gitos.",
        creditcard: "Por favor, forneça um cart&atilde;o de cr&eacute;dito válido.",
        equalTo: "Por favor, forneça o mesmo valor novamente.",
        accept: "Por favor, forneça um valor com uma extens&atilde;o válida.",
        maxlength: $.validator.format("Por favor, forneça n&atilde;o mais que {0} caracteres."),
        minlength: $.validator.format("Por favor, forneça ao menos {0} caracteres."),
        rangelength: $.validator.format("Por favor, forneça um valor entre {0} e {1} caracteres de comprimento."),
        range: $.validator.format("Por favor, forneça um valor entre {0} e {1}."),
        max: $.validator.format("Por favor, forneça um valor menor ou igual a {0}."),
        min: $.validator.format("Por favor, forneça um valor maior ou igual a {0}.")
    });

    $.validator.setDefaults({
        highlight: function (element) {
            $(element).closest('.form-group').addClass('has-error');
        },
        unhighlight: function (element) {
            $(element).closest('.form-group').removeClass('has-error');
        },
        errorElement: 'span',
        errorClass: 'help-block',
        errorPlacement: function (error, element) {
            if (element.parent('.input-group').length) {
                error.insertAfter(element.parent());
            } else {
                error.insertAfter(element);
            }
        }
    });
    
} (jQuery));

