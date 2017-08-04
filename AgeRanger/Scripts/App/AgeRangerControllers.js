
var controllerModule = angular.module('AgeRanger.Controllers', []);

controllerModule.controller("UpdatePersonController",
    function ($scope, $location, $routeParams, PersonService) {
        var id = $routeParams.id;
        $scope.personForm = {
            Id: '',
            FirstName: '',
            LastName: '',
            Age: '',
            processPersonFailure: false
        };

        PersonService.FindPerson(id, function (data) {
            $scope.personForm = {
                Id: data.Id,
                FirstName: data.FirstName,
                LastName: data.LastName,
                Age: data.Age,
                processPersonFailure: false
            };
        });
        

        $scope.updatePerson = function (callback) {
            
            PersonService.UpdatePerson($scope.personForm,
                function(result) {
                    if (result.Success) {
                        $location.path('/');
                    } else {
                        
                        $scope.personForm.processPersonFailure = true;
                    }
                    if (typeof callback === "function") {
                        callback(result);
                    }

                });

        }
    });

controllerModule.controller("PeopleController", function ($scope, $location, PersonService) {
   PersonService.GetPersons(function (data) {
        $scope.Persons = data;
    });

    $scope.addPersonForm = {
        FirstName: '',
        LastName: '',
        Age: '',
        addPersonFailure: false
    };
    

    $scope.addPerson = function (callback) {
        PersonService.AddPerson($scope.addPersonForm, function (result) {
            
            if (result.Success) {
                $location.path('/');
            } else {
                $scope.addPersonForm.addPersonFailure = true;
            }
            if (typeof callback === "function") {
                callback(result);
            }
            
        });
        
        
    }
});
controllerModule.controller("menuController",
    function ($scope, $location) {

        $scope.getClass = function (path) {
            return ($location.path() === path) ? 'highlight' : '';
        }
    });