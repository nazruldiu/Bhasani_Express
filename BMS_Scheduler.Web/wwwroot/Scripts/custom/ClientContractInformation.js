var grid;
var id;

var columns = [
    { id: "Id", name: "Id", field: "Id", width: 0, minWidth: 0, maxWidth: 0, cssClass: "reallyHidden", headerCssClass: "reallyHidden" },
    { id: "edit", name: "Edit", field: "edit", minWidth: 50, width: 50, maxWidth: 70, formatter: editButtonFormatter, headerCssClass: 'edit' },
    { id: "clientId", name: "Client", field: "clientId", minWidth: 150, width: 200, maxWidth: 300 },
    { id: "contractDate", name: "Contract Date", field: "contractDate", minWidth: 150, width: 200, maxWidth: 300 },
    { id: "effectiveDate", name: "Effective Date", field: "effectiveDate", minWidth: 150, width: 200, maxWidth: 300 },
];

var options = {
    enableCellNavigation: true,
    enableColumnReorder: false,
    forceFitColumns: true
};

$(function () {
    var data = [];

    $.getJSON("/Task/GetSlickGridData", function (items) {
        console.log(items);
        for (var i = 0; i < items.length; i++) {
            data[i] = {
                Id: items[i].Id,
                clientId: items[i].ClientId,
                contractDate: items[i].ContractDate,
                effectiveDate: items[i].EffectiveFrom
            };
        }
        grid = new Slick.Grid("#myGrid", data, columns, options);
    });
})

function editButtonFormatter(row, cell, value, columnDef, dataContext) {
    return '<a class="inline-actions view-details edtBtn" onclick="return gotoEditPage(' + dataContext.Id + ')" title="Edit" style="font-size: 1.2em;color: blue;"><i class="view-details fa fa-pencil-square-o"></i></a>'
}

function gotoEditPage(id) {
    window.location.href = '/Task/EditClientContractInformation/' + id;
}

function gotoCreatePage() {
    window.location.href = '/Task/CreateClientContractInformation';
}
