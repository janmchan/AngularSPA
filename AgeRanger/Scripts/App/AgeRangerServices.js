var servicesModule = angular.module('AgeRanger.Services', []);

servicesModule.service('PersonService', function (PersonRepository) {
    this.GetPersons = function (callback) {
        return PersonRepository.find(function (response) {
            callback(response);
        }, function (err) {
            callback(err);
        });
    };
    this.FindPerson = function (id, callback) {
        return PersonRepository.findById(id, function (response) {
            callback(response);
        }, function (err) {
            callback(err);
        });
    };
    this.AddPerson = function (formData, callback) {
        return PersonRepository.add(formData, function (response) {
            callback(response);
        }, function (err) {
            callback(err.Message);
        });
    };
    this.UpdatePerson = function (formData, callback) {
        return PersonRepository.put(formData, function (response) {
            callback(response);
        }, function (err) {
            callback(err.Message);
        });
    };
});