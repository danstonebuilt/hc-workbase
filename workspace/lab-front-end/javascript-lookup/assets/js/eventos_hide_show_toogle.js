$(function() {

	var evt_main = $('#div_main');	
	

	$('.evt-hide').click(function() {
		evt_main.hide('slow');
	});

	$('.evt-show').click(function() {
		evt_main.show('slow');
	});

	$('.evt-toggle').click(function() {
		evt_main.toggle('slow');
	});

});