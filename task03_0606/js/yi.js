$('#twzipcode').twzipcode({
    'detect': true
});

$(document).ready(function () {
    //$("#ConfirmPassword").blur(function () {
    //    alert("OK")
    //})

    if ($("#Password1").val() !== $("#ConfirmPassword").val()) {
        $("#ConfPwdLabel").html('確認密碼 <i class="fa fa-times" style="color: red;"></i> 密碼不相符');
        $("#submitBtn").attr({ type: "button" });
        //$("#ConfirmPassword").tooltip("show");
    }

    $("#ConfirmPassword").blur(function () {
        if ($("#Password1").val() !== $("#ConfirmPassword").val()) {
            $("#ConfPwdLabel").html('確認密碼 <i class="fa fa-times" style="color: red;"></i> 密碼不相符');
            $("#submitBtn").attr({ type: "button" });
            //$("#ConfirmPassword").tooltip("show");
        }
        if ($("#Password1").val() === $("#ConfirmPassword").val()) {
            $("#ConfPwdLabel").html('確認密碼 <i class="fa fa-check" style="color: green;"></i>');
            $("#submitBtn").attr({ type: "submit" });
            //$("#ConfirmPassword").tooltip("hide");
        }

    });

    $("#button1").click(function () {
        swal({
            type: 'success',
            title: '成功放入購物車',
            showConfirmButton: false,
            timer: 1000
        })
    });
    $("#button2").click(function () {
        swal({
            type: 'success',
            title: '成功放入購物車',
            showConfirmButton: false,
            timer: 1000
        })
    });
});