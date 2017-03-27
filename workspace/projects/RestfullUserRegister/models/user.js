var mongoose = require('mongoose');
var Schema = mongoose.Schema;


var UserSchema = new Schema({

    first_name: {
       type: String,
       required: [true, 'First name is required']
    },

    last_name: {
       type: String,
       required: [true, 'Last name is required']
    },

    password: {
       type: String
    },

    created_at: {
        type: Date
    }
});

var User = mongoose.model('user', UserSchema);

module.exports = User;
