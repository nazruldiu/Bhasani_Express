function usingAgGrid() {
    if (window['agGrid']) {
        return;
    } else {
        loadCss("~/Scripts/agGrid/ag-grid.css", "agGridCss");
        loadCss("~/Scripts/agGrid/ag-theme-balham.css", "agGridThemeCss");

        loadScript(Q.resolveUrl("~/Scripts/agGrid/ag-grid-community.noStyle.js"));
        loadScript(Q.resolveUrl("~/Scripts/agGrid/ag-grid-vue.umd.js"));
    }
}


function usingTabulator() {
    if (window['Tabulator']) {
        return;
    } else {
        loadScript(Q.resolveUrl("~/Scripts/tabulator/Tabulator.js"));
        //loadCss("~/Scripts/tabulator/tabulator4.9.3.css", "tabulatorCss");
        //loadCss("~/Scripts/tabulator/tabulator_493_Simple.min.css", "tabulatorCss");
        loadCss("~/Scripts/tabulator/tabulator.css", "tabulatorCss");
        //loadCss("~/Scripts/agGrid/ag-theme-balham.css", "agGridThemeCss");
        //loadScript(Q.resolveUrl("~/Scripts/tabulator/options.js"));
        //loadScript(Q.resolveUrl("~/Scripts/tabulator/ColumnManager.js"));
        //loadScript(Q.resolveUrl("~/Scripts/tabulator/RowManager.js"));
        //loadScript(Q.resolveUrl("~/Scripts/tabulator/FooterManager.js"));
        
        //loadScript(Q.resolveUrl("~/Scripts/agGrid/ag-grid-vue.umd.js"));
        
        //loadScript(Q.resolveUrl("~/Scripts/tabulator/tabulator4.9.3.js"));
        //loadScript(Q.resolveUrl("~/Scripts/vue-select.min.js"));
        //loadScript(Q.resolveUrl("~/Scripts/tabulator/tabulator47.js"));
        //loadScript(Q.resolveUrl("~/Scripts/tabulator/jquery_wrapper.js"));
        loadScript(Q.resolveUrl("~/Scripts/xlsx.full.min.js"));
    }
}

function usingSlickCellMenu() {
    if (window['Slick'] && window['Slick']['Plugins'] && window['Slick']['Plugins']['CellMenu']) {
        return;
    } else {
        $("<link/>")
            .attr("type", "text/css")
            .attr("id", "CustomSlickGridPlugin")
            .attr("rel", "stylesheet")
            .attr("href", Q.resolveUrl("~/Modules/_Ext/CustomSlickGridPlugin/slick.cellmenu.css"))
            .appendTo(document.head);

        loadScript(Q.resolveUrl("~/Modules/_Ext/CustomSlickGridPlugin/slick.cellmenu.js"))
    }
}


function usingSplitJs() {
    if (window['Split']) {
        return;
    } else {
        loadScript(Q.resolveUrl("~/Scripts/splitJs/split.js"));
    }
}

function usingResizable() {
    if (window['resizable']) {
        return;
    } else {
        loadScript(Q.resolveUrl("~/Scripts/splitJs/Resizable.js"));
    }
}