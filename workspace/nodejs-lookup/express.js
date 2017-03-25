var express = require('express');

var app = express();

app.get('/', function(req, res) {

    /*For strings*/
    res.send('This is is the home page');
});

app.get('/home', function(req, res) {

    /*For files*/
    res.sendFile(__dirname+'/files/index.html');
});

app.get('/contact', function(req, res) {

    res.send('This is is the contact page');
});

app.get('/profile/:id', function(req, res) {

    res.send(`You requested this ${req.params.id}`);
});


app.listen(3050);

console.log('Server is on...');
