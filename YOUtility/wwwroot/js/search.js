$("#s_search_button").click(find_users);


function find_users() {
    var data = {
        search: "" + $(".s_input").val()
    }

    $.post("/Home/Find", data)
        .done(function (result) {
            var new_app = $("#s_search_template").clone();
            console.log(result);
            console.log(result.userName)
            var username = result.userName;
            var biography = result.biography;
            var address = result.address;
            var href = "/Home/Profile/" + result.id;
            console.log(href);

            $("#s_application_display").prepend(new_app);

            $(new_app).find(".card-header").html(username);
            $(new_app).find("h5").html(address);
            $(new_app).find(".card-text").html(biography);
            $(new_app).removeAttr("id");
            
            $(new_app).find(".btn-primary").attr("href", href);
            new_app.show();
        })
        .fail(function (result) {
        })
        .always(function () {
        });
    return false;
}