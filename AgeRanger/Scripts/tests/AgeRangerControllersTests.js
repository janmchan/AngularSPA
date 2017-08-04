angular.module('AgeRangerApp').provider({
    $rootElement: function () {
        this.$get = function () {
            return angular.element('<div ng-app></div>');
        };
    }
});


var controllerInjector = angular.injector(['ng', 'AgeRangerApp']);

var testData = "Service Data", sandbox, personService, stubLocation, controller;

QUnit.module('Controller Tests', {
    beforeEach: function () {

        sandbox = sinon.sandbox.create();


        this.$scope = controllerInjector.get('$rootScope').$new();
        var $controller = controllerInjector.get('$controller');
        personService = controllerInjector.get('PersonService');
        var location = controllerInjector.get('$location');
        sandbox.stub(location, "path");
        sandbox.stub(personService, 'GetPersons', callback =>  callback(testData));
        controller = $controller('PeopleController', {
            $scope: this.$scope,
            $location: location,
            PersonService: personService
        });
        
    },
    afterEach: function () {
        sandbox.restore();
    }
});

test('AddPerson test', function () {
    

    ok(angular.equals(this.$scope.Persons, testData), "PersonService properly populated");

    stop();
    var currentScope = this.$scope;
    sandbox.stub(personService, 'AddPerson', function (formData, callback) {
        callback({ Success: true, Message: '' });
    });

    this.$scope.addPerson(function () {
        ok(angular.equals(currentScope.addPersonForm.addPersonFailure, false), "Person sucessfully added");
        start();
    });
});

test('AddPerson test failed', function () {
   var currentScope = this.$scope;
    sandbox.stub(personService, 'AddPerson', function (formData, callback) {
        callback({ Success: false, Message: '' });
    });

    stop();
    this.$scope.addPerson(function () {
        ok(angular.equals(currentScope.addPersonForm.addPersonFailure, true), "Person not successfully added");
        start();
    });
});

