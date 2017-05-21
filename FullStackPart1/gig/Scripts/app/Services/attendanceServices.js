var AttendanceServices = function() {


    var createAttendendance = function(GigId, done, fail) {

        $.post("/api/Attendances", { GigId: GigId })
            .done(done).fail(fail);

    };

    var deleteAttendendance = function(gigId, done, fail) {
        $.ajax({
            url: "/api/Attendances/" + gigId,
            method: "DELETE"

        }).done(done).fail(fail);
    };


    return {
        createAttendendance: createAttendendance,
        deleteAttendendance: deleteAttendendance
    }
}();