var Id;
var data; 
var SpouseId; 
var tags = [];


$('select').on('change', function () {
    if ($(this).val() === "Married") {
        $('#myModal').modal('show');
        Id = $(this).attr("data-id");
    }
});


$('#myModal').on('shown.bs.modal', function (e) {
    $.ajax({
        url: "http://localhost:52009/Home/GetPersonNames",
        type: "POST",
        data: { id: Id },
        async: false,
        success: function (response) {
            data = response;
          
            $.each(data, function (index, value) {
                tags.push(value.firstName);
            });
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(jqXHR);
            console.log(textStatus);
            console.log(errorThrown);
        }
    })
});

$('#myModal').on('hide.bs.modal', function () {
    $('select').each(function () {
        if ($(this).attr('data-id') === Id) {
            $(this).val("Single");
        }
    })
});


$(function () {
    $("#selectSpouse").autocomplete({
        source: tags
    });
});

$("#submit_button").click(function () {
    if (existsValidSpouse()) {
        SpouseId = existsValidSpouse().id;
        $.ajax({
            url: "http://localhost:52009/Home/MarryTwoPerson",
            type: "POST",
            data: {
                PersonId: Id,
                SpouseId: SpouseId
            },
            async: false,
            success: function (response, textStatus, jqXHR) {
                $('#myModal').modal('toggle');
                location.reload(true);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(jqXHR);
                console.log(textStatus);
                console.log(errorThrown);
            }
        });
    }
});

var existsValidSpouse = function () {
    const result = data.find(({ firstName, id }) => firstName === $("#selectSpouse").val());
    return result; 
}

$("#addRow").click(function () {
    var html = '';
    html += '<div id="inputFormRow" class = "mt-3 mx-3">';
    html += '<div class="row">';
    html += '<input type="text" name="phoneNumber" class="form-control col-md-10">';
    html += '<button id="removeRow" type="button" class="btn btn-primary offset-1 col-md-1">-</button>';
    html += '</div>';
    html += '</div>';

    $('#newRow').append(html);
});

$("#addAddress").click(function () {
    var html = '';
    html += '<div id="inputFormAddress">';
    html += '<div class="d-flex justify-content-between align-items-center">';
    html += '<textarea name="address" class="form-control col-md-7" rows="1"></textarea>';
    html += '<div class="radio"><label><input name = "primary" class="m-3 radio_button" type="radio"/>Primary</label></div>';
    html += '<button id="removeAddressArea" type="button" class="btn btn-primary h-50 offset-1 col-md-1">-</button>';
    html += '</div>';
    html += '</div>';

    $('#newAddressRow').append(html);
});

$('#newAddressRow').on('click', '#removeAddressArea', function () {
    $(this).closest('#inputFormAddress').remove();
});


$('#newRow').on('click', '#removeRow', function () {
    $(this).closest('#inputFormRow').remove();
});


$('#newAddressRow').on('click', '.radio_button', function () {
    $('.radio_button').not(this).prop('checked', false);
}); 


var depthOfPrimaryRadioButton = function (radioButtons) {
    var depth = 0; 
    radioButtons.each(function () {
        if ($(this).is(":checked")) {
            return false;
        }
        ++depth;
    }); 
    return depth; 
}

var containsPrimaryRadioButton = function (radioButtons) {
    var contains = false; 
    radioButtons.each(function () {
        if ($(this).is(":checked")) {
            contains = true; 
        }
    }); 
    return contains;
}


$(function () {

    $("form[name = 'registration']").on('submit', function () {

        var depth = depthOfPrimaryRadioButton($("input[type = 'radio']"));
        var textAreas = $('textarea');
     
        if ($("form[name = 'registration']").valid()) {
            textAreas[depth].name = "primaryAddress"; 
        }

    });
});


$(function () {
    $("form[name='registration']").validate({
        rules: {
            firstName: "required",
            lastName: "required",
            birthDate: "required",
            address: "required",
            primary: "required", 
            phoneNumber: "required"
        },

        messages: {
            firstname: "Please enter your firstname",
            lastname: "Please enter your lastname",
            birthDate: "Please enter your birthdate",
            primary: "Please select primary address", 
            phoneNumber: "Please enter your phone number",
           
        },

        errorPlacement: function (error, element) {
            element.parents(".form-group").append(error);
        },

 
        submitHandler: function (form) {
                form.submit();
        }
    });
});

