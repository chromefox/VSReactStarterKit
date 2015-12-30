// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX

import reactEx1 = require("./react-example");

class ReactExampleMain {
    constructor() {
        // init all the different modules
        var infobox = new reactEx1.InfoBox();
        infobox.domRender("content", "Hello from React!.");
    }
}

new ReactExampleMain();