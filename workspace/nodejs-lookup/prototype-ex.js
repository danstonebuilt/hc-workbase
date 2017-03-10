Employee = function(name, ocupation, nationality) {
    
    this._name = name;
    this._ocupation = ocupation;
    this._nationality = nationality;
};


emp1 = new Employee("Daniel Stonebuilt", "Analyst", "Brazilian");
emp1.__proto__.age = 32;


console.log(emp1);

emp2 = new Employee("Reginaldo", "Programmer", "Brazilian");
console.log(emp2)
