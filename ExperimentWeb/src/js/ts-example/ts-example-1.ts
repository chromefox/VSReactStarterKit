import $ = require('jQuery');

export class Example1 {
    constructor() {
        // initialize code on documentReady jQuery style
        $(document).ready(this.init);
    }

    init() {
        var idSelector = "#ts-example-1";
        var idContent = "Hello Example 1";
        $(idSelector).text(idContent);
        $(idSelector).css("background", "red");
    }
}