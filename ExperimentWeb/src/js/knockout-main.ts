/// <reference path="../../typings/tsd.d.ts" />

class ViewModel {
    firstName: any;
    lastName: any;
    fullName: any;

    constructor(firstName: string, lastName: string) {
        this.firstName = ko.observable(firstName);
        this.lastName = ko.observable(lastName);
        this.fullName = ko.computed(this.createFullname, this);
    }

    createFullname() {
        return this.firstName() + " " + this.lastName();
    }

    capitalizeLastName() {
        var currentVal = this.lastName();
        this.lastName(currentVal.toUpperCase());
    }
}

$(document).ready(() => {
    var model = new ViewModel("Ronny", "Muliawan");
    ko.applyBindings(model);
});