var evt = require('events');

var eventEmitter = new evt.EventEmitter();


eventEmitter.on('doorOpen', function(){
    
    console.log('Ring ring ring...');
});

eventEmitter.emit('doorOpen');