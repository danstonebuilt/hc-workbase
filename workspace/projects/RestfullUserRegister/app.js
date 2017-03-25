/*Declarations*/
var express = require('express');
var body_parser = require('body-parser');
var mongoose = require('mongoose')
               .connect('mongodb://127.0.0.1/personal');




/*Setups*/
var app = express();

/*app.use(body_parser.json());*/

/*
app.use(body_parser.urlencoded({
    extended: true
}));
*/
var mongo_con = mongoose.connection;

mongo_con.on('error',
             console.error.bind(console,
                                'Erro ao conectar no banco'));
var User;

mongo_con.once('open', function() {

    var userSchema = mongoose.Schema({
        first_name: String,
        last_name: String,
        password: String,
        created_at: Date
    });

    User = mongoose.model('User', userSchema);
});

app.listen(5000);

console.log('Server is ON :)');

/*Routes*/
app.get('/', function(request, response) {

    new User({
        first_name: 'Daniel',
        last_name: 'Anselmo',
        email: 'daanselmo@hcrp.usp.br',
        password: 'hcrp#$13',
        created_at: new Date()
    }).save(function(err, user) {
        if(!err)
            response.json(user);
        else
           response.json({err: 'Não foi possivel salvar o usuário'});
    });

});

app.get('/teste', function(request, response) {

     response.send('Teste acessado');
});

app.get('/users', function(request, response) {

    User.find({}, function(err, user) {
      if(!err)
          response.json(user);
      else
         response.json({err: 'Não foi possivel retornar o usuário'});
    });
});

app.get('/users/:id', function(request, response) {

    var id = request.params.id;

     User.findById(id, function(err, user) {
       if(!err)
           response.json(user);
       else
          response.json({err: 'Não foi possivel encontrar o usuário'});
     });
});

app.post('/users', function(request, response) {

  response.json(request.body);
/*
  new User({
      first_name: 'Daniel',
      last_name: 'Anselmo',
      email: 'daanselmo@hcrp.usp.br',
      password: 'hcrp#$13',
      created_at: new Date()
  }).save(function(err, user) {
      if(!err)
          response.json(user);
      else
         response.json({err: 'Não foi possivel salvar o usuário'});
  });
  */
});

app.put('/users/:name', function(request, response) {
     response.send(`Put requested, data updated: ${request.params.name}`);
});

app.delete('/users/:id', function(request, response)
{
    var id = request.params.id;

     User.findById(id, function(err, user)
     {
         if(!err)
         {
             user.remove(function(err)
             {
                  if(!err)
                      response.json({res: 'Usuário excluido com sucesso!'});
             });
         }
     });
});
