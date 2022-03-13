
function addFriend(Id) {

    console.log(Id);

    var data = {
        id: Id
    }

    $.post("/Home/friend_request", data)
        .done(function (result) {

            console.log(result);
        })
}

function removeFriend(Id) {

    console.log(Id);

    var data = {
        id: Id
    }

    $.post("/Home/delete_friend", data)
        .done(function (result) {

            console.log(result);
        })
}