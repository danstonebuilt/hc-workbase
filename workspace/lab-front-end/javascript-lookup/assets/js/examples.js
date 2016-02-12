$(document).ready(function(){
	/*
	$('H1')
	.css("color", "#947CBB")
	.hide()
	.delay('2000')
	.fadeIn("slow")
	.text('News Comeby')
	.click(function(){
		$('body').css("background", "#F5C4C7");
	})
*/
/*
$('a')
	 .css({ color: 'red', display: 'block' })
	 .click(function(){
	 	$('a')
			 .css({ color: '#F9AA33', display: 'block' })
	  });


*/

$('a')
	 .css({ color: '#F9AA33', display: 'block' })

$('.link2').css("color", "green");

$('#link3').css("color", "#000");

$('*').css({margin: '0', padding: '0'});

/*
$('img').error(function() {
	//$('img').attr("src", "assets/img/semImagem.jpg")
	var img_path = $('img').attr("src");
	var image = $('img');
	alert('Imagem '+ img_path + ' Indisponivel.');
	
	image.attr("src", "assets/img/semImagem.jpg");
})
*/



$(window).resize(function() {

	$('img')
			.width($(window).width())
			.height($(window).height());
});


});