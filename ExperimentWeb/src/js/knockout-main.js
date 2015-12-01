/// <reference path="../../typings/tsd.d.ts" />
var ViewModel = (function () {
    function ViewModel(firstName, lastName) {
        this.firstName = ko.observable(firstName);
        this.lastName = ko.observable(lastName);
        this.fullName = ko.computed(this.createFullname, this);
    }
    ViewModel.prototype.createFullname = function () {
        return this.firstName() + " " + this.lastName();
    };
    ViewModel.prototype.capitalizeLastName = function () {
        var currentVal = this.lastName();
        this.lastName(currentVal.toUpperCase());
    };
    return ViewModel;
})();
$(document).ready(function () {
    var model = new ViewModel("Ronny", "Muliawan");
    ko.applyBindings(model);
});
