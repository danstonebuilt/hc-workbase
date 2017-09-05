var fs = require('fs');
var filename = __dirname+'/'+'any.txt';
var filename2 = __dirname+'/'+'Write2.txt';

/*This is a blocking code, hence it release the flow until it finish the execution*/
var read = fs.readFileSync(filename, 'utf8');

// Write Synchronous
fs.writeFileSync('WriteMe.txt', read);

/*Read Asynchronous*/
fs.readFile(filename, 'utf8', function(err, data) {

  fs.writeFile('Write2.txt', data);
  //console.log(data);
});


/*Delete a file*/
fs.unlink('Write2.txt');
/*Ex of use*/
fs.unlink('./stuff/Write2.txt', function() {
   fs.mkdir('stuff');
});


/*Create a Directory*/
fs.mkdirSync('stuff');

/*Remove a directory*/
fs.rmdirSync('stuff');

/*Create a Directory Asynchronous*/
fs.mkdir('stuff');

/*Sample of use*/
fs.mkdir('stuff', function() {
   fs.readFile('readMe2.txt', 'utf8', function(err, data) {
      fs.writeFile('./stuff/write3.txt', data);
   });
});

/*Remove a directory Asynchronous*/
fs.rmdir('stuff');
