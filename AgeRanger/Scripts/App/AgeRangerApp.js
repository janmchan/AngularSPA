var app = angular.module("AgeRangerApp",
[
    'AgeRanger.Controllers',
    'AgeRanger.Services',
    'AgeRanger.Repositories',
    'ngRoute'
]);

app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.
        when('/', {
            templateUrl: 'PersonMgmt/'
        }).
        when('/personMgmt', {
            templateUrl: 'PersonMgmt/'
        })
        .when('/addPerson', {
            templateUrl: 'PersonMgmt/Add',
            controller: 'PeopleController'
        }).when('/updatePerson/:id', {
            templateUrl: 'PersonMgmt/Update',
            controller: 'UpdatePersonController'
        });
}]);