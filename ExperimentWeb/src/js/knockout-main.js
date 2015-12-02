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
var SeatReservation = (function () {
    function SeatReservation(inputName, initialMeal) {
        this.name = inputName;
        this.meal = ko.observable(initialMeal);
        this.formattedPrice = ko.computed(this.computePrice, this);
    }
    SeatReservation.prototype.computePrice = function () {
        var price = this.meal().price;
        return price ? "$" + price.toFixed(2) : "None";
    };
    return SeatReservation;
})();
var ReservationsViewModel = (function () {
    function ReservationsViewModel() {
        var _this = this;
        this.removeSeat = function (seat) {
            _this.seats.remove(seat);
        };
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
    ReservationsViewModel.prototype.computeTotal = function () {
        var total = 0;
        for (var i = 0; i < this.seats().length; i++)
            total += this.seats()[i].meal().price;
        return total;
    };
    ReservationsViewModel.prototype.addSeat = function () {
        this.seats.push(new SeatReservation("", this.availableMeals[0]));
    };
    return ReservationsViewModel;
})();
var WebmailViewModel = (function () {
    function WebmailViewModel() {
        var _this = this;
        this.goToFolder = function (folder) {
            _this.chosenFolderId(folder);
            _this.chosenMailData(null); // Stop showing a folder
            $.get('/Knockout/GetMails', { folder: folder }, _this.chosenFolderData);
        };
        this.goToMail = function (mail) {
            _this.chosenFolderId(mail.folder);
            _this.chosenFolderData(null); // Stop showing a folder
            $.get("/Knockout/GetMail", { mailId: mail.id }, _this.chosenMailData);
        };
        this.folders = ['Inbox', 'Archive', 'Sent', 'Spam'];
        this.chosenFolderId = ko.observable();
        this.chosenFolderData = ko.observable();
        this.chosenMailData = ko.observable();
        this.goToFolder("Inbox");
    }
    return WebmailViewModel;
})();
;
var Answer = (function () {
    function Answer(text) {
        this.answerText = text;
        this.points = ko.observable(1);
    }
    return Answer;
})();
var SurveyViewModel = (function () {
    function SurveyViewModel(question, pointsBudget, answers) {
        var _this = this;
        this.pointsUsedFn = function () {
            var total = 0;
            for (var i = 0; i < _this.answers.length; i++)
                total += _this.answers[i].points();
            return total;
        };
        this.question = question;
        this.pointsBudget = pointsBudget;
        this.answers = $.map(answers, function (text) { return new Answer(text); });
        this.pointsUsed = ko.computed(this.pointsUsedFn, this);
    }
    SurveyViewModel.prototype.save = function () {
        alert("To do");
    };
    return SurveyViewModel;
})();
;
$(document).ready(function () {
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
    ko.bindingHandlers.jqButton = {
        init: function (element) {
            $(element).button();
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
