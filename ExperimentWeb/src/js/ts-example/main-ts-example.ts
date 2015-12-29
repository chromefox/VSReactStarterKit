/// <reference path="ts-example-1.ts" />
import Tsexample1 = require("./ts-example-1");

class TypeScriptExampleMain
{
    constructor() {
        // init all the different modules
        new Tsexample1.Example1();
    }
}

new TypeScriptExampleMain();