const express = require('express');
const router = express.Router();
const User = require('../models/user');

router.get('/', (request, response) => {
     response.send('Welcome to my site!');
});


router.get('/users', (request, response) => {

});

router.get('/users/:id', (request, response) => {

    let id = request.params.id;
});

router.post('/users', (request, response) => {

      User.create(request.body).then(users => {
           response.send(users);
      });

});

router.put('/users/:name', (request, response) => {

     response.send(`Put requested, data updated: ${request.params.name}`);
});

router.delete('/users/:id', (request, response) => {
    let id = request.params.id;
});


module.exports = router;
