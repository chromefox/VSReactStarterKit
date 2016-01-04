
/// <reference path="knockout/knockout.d.ts" />
/// <reference path="jquery/jquery.d.ts" />
/// <reference path="jqueryui/jqueryui.d.ts" />
/// <reference path="jsdom/jsdom.d.ts" />
/// <reference path="node/node.d.ts" />
/// <reference path="jquery.dataTables/jquery.dataTables.d.ts" />

interface KnockoutBindingHandlers {
    fadeVisible: KnockoutBindingHandler;
    jqButton: KnockoutBindingHandler;
}

interface Global {
    $: any;
    L: any;
    map: any;
    mapClass : any;
    leftClass : any;
}