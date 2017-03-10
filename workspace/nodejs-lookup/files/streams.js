var http = require('http');
var fs = require('fs');

var server = http.createServer(function(req, res) {

   console.log(`Request made: ${req.url}`);

   res.writeHead(200, {'Content-Type': 'text/plain'});

   var myStream = fs.createReadStream('any.txt', 'utf8');
   myStream.pipe(res);
});

server.listen(8888, '127.0.0.1');

console.log('Server is on...');
