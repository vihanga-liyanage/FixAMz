$(".expand-item-title").click(function () {

    $header = $(this);
    //getting the next element
    $content = $header.next();
    //open up the content needed - toggle the slide- if visible, slide up, if not slidedown.
    $content.slideToggle(800, function () {});

});

function addNewClearAll() {
    document.forms[0]["AddNewFirstNameTextBox"].value = "";
    document.forms[0]["AddNewLastNameTextBox"].value = "";
    document.forms[0]["AddNewEmailTextBox"].value = "";
    document.forms[0]["AddNewContactTextBox"].value = "";
    document.forms[0]["AddNewUsernameTextBox"].value = "";
    document.forms[0]["AddNewPasswordTextBox"].value = "";
    document.forms[0]["AddNewConfirmPasswordTextBox"].value = "";
    return false;
}

function updateClearAll() {
    document.forms[0]["UpdateFirstNameTextBox"].value = "";
    document.forms[0]["UpdateLastNameTextBox"].value = "";
    document.forms[0]["UpdateEmailTextBox"].value = "";
    document.forms[0]["UpdateContactTextBox"].value = "";
    document.forms[0]["updateUserInitState"].style.display = block;
    return false;
}

function requiredFieldValidator(controller, msg) {
    var content = document.forms[0][controller + "TextBox"].value;
    if (content == "") {
        document.getElementById(controller + "Validator").innerHTML = msg;
        return false;
    } else {
        document.getElementById(controller + "Validator").innerHTML = "";
        return true;
    }
}

function isValidAddNew() { //try to write functions for overlaps
    var isValid = [true, true, true, true, true, true, true];
    var email = document.forms[0]["AddNewEmailTextBox"].value;
    var contact = document.forms[0]["AddNewContactTextBox"].value;
    var confirmPassword = document.forms[0]["AddNewConfirmPasswordTextBox"].value;

    isValid[0] = requiredFieldValidator("AddNewFirstName", "First name cannot be empty.");
    isValid[1] = requiredFieldValidator("AddNewLasttName", "Last name cannot be empty.");

    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if (email == "") {
        document.getElementById("AddNewEmailValidator").innerHTML = "Email cannot be empty.";
        isValid[2] = false;
    } else if (!re.test(email)) {
        document.getElementById("AddNewEmailValidator").innerHTML = "Enter a valid email address.";
        isValid[2] = false;
    } else {
        document.getElementById("AddNewEmailValidator").innerHTML = "";
        isValid[2] = true;
    }

    var prefix = contact.substring(0, 3);
    if (contact == "") {
        document.getElementById("AddNewContactValidator").innerHTML = "Contact cannot be empty.";
        isValid[3] = false;
    } else if (contact.length != 10) {
        document.getElementById("AddNewContactValidator").innerHTML = "Please enter a valid contact.";
        isValid[3] = false;
    } else if (!contact.match(/^\d{10}$/)) {
        document.getElementById("AddNewContactValidator").innerHTML = "Contact cannot have non-digits.";
        isValid[3] = false;
    } else if (!(prefix == "077" || prefix == "071" || prefix == "072" || prefix == "075" || prefix == "076")) {
        document.getElementById("AddNewContactValidator").innerHTML = "Please enter a valid contact.";
        isValid[3] = false;
    } else {
        document.getElementById("AddNewContactValidator").innerHTML = "";
        isValid[3] = true;
    }

    isValid[4] = requiredFieldValidator("AddNewUserName", "User name cannot be empty.");
    isValid[5] = requiredFieldValidator("AddNewPassword", "Password cannot be empty.");

    if (confirmPassword == "") {
        document.getElementById("AddNewConfirmPasswordValidator").innerHTML = "Confirm password cannot be empty.";
        isValid[6] = false;
    } else if (confirmPassword != password) {
        document.getElementById("AddNewConfirmPasswordValidator").innerHTML = "Confirm password does not match with password.";
        isValid[6] = false;
    } else {
        document.getElementById("AddNewConfirmPasswordValidator").innerHTML = "";
        isValid[6] = true;
    }

    for (var i = 0; i < isValid.length; i++) {
        if (!isValid[i]) { return false; }
    }
    return true;
}

function isValidUpdateEmpID() {
    return requiredFieldValidator("UpdateEmpID", "Employee ID cannot be empty.");
}

function isValiUpdate() {
    var isValid = [true, true, true, true];
    var firstname = document.forms[0]["UpdateFirstNameTextBox"].value;
    var lastname = document.forms[0]["UpdateLastNameTextBox"].value;
    var email = document.forms[0]["UpdateEmailTextBox"].value;
    var contact = document.forms[0]["UpdateContactTextBox"].value;

    isValid[0] = requiredFieldValidator("UpdateFirstName", "First name cannot be empty.");
    isValid[1] = requiredFieldValidator("UpdateLastName", "Last name cannot be empty.");

    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if (email == "") {
        document.getElementById("UpdateEmailValidator").innerHTML = "Email cannot be empty.";
        isValid[2] = false;
    } else if (!re.test(email)) {
        document.getElementById("UpdateEmailValidator").innerHTML = "Enter a valid email address.";
        isValid[2] = false;
    } else {
        document.getElementById("UpdateEmailValidator").innerHTML = "";
        isValid[2] = true;
    }

    var prefix = contact.substring(0, 3);
    if (contact == "") {
        document.getElementById("UpdateContactValidator").innerHTML = "Contact cannot be empty.";
        isValid[3] = false;
    } else if (contact.length != 10) {
        document.getElementById("UpdateContactValidator").innerHTML = "Please enter a valid contact.";
        isValid[3] = false;
    } else if (!contact.match(/^\d{10}$/)) {
        document.getElementById("UpdateContactValidator").innerHTML = "Contact cannot have non-digits.";
        isValid[3] = false;
    } else if (!(prefix == "077" || prefix == "071" || prefix == "072" || prefix == "075" || prefix == "076")) {
        document.getElementById("UpdateContactValidator").innerHTML = "Please enter a valid contact.";
        isValid[3] = false;
    } else {
        document.getElementById("UpdateContactValidator").innerHTML = "";
        isValid[3] = true;
    }

    for (var i = 0; i < isValid.length; i++) {
        if (!isValid[i]) { return false; }
    }
    return true;
}

function isValidAddCat() {
    //var isValid = true ;
    var catname = document.forms[0]["AddCategoryNameTextBox"].value;
    
    if (catname == "") {
        document.getElementById("AddCategoryValidator").innerHTML = "Enter Category Name.";
        //isValid[0] = false;
        return false
    } else {
        document.getElementById("AddCategoryValidator").innerHTML = "";
        //isValid[0] = true;
        return true;
    }
    
}

function isValidAddLoc() {
    //var isValid = true ;
    var catname = document.forms[0]["AddLocationNameTextBox"].value;

    if (catname == "") {
        document.getElementById("AddLocationValidator").innerHTML = "Enter Location Name";
        //isValid[0] = false;
        return false
    } else {
        document.getElementById("AddLocationValidator").innerHTML = "";
        //isValid[0] = true;
        return true;
    }

}


function addCategoryClearAll() {
    document.forms[0]["AddCategoryNameTextBox"].value = "";
    return false;
}

function addLocationClearAll() {
    document.forms[0]["AddLocationNameTextBox"].value = "";
    document.forms[0]["AddLocationAddressTextBox"].value = "";
    document.forms[0]["AddLocationContactTextBox"].value = "";
    document.forms[0]["AddLocationTypeTextBox"].value = "";
    document.forms[0]["AddLocationManagerOfficeTextBox"].value = "";
    document.forms[0]["AddLocationDepartmentTextBox"].value = "";
    document.forms[0]["AddLocationBranchTextBox"].value = "";
    document.forms[0]["AddLocationZonalOfficeTextBox"].value = "";
    return false;
}


function searchClearAll() {
    document.forms[0]["SearchEmployeeIDTextBox"].value = "";
    document.forms[0]["SearchFirstNameTextBox"].value = "";
    document.forms[0]["SearchLastNameTextBox"].value = "";
    document.forms[0]["SearchEmailTextBox"].value = "";
    document.forms[0]["SearchContactTextBox"].value = "";
    document.forms[0]["SearchUsernameTextBox"].value = "";
    return false;
}



function isValidUpdateCat() {
    //var isValid = true ;
    var catname = document.forms[0]["UpdateCatNameTextBox"].value;

    if (catname == "") {
        document.getElementById("UpdateCategoryNameValidator").innerHTML = "Enter Category Name.";
        //isValid[0] = false;
        return false
    } else {
        document.getElementById("UpdateCategoryNameValidator").innerHTML = "";
        //isValid[0] = true;
        return true;
    }

}

function isValidUpdateLoc() {
    //var isValid = true ;
    var locname = document.forms[0]["UpdateLocNameTextBox"].value;

    if (locname == "") {
        document.getElementById("UpdateLocationNameValidator").innerHTML = "Enter Location Name.";
        //isValid[0] = false;
        return false
    } else {
        document.getElementById("UpdateLocationNameValidator").innerHTML = "";
        //isValid[0] = true;
        return true;
    }

}

function updateCategoryClearAll() {
    document.forms[0]["UpdateCatNameTextBox"].value = "";
    document.forms[0]["updatecategoryrInitState"].style.display = block;
    return false;
}

function updateLocationClearAll() {
    document.forms[0]["UpdateLocNameTextBox"].value = "";
    document.forms[0]["updatelocationInitState"].style.display = block;
    return false;
}

function isValidDeleteEmpID() {
    return requiredFieldValidator("DeleteEmpID", "Employee ID cannot be empty.");
}

function isValidUpdateCat() {
    //var isValid = true ;
    var catname = document.forms[0]["UpdateCatNameTextBox"].value;

    if (catname == "") {
        document.getElementById("UpdateCategoryNameValidator").innerHTML = "Enter Category Name.";
        //isValid[0] = false;
        return false
    } else {
        document.getElementById("UpdateCategoryNameValidator").innerHTML = "";
        //isValid[0] = true;
        return true;
    }

}