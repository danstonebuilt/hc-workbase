// Limpar campos
function LimparCampos(arrayObjetos) {
    if (arrayObjetos != null) {
        $.each(arrayObjetos, function (index) {
            if ($(arrayObjetos[index]).is('input:text')) {
                $(arrayObjetos[index]).val(''); // input text
            }
            else if ($(arrayObjetos[index]).is("select")) {
                $(arrayObjetos[index]).val(0);
            }
        });
    }
}

$(document).ready(function () {
    var originalXhr = $.ajaxSettings.xhr;
    $.ajaxSetup({

        async: true,
        cache: false,
        error: function (xhr, errorType, exception) {
            if (xhr.status == 550)
                alert("550 Error Message");
            else if (xhr.status == "403")
                alert("403. Not Authorized");
            else if (xhr.status == "500") {
                var errorMessage;

                try {
                    //Error handling for POST calls
                    errorMessage = JSON.parse(xhr.responseText);
                    //errorMessage = err.Message;
                }
                catch (ex) {
                    //Error handling for GET calls
                    //$('#divMaster').appsend('<div id="divErrorResponse">' + XHR.responseText + '</div>');
                    //errorMessage = 'Page load error: ';
                    //errorMessage = $(xhr.responseText).find('h2 i').html();
                    errorMessage = $(xhr.responseText).html();
                }

                toastr.options = { "timeOut": 0, "positionClass": "toast-top-full-width", "fadeIn": 200, "fadeOut": 600 };
                toastr.error("Erro : " + errorMessage.Message);

            } else {
                toastr.options = { "timeOut": 0, "positionClass": "toast-top-full-width", "fadeIn": 200, "fadeOut": 600 };
                toastr.error("Erro : " + xhr.responseText);
            }
        },
        success: function (xhr) {
            //do something global on success... 
        }
    });

    $(document).ajaxStart(function (xhr) {
        if ($("#progress").length === 0) {
            $("body").append($("<div><dt/><dd/></div>").attr("id", "progress"));
            $("#progress").width((50 + Math.random() * 10) + "%");
        }
    });

    $(document).ajaxStop(function () {
        $("#progress").width("101%").delay(200).fadeOut(600, function () {
            $(this).remove();
        });
    });


    $.LimparCamposModal = function (area) {
        // Limpar textbox
        $(area).find('input[type="text"],input[type="email"],input[type="hidden"],textarea').val('');

        // Limpa options da dropdown
        $(area).find('select option').remove();

        $(area).find('table  > tbody').empty();
    };
});

function ExibirMensagemSucesso(mensagem) {
    
    toastr.options = { 'timeOut': 6000, 'positionClass': 'toast-top-full-width', 'fadeIn': 200, 'fadeOut': 600 };
    toastr.success(mensagem);

}

function ExibirMensagemAlerta(mensagem) {

    toastr.options = { 'timeOut': 10000, 'positionClass': 'toast-top-full-width', 'fadeIn': 200, 'fadeOut': 600 };
    toastr.warning(mensagem);

}

function ExibirMensagemInfo(mensagem) {

    toastr.options = { 'timeOut': 11000, 'positionClass': 'toast-top-full-width', 'fadeIn': 200, 'fadeOut': 600 };
    toastr.info(mensagem);

}

function ExibirMensagemErro(mensagem) {

    toastr.options = { 'timeOut': 0, 'positionClass': 'toast-top-full-width', 'fadeIn': 200, 'fadeOut': 600 };
    toastr.error(mensagem);

}

function formataData(data) {

    var myDate = new Date(data);

    var mes = (myDate.getMonth() + 1);
    
    if (mes < 10) {
        mes = "0" + mes;
    }

    return myDate.getDate() + "/" + mes + "/" + myDate.getFullYear();
}