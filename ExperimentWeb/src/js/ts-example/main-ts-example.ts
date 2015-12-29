/// <reference path="../../../typings/tsd.d.ts" />
// satisfy the TypeScript compilation
declare var global: Global;

'use strict';
import Tsexample1 = require("./ts-example-1");
import Tsexample2 = require("./ts-example-2");

global.$ = require('jquery');

class TypeScriptExampleMain
{
    constructor() {
        // init all the different modules
        new Tsexample1.Example1();
        new Tsexample2.Example2();
    }
}

new TypeScriptExampleMain();