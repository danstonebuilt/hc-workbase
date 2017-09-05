var http = require('http');
var fs = require('fs');
var dirpath = __dirname;

var server = http.createServer(function(request, response) {
   console.log(`Request made: ${request.url}`);

    if(request.url === '/home' || request.url === '/')
    {
        response.writeHead(200, {'Content-Type': 'text/html'});
        //response.end(__dirname + '/files/index.html');
        fs.createReadStream(__dirname + '/files/index.html').pipe(response);
    }
   //response.writeHead(200, {'Content-Type': 'text/plain'});
   //response.end('Hello Rockers!');
});

server.listen(8888, '127.0.0.1');

console.log('Server is on...');
