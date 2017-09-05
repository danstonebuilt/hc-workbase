$.ajax({
	url : "/pagina-teste",
	type : "post",
	data : "parama=TESTE&paramb=TESTE2",
	success : function (data) {
		$("div#teste").html(data);
	},
	error : function (xhr) {
		$("div#error").html(xhr.responseText);
	}
})


//Sintaxe AJAX
/*
$(function() {
  $.ajax({
	url : "loremipsum.html",
	context : document.body,
	type : “GET”,
	success : function () {
		$(this).addClass("done");
	}
  });
});
*/

/*
url
Serve para passarmos o endereço da pagina requisitada.

data: o parâmetros serializados da requisição, no formato padrão 


context
Serve para indicarmos qual o objeto de contexto da requisição, podendo ser qualquer objeto do DOM.


type
Serve para passarmos o método de requisição podendo ser: “GET”, “POST:, “DELETE” e “PUT”.


success
Serve para passarmos a função de callback a ser executada após o sucesso da requisição.
*/