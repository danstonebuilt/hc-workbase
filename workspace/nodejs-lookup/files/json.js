var http = require('http');
var fs = require('fs');

var server = http.createServer(function(req, res) {

   console.log(`Request made: ${req.url}`);

   res.writeHead(200, {'Content-Type': 'application/json'});
   /*
   Obs: res parameter is a writeble stream and accepts a buffer or string,
   as we will work on json files it must be converted in string format
   */

    var json = {
      name: 'Daniel Stonebuilt',
      age: 32,
      job: 'Software Developer'
    }

   res.end(JSON.stringify(json));
});

server.listen(8888, '127.0.0.1');

console.log('Server is on...');
