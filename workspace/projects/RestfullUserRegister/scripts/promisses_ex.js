let isMeHappy = true;


/*A Promise*/
let willMeGetAPhone = new Promise((resolve, reject) =>
{
    if(isMeHappy)
    {
       let phone = { brand: 'Apple Iphone', color: 'Golden' }
       resolve(phone);
    }
    else
    {
       let reason = new Error('Im not happy :-(');
       reject(reason);//Optional
    }
});

/*Another promise*/
const showOff = brandPhone =>
{
    let message = `Hey buddy, i have a new ${brandPhone.color} ${brandPhone.brand}`;
    return Promise.resolve(message);
};



/*Consuming promises*/

let askMe = () =>
{
     willMeGetAPhone.then(showOff)
                    .then( fulfilled => {  console.log( fulfilled ); } )
                    .catch( error => { console.log(error.message); } );
};

askMe();
