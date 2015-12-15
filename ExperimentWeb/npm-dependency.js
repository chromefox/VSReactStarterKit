// This file defines all your dependencies for browserify.
// gulp + browserify will take all the distributable files and concatenate them for browser consumption.
global.$ = require('jquery');
global.jQuery = global.$;
var jqueryUI = require('jquery-ui');
global.ko = require('knockout');

require()