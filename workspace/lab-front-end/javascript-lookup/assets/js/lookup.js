$(function() {
  
    $("button").on("click", function() {
        $(this).text('changed');
        console.log(this);       
    });
});

  /*
    var inptCodBar = $("input[type='text'][name='codbar']");
    var inptContent = $("input[type='text'][name='content']");
    */

/*
function exibeMsg(msg)
{
    
    $('input'
}
*/

/*
function padLeft(seq)
{
    var size = 8 - seq.length;
    var str = "";
    
    for(var i=0; i<size; i++)
    {
        str += '0';
    }
     
    return 'RE'+str+seq;
}
*/