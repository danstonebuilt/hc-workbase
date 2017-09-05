$(function() {
    $('form').on('submit', function(evt) {
        console.log($(this).serialize());
        console.log($(this).find('input').val());
        console.log($(this).find('textarea').val());
        evt.preventDefault();
    });
});