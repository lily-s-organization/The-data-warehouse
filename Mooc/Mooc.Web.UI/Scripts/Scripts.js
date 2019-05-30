function jQueryAjaxPost(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
      //  alert("valid ok");
        var ajaxConfig = {
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            dataType: "json", 
            success: function (response) {
                if (response.success) {
                    alert("success");
                }
                else {
                    alert("fail");
                }
               
            },
            error: function () {
                alert("error");
            }
        }
       
        $.ajax(ajaxConfig);

    }
    return false;
}


function Delete(url) {
    if (confirm('Are you sure to delete this record ?') == true) {
        $.ajax({
            type: 'POST',
            url: url,
            success: function (response) {
                alert("success");
            },
            error: function () {
                alert("error");
            }
    

        });

    }
}

function AfterDelete(data, status, xhr)
{    
    var id = data.userid
    console.log(id);
    alert(id);
    $("#" + id).remove();
   // $('#19').remove();
    alert(data.message);
}

function AfterAdd(data, status, xhr) {
    
    alert(data.message);
    $("input").val("");
    
}

function onSuccess(data, status, xhr, id) {
    alert("enter");    //alert弹出错误提示信息。
    var $form = $(id);
    $form.reset();//清空form表单。    
}
