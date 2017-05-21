var FollowingServices = function () {
    var CreateFollowing = function (gigId, done, fail) {
        $.post("/api/Followings", { FolloweeId: gigId })
            .done(done).fail(fail);
    };

    var unfollow = function (gigId, done, fail) {
        $.ajax({
            method: "DELETE",
            url: "/api/followings/" + gigId

        }).done(done).fail(fail);
    }

    return {
        CreateFollowing: CreateFollowing,
        unfollow: unfollow
    }
}();
