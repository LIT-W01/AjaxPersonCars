$(function() {
    $("#addPerson").on('click', function() {
        $("#addPersonModal").modal();
    });

    $(".submitPerson").on('click', function() {
        var first = $("#first").val();
        var last = $("#last").val();

        $.post("/home/addperson", { firstName: first, lastName: last }, function(person) {
            //var row = $("<tr><td>" + person.FirstName + "</td><td>" + person.LastName + "</td><td><button class='btn btn-primary'>Something-" + person.Id + "</button></td></tr>");
            //$("#peopleTable").append(row);
            addPersonToTable(person);
            clearModals();
        });
    });

    $.get("/home/getpeople", function(people) {
        people.forEach(addPersonToTable);
        //people.forEach(function(person) {
        //    addPersonToTable(person);
        //});
    });

    $("#peopleTable").on('click', '.viewCars', function() {
        var personId = $(this).data('personid');
        $.post("/home/getcarsforperson", { personId: personId }, function (cars) {
            $("#carsTable tr:gt(0)").remove();
            cars.forEach(addCarToTable);
            $("#viewCarsModal").modal();
        });
    });

    $("#peopleTable").on('click', '.addCar', function() {
        var personId = $(this).data('personid');
        $("#addCarModal").data('personid', personId);
        $("#addCarModal").modal();
    });

    $(".submitCar").on('click', function() {
        var personId = $("#addCarModal").data('personid');
        var make = $("#make").val();
        var model = $("#model").val();
        var year = $("#year").val();
        $.post("/home/addcar", { personId: personId, make: make, model: model, year: year }, function() {
            clearModals();
        });
    });

    $("#peopleTable").on('click', '.deleteCar', function() {
        var personId = $(this).data('personid');
        var row = $(this).parent().parent();
        $.post('/home/deleteperson', { personId: personId }, function() {
            row.remove();
        });
    });

    function addPersonToTable(person) {
        var row = new EJS({ url: '/content/templates/person-row.html' })
            .render(person);
        $("#peopleTable").append($(row));
    }

    function addCarToTable(car) {
        var row = new EJS({ url: '/content/templates/car-row.html' })
            .render(car);
        $("#carsTable").append($(row));
    }

    function clearModals() {
        $(".modal input").val('');
    }
});