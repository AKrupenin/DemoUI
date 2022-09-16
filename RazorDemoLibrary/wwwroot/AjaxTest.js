function GetServerTime() {
    $.ajax({
        url: '/api/Ajax/Test',
        data: {

        }
    })
        .done(function (result) {
            alert(result);
        });
}