$('#twzipcode').twzipcode({
    'detect': true
});

$(document).ready(function () {
    //$("#ConfirmPassword").blur(function () {
    //    alert("OK")
    //})

    $("#ConfirmPassword").blur(function () {
        if ($("#Password1").val() != $("#ConfirmPassword").val()) {
            $("#ConfPwdLabel").html('確認密碼 <i class="fas fa-times" style="color: red;"></i> 密碼不相符');
            //$("#ConfirmPassword").tooltip("show");
        }
        if ($("#Password1").val() == $("#ConfirmPassword").val()) {
            $("#ConfPwdLabel").html('確認密碼 <i class="fas fa-check" style="color: green;"></i>');
            //$("#ConfirmPassword").tooltip("hide");
        }

    })
})