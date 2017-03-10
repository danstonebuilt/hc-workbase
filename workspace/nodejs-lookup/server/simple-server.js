var http = require('http');

var server = http.createServer(function(request, response) {
   console.log(`Request made: ${request.url}`);

   response.writeHead(200, {'Content-Type': 'text/plain'});
   response.end('Hello Rockers!');
});

server.listen(8888, '127.0.0.1');

console.log('Server is runing...');
