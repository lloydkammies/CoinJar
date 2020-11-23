function InsertCoin(coinAmount) {

    $.post('Jar/InsertCoin', { 'coinValue': coinAmount },
        function (returnedData) {
            console.log(returnedData);
            if (returnedData.status === "Warning" && returnedData.data.Message ==="Jar-Full") {
                $('#full-jar-paragraph').show();
            }
            GetTotalAmount();
        }).fail(function (returnedData) {
            console.log(returnedData.responseJSON.status + ": " + returnedData.responseJSON.data.message);
        });
}

function GetTotalAmount() {
    $.get('Jar/GetTotalAmount',
        function (returnedData) {
            if (returnedData.status === "Success")
                $('#total_coins').text(currency(returnedData.data).format());
        }).fail(function () {
            console.log("Error: Failed to get total amount in jar.");
        });
}

function ResetTotals() {
    $.post('Jar/Reset',
        function (returnedData) {
            if (returnedData.status === "Success") {
                $('#total_coins').text(currency(0).format());
                $('#full-jar-paragraph').hide();
            }
        }).fail(function () {
            console.log("Error: Failed to not reset jar to zero.");
        });
}