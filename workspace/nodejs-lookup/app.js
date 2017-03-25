//console.log(__filename);
//console.log(__dirname);
/*Setups*/
//--------------------------------------
var express = require('express');
var app = express();
var body_parser = require('body-parser');
var url_encoded_parser = body_parser.urlencoded({extended: false});

/*Set Views*/
app.set('view engine', 'ejs');
/*Set Static files*/
app.use('/assets', express.static('assets'));

//-----------------------------------------------------
/*Http Verbs*/
app.get('/home', function(req, res) {

    console.log(req.url);
    res.sendFile(__dirname+'/files/index.html');
});

app.get('/contact', function(req, res) {

    res.render('contact', {name: req.query});
});

app.get('/profile/:name', function(req, res) {

    res.render('profile', {name: req.params.name});
});

app.get('/form', function(req, res) {
    res.render('simple-form');
});

/*
app.post('/form', url_encoded_parser, function(req, res) {
    console.log(req.body);
    res.render('simple-form');
});
*/
app.post('/another', url_encoded_parser, function(req, res) {
    console.log(req.body);
});

app.listen(3050);
//---------------------------------------------------------------------
console.log('Server is on...');
