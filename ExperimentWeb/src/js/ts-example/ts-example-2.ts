﻿/// <reference path="../../../typings/tsd.d.ts" />
require('datatables.net')();

export class Example2 {
    constructor() {
        $(document).ready(this.init);
    }

    init() {
        // enhances all tables
        $('table').DataTable();
    }
}