var ID;
var parkingID;
var slotID;
var userID;
function onChange(arg) {
    var grid = $("#allUsersGrid").data("kendoGrid");
    var selectedItem = grid.dataItem(grid.select());
    ID = selectedItem.UserID
}

function onChangeparkingGrid(arg) {
    var grid = $("#allParkingsGrid").data("kendoGrid");
    var selectedItem = grid.dataItem(grid.select());
    parkingID = selectedItem.ParkingID
}

function onChangeslotGrid(arg) {
    var grid = $("#allSlotsGrid").data("kendoGrid");
    var selectedItem = grid.dataItem(grid.select());
    slotID = selectedItem.SlotID
}
function onChangesSlotTransactionGrid(arg) {
    var grid = $("#allSlotsTransactionGrid").data("kendoGrid");
    var selectedItem = grid.dataItem(grid.select());
    userID = selectedItem.UserID
}
function LoadAddNewUserForm() {
    $.ajax({
        url: "UserManagement/GetUserEntryForm",
        type: "GET",
        data: null,
        success: function (responseData) {
            $("#divUsersGrid").html(responseData);
            ID = null;
        },
        error: renderErrorMessage

    });
}
function AddUser() {
    var formData = new FormData($('#userForm')[0]);
    $.ajax({
        url: "UserManagement/AddUser",
        type: "POST",
        data: formData,
        success: function (responseData) {
            $("#divUsersGrid").html(responseData);
        },
        cache: false,
        contentType: false,
        processData: false,
        error: renderErrorMessage

    });
}

function UpdateUser() {
    var formData = new FormData($('#userForm')[0]);
    $.ajax({
        url: "UserManagement/UpdateUser",
        type: 'POST',
        data: formData,
        success: function (responseData) {
            if (responseData == true) {
                CallGetPartial();
                ID = null;
            }

        },
        //Options to tell jQuery not to process data or worry about content-type.
        cache: false,
        contentType: false,
        processData: false,
        error: renderErrorMessage
    });

}

function LoadEditUserForm() {
    if (ID == "" || ID == null || ID == undefined) {
        alert("Please select User to Edit !");
        return;
    }
    $.ajax({
        url: "UserManagement/GetUserEditForm?userID=" + ID,
        type: "GET",
        success: function (responseData) {
            $("#divUsersGrid").html(responseData);
            $.validator.unobtrusive.parse("#userForm");
            ID = null;
        },
        error: renderErrorMessage

    });
}

function CallGetPartial() {
    $.ajax({
        type: "GET",
        url: "UserManagement/LoadIndex",
        data: null,
        success: function (response) {
            $("#divUsersGrid").html(response);
            ID = null;
        },
        error: renderErrorMessage

    });
}

function renderErrorMessage() {
    hideLoader();
    var dv = $('#dvError');
    //var ErrortMessage;
    console.log("Error: in renderErrorMessage() ");
    dv.html(ErrorMessage);
}


function LoadAddSlotForm() {
    $.ajax({
        url: "ParkingSlot/GetSlotAddForm",
        type: 'GET',
        success: function (response) {
            $("#divSlotAdd").html(response);
        },
        //Options to tell jQuery not to process data or worry about content-type.
        cache: false,
        contentType: false,
        processData: false,
        error: renderErrorMessage
    });
}

function LoadEditSlot() {
    if (slotID == "" || slotID == null || slotID == undefined) {
        alert("Please select Slot No to Edit !");
        return;
    }
    $.ajax({
        url: "ParkingSlot/GetSlotEditForm?slotID=" + slotID,
        type: 'GET',
        success: function (response) {
            $("#divSlotAdd").html(response);
            slotID = null;
        },
        //Options to tell jQuery not to process data or worry about content-type.
        cache: false,
        contentType: false,
        processData: false,
        error: renderErrorMessage
    });
}
function AddSlot() {
    var formData = new FormData($('#slotForm')[0]);
    $.ajax({
        url: "ParkingSlot/AddSlot",
        type: 'POST',
        data: formData,
        success: function (response) {
            //$("#divUsersGrid").html(response);
            CallGetPartialForSlotManagement();

        },
        //Options to tell jQuery not to process data or worry about content-type.
        cache: false,
        contentType: false,
        processData: false,
        error: renderErrorMessage
    });
}
function UpdateSlot() {
    var formData = new FormData($('#slotForm')[0]);
    $.ajax({
        url: "ParkingSlot/UpdateSlot",
        type: 'POST',
        data: formData,
        success: function (response) {
            //$("#divUsersGrid").html(response);
            CallGetPartialForSlotManagement();

        },
        //Options to tell jQuery not to process data or worry about content-type.
        cache: false,
        contentType: false,
        processData: false,
        error: renderErrorMessage
    });
}
function SearchSlot() {
    $('#allSlotsGrid').data('kendoGrid').dataSource.read();
}



function CallGetPartialForSlotManagement() {
    $.ajax({
        type: "GET",
        url: "ParkingSlot/LoadIndex",
        data: null,
        success: function (response) {
            $("#divSlotAdd").html(response);
            ID = null;
        },
        error: renderErrorMessage

    });
}
function AddParking() {
    var formData = new FormData($('#parkingForm')[0]);
    $.ajax({
        url: "Parkings/AddParking",
        type: 'POST',
        data: formData,
        success: function (response) {
            //$("#divUsersGrid").html(response);
            CallGetPartialForParking();

        },
        //Options to tell jQuery not to process data or worry about content-type.
        cache: false,
        contentType: false,
        processData: false,
        error: renderErrorMessage
    });
}
function LoadParkingAdd() {
    $.ajax({
        type: "GET",
        url: "Parkings/GetParkingAddForm",
        data: null,
        success: function (response) {
            $("#divParkings").html(response);
            ID = null;
        },
        error: renderErrorMessage

    });
}
function LoadParkingEdit() {
    if (parkingID == "" || parkingID == null || parkingID == undefined) {
        alert("Please Select Parking to Edit !");
        return;
    }
    $.ajax({
        type: "GET",
        url: "Parkings/GetParkingEditForm?parkId=" + parkingID,
        data: null,
        success: function (response) {
            $("#divParkings").html(response);
            ID = null;
            parkingID = null;
        },
        error: renderErrorMessage

    });
}

function UpdateParking() {
    var formData = new FormData($('#parkingForm')[0]);
    $.ajax({
        url: "Parkings/UpdateParking",
        type: 'POST',
        data: formData,
        success: function (responseData) {
            if (responseData == true) {
                CallGetPartialForParking();
            }
            ID = null;
            parkingID = null;

        },
        //Options to tell jQuery not to process data or worry about content-type.
        cache: false,
        contentType: false,
        processData: false,
        error: renderErrorMessage
    });
}
function CallGetPartialForParking() {
    $.ajax({
        type: "GET",
        url: "Parkings/LoadIndex",
        data: null,
        success: function (response) {
            $("#divParkings").html(response);
            ID = null;
        },
        error: renderErrorMessage

    });
}


function SearchReadData() {
    var dropdownlist = $("#ParkingID").data("kendoDropDownList");
    return {
        parkingID: dropdownlist.value()
    };
}

function SearchTransaction() {
    $('#allSlotsTransactionGrid').data('kendoGrid').dataSource.read();
}
function SetLogMode() {

    if ($("#rdoLogBySlot_logBySlot").attr("checked")) {
        var dropdownlistUsr = $("#ParkingID").data("kendoDropDownList");
        dropdownlistUsr.enable(true);
        var dropdownlistParkID = $("#UserID").data("kendoDropDownList");
        dropdownlistParkID.enable(false);
        dropdownlistParkID.select(0);

    }
    if ($("#rdoLogByUser_logByUser").attr("checked")) {
        var dropdownlistUsr = $("#UserID").data("kendoDropDownList");
        if (dropdownlistUsr != undefined) {
            dropdownlistUsr.enable(true);

            var dropdownlistParkID = $("#ParkingID").data("kendoDropDownList");
            dropdownlistParkID.enable(false);
            dropdownlistParkID.select(0)

            var dropdownlistSlotID = $("#SlotID").data("kendoDropDownList");
            dropdownlistSlotID.enable(false);
            dropdownlistSlotID.select(0)
        }
    }

}
$(document).ready(function () {
    $("#rdoLogByUser_logByUser").change(function () {
        SetLogMode();
    });

    $("#rdoLogBySlot_logBySlot").change(function () {
        SetLogMode();
    });
});

