// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var Id;
var data; 
var SpouseId; 
var tags = [];

$('select').on('change', function () {
    if ($(this).val() === "Married") {
        $('#myModal').modal('show');
        Id = $(this).attr("data-id");
        console.log(Id); 
    }
});



$('#myModal').on('shown.bs.modal', function (e) {
    $.ajax({
        url: "http://localhost:52009/Home/GetPersonNames",
        type: "POST",
        data: { Id: Id },
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



