/*Declarations*/
var express = require('express');
var body_parser = require('body-parser');
var mongoose = require('mongoose');
var app = express();

/*Setups*/
app.listen(5000, () => {
    console.log('Server is ON :)');
});

mongoose.connect('mongodb://localhost/personal');
mongoose.Promise = global.Promise;

app.use(body_parser.json());

app.use(body_parser.urlencoded({
    extended: true
}));

app.use('/api', require('./routes/api'));
