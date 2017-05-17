function testfunction() {
    $.ajax({
        url: testurl,
        type: "POST",
        success: function (result) {
            if (result.success) {
                $('#cardForm').submit();
            }
            else {
                alert(result.error)
            }
        }
    })
}

