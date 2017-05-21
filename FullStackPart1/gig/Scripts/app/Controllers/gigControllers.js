var GigConrollers = function (attendanceServices) {
    var toggleAttendance;
    var button;

    var init = function (container) {

        $(container).on("click", ".js-toggle-attendance", toggleAttendance);
    };
    var done;
    var fail;
    toggleAttendance = function (e) {
        button = $(e.target);
        var gigId = button.attr("data-gig-id");
       
        if (button.hasClass("btn-default"))
            attendanceServices.createAttendendance(gigId, done, fail);
        else
            attendanceServices.deleteAttendendance(gigId, done, fail);
    };
    done = function () {
        var text = (button.text() == "Going") ? "Going?" : "Going";
        button.toggleClass("btn-info").toggleClass("btn-default").text(text);

    };
    fail = function () { alert("Sonthing failed!"); };
    return { init: init }

}(AttendanceServices);
