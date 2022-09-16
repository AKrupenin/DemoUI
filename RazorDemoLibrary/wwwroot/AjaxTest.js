function GetServerTime() {
    $.ajax({
        url: '/UI/GetServerTimeHandler',
        data: {

        }
    })
        .done(function (result) {
            alert(result);
        });
}