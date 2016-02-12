$(function() {

	var div_action = $('#div_action');

/*Evento change*/
	$('input').change(function() {
		div_action.text ($(this).val());

	});
});