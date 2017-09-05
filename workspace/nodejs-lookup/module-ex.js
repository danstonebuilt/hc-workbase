
var doSomething = function( func ) {

    if(typeof func === 'function') {

        func();
    }
}


module.exports = {
   'doSomething': doSomething
}
