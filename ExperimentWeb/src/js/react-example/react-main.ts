// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
declare var global: Global;
global.$ = require('jquery');
// jQuery plugins
require('datatables.net-bs')();
require('jquery-validation');
require('jquery-validation-unobtrusive');
import reactEx1 = require("./react-example");

class ReactExampleMain {
    instruction: string = "Hello from React! The Datatable initialization and components are all created via React and JS." +
    "For the purposes of demo, the onclick events are all attached from the React component itself (Please click the table's name column)." +
    "For jQuery plugins integration, I think it is much safer to integrate the onclick via the plugin API.";

    data: Array<Object> = [
        {
            name: "Alpha",
            email: "Alpha@email.com",
            year: "1989"
        },
        {
            name: "Beta",
            email: "Beta@email.com",
            year: "2039"
        },
        {
            name: "Charlie",
            email: "Charlie@email.com",
            year: "2736"
        }
    ];

    inputAttributes: Object = {};

    constructor() {
        // init all the different modules
        var infobox = new reactEx1.LabsPage();

        // setup input attributes for testing
        this.inputAttributes["data-val"] = true;
        this.inputAttributes["data-val-required"] = "Validation error msg";
        this.inputAttributes["name"] = "Test";

        infobox.domRender("content", this.instruction, this.data, this.inputAttributes);
        this.initLibs();
    }

    initLibs() {
        $("table").DataTable();
    }
}

new ReactExampleMain();