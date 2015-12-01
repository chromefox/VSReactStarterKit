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

class SeatReservation {
    name: any;
    meal: any;
    formattedPrice: any;

    constructor(inputName: string, initialMeal: any) {
        this.name = inputName;
        this.meal = ko.observable(initialMeal);
        this.formattedPrice = ko.computed(this.computePrice, this);
    }

    computePrice() {
        var price = this.meal().price;
        return price ? "$" + price.toFixed(2) : "None";
    }
}

class ReservationsViewModel {
    availableMeals: any;
    seats: any;
    totalSurcharge: any;
    self: ReservationsViewModel;

    constructor() {
        this.availableMeals = [
            { mealName: "Standard (sandwich)", price: 0 },
            { mealName: "Premium (lobster)", price: 34.95 },
            { mealName: "Ultimate (whole zebra)", price: 290 }
        ];

        this.seats = ko.observableArray([
            new SeatReservation("Steve", this.availableMeals[0]),
            new SeatReservation("Bert", this.availableMeals[0])
        ]);

        this.totalSurcharge = ko.computed(this.computeTotal, this);
        this.self = this;
    }

    computeTotal() {
        var total = 0;
        for (var i = 0; i < this.seats().length; i++)
            total += this.seats()[i].meal().price;
        return total;
    }

    addSeat() {
        this.seats.push(new SeatReservation("", this.availableMeals[0]));
    }

    public removeSeat = (seat: string) => {
        this.seats.remove(seat);
    }   
}

$(document).ready(() => {
    var model = new ViewModel("Ronny", "Muliawan");
    ko.applyBindings(model, document.getElementById("example1"));
    ko.applyBindings(new ReservationsViewModel(), document.getElementById("example2"));
});