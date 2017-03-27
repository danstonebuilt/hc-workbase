var express = require('express');
var router = express.Router();
var User = require('../models/user');

router.get('/', function(request, response)
{
     response.send('Welcome to my site!');
});


router.get('/users', function(request, response) {
    /*
    userModel.find({}, function(err, user) {
      if(!err)
          response.json(user);
      else
         response.json({err: 'Não foi possivel retornar o usuário'});
    });
    */
});

router.get('/users/:id', function(request, response) {

    var id = request.params.id;
   /*
     userModel.findById(id, function(err, user) {
       if(!err)
           response.json(user);
       else
          response.json({err: 'Não foi possivel encontrar o usuário'});
     });
     */
});

router.post('/users', function(request, response) {
    /*
     User.create(request.body).then(function(err, user)
     {
           if(!err)
               response.json(user);
           else
              response.json({err: 'Não foi possivel salvar o usuário'});
      });

      */
      User.create(request.body).then(function(users) {
           response.send(users);
      });

});

router.put('/users/:name', function(request, response) {

     response.send(`Put requested, data updated: ${request.params.name}`);
});

router.delete('/users/:id', function(request, response)
{
    var id = request.params.id;
     /*
     userModel.findById(id, function(err, user)
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
     */
});


module.exports = router;
