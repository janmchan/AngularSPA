var mockPersonValue = "ABC";
var mockAddPersonValue = "DEF";

    function MockPersonRepository() {
        this.find = function(callback) {
            return callback(mockPersonValue);
        }
        this.add = function (data, callback) {
            return callback(mockAddPersonValue);
        }
    }
    //Create Mock of Person Repository
    angular.module('AgeRanger.Services').factory('PersonRepository', function () {
        return new MockPersonRepository;
    });
    $injector = angular.injector(['ng', 'AgeRanger.Services']);

    var personService;
    QUnit.module('Service Tests', {
        beforeEach: function () {
           personService = $injector.get('PersonService');
        }
    });

    test('Person Data Properly Returned by Service',
        function () {
            expect(1);
            stop();
            personService.GetPersons(function(data) {
                ok(angular.equals(data, mockPersonValue), "Person Data from Service is expected");
                start();
            });

        }
    );
    test('Person Data Added by Service',
           function () {
               stop();
               var input = {};
               personService.AddPerson(input, function (data) {
                   ok(angular.equals(data, mockAddPersonValue), "Person Data from Service is expected");
                   start();
               });

           }
       );

