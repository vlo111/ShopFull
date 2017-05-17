function openModal() {
    $('#PayModal').modal('show')
}
function CheckDate() {
    var month = $('#ExpirationDateMonth').val();
    var d = new Date();
    var n = d.getMonth() + 1;
    if (month < n) {
        $('#DateError').html("Expiration date is not valid")

        return false;
    }
    else {
        $('#DateError').html("");
        return true;
    }

    //$.ajax({
    //    type: "POST",
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    url: "WebService.asmx/WebMethodName",
    //    data: "{}",
    //    success: function (response) {

    //    }
    //});
}




function bindLinks() {
    $('#PayBtn').click(function () {
        $('#dialogContent').load(this.href, function () {
            $('#DialogDiv').modal('show');
            bindForm(this)
        })
        return false;
    })
}
$(bindLinks);

function bindForm(dialog) {

    $('form', dialog).submit(function () {
        $.validator.unobtrusive.parse("#pform");
        if ($('form', dialog).valid()) {
            var mform = this;
            $.ajax({
                type: mform.method,
                url: mform.action,
                data: $(mform).serialize(),
                success: function (response) {
                    if (response.success) {
                        $('#DialogDiv').modal('hide');
                        $("#testDiv").html(response.pmodel.NameOnCard)
                    }
                }
            });


        }

        return false;

    })



}