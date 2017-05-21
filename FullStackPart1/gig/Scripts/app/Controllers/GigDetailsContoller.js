
var GigDetailsContoller = function (followingServices) {
    var toggleFollowing;
    var button;

    var init = function () { $(".js-toggle-Follow").on("click", toggleFollowing); }
    var done;
    var fail;
    toggleFollowing = function (e) {
        button = $(e.target);

        var followeeId = button.attr("data-gig-id");

        if (button.hasClass('btn-default')) {
            FollowingServices.CreateFollowing(followeeId, done, fail);
        } else if (button.hasClass('btn-info')) {
            FollowingServices.unfollow(followeeId, done, fail);
        }

    }
    done = function () {
        var text = (button.text() == "follow") ? "following" : "follow";
        button.toggleClass("btn-info").toggleClass("btn-default").text(text);

    };
    fail = function () {
        alert("Sonthing failed!");
    };

    return { init: init }
}(FollowingServices);