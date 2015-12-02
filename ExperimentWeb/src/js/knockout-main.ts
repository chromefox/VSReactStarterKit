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
            { mealName: "Ultimate (whole zebra)", price: 390 }
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

    removeSeat = (seat: any) => {
        this.seats.remove(seat);
    }
}

class WebmailViewModel {
    // Data
    folders: any;
    chosenFolderId: any;
    chosenFolderData: any;
    chosenMailData : any;

    constructor() {
        this.folders = ['Inbox', 'Archive', 'Sent', 'Spam'];
        this.chosenFolderId = ko.observable();
        this.chosenFolderData = ko.observable();
        this.chosenMailData = ko.observable();
        this.goToFolder("Inbox");
    }

    goToFolder = (folder) => {
        this.chosenFolderId(folder);
        this.chosenMailData(null); // Stop showing a folder
        $.get('/Knockout/GetMails', { folder: folder }, this.chosenFolderData);
    }

    goToMail = (mail) => {
        this.chosenFolderId(mail.folder);
        this.chosenFolderData(null); // Stop showing a folder
        $.get("/Knockout/GetMail", { mailId: mail.id }, this.chosenMailData);
    }
};

class Answer {
    answerText: string;
    points: any;

    constructor(text: string) {
        this.answerText = text;
        this.points = ko.observable(1);
    }
}

class SurveyViewModel {
    question: any;
    pointsBudget: any;
    answers: any;
    pointsUsed: any;

    save() {
        alert("To do");
    }

    constructor(question: string, pointsBudget: any, answers: any) {
        this.question = question;
        this.pointsBudget = pointsBudget;
        this.answers = $.map(answers, text => new Answer(text));
        this.pointsUsed = ko.computed(this.pointsUsedFn, this);
    }

    pointsUsedFn = () => {
        var total = 0;
        for (var i = 0; i < this.answers.length; i++)
            total += this.answers[i].points();
        return total;
    }
};

$(document).ready(() => {    
    
    ko.bindingHandlers.fadeVisible = {
        init: function (element, valueAccessor) {
            // Start visible/invisible according to initial value
            var shouldDisplay = valueAccessor();
            $(element).toggle(shouldDisplay);
        },
        update: function (element, valueAccessor) {
            // On update, fade in/out
            var shouldDisplay = valueAccessor();
            shouldDisplay ? $(element).fadeIn() : $(element).fadeOut();
        }
    };


    var model = new ViewModel("Ronny", "Muliawan");
    ko.applyBindings(model, document.getElementById("example1"));
    ko.applyBindings(new ReservationsViewModel(), document.getElementById("example2"));
    ko.applyBindings(new WebmailViewModel(), document.getElementById("example3"));
    ko.applyBindings(new SurveyViewModel("Which factors affect your technology choices?", 10, [
        "Functionality, compatibility, pricing - all that boring stuff",
        "How often it is mentioned on Hacker News",
        "Number of gradients/dropshadows on project homepage",
        "Totally believable testimonials on project homepage"
    ]), document.getElementById("example4"));
});