function jQueryAjaxPost(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
      //  alert("valid ok");
        var ajaxConfig = {
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            success: function (response) {
                if (response.success) {
                    $("p").text("succeess");
                }
                else {
                    $("p").text("bad");
                }
            }
        }
       
        $.ajax(ajaxConfig);

    }
    return false;
}