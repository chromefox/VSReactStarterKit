'use strict';
import Tsexample1 = require("./ts-example-1");
import Tsexample2 = require("./ts-example-2");

var $ = require('jquery');
var dt = require('datatables.net')();

class TypeScriptExampleMain
{
    constructor() {
        // init all the different modules
        new Tsexample1.Example1();
        new Tsexample2.Example2();
    }
}

new TypeScriptExampleMain();