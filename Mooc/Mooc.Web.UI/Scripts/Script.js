


function TestEmail(email)
{
    var reg = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
    isok = reg.test(email);
    if (!isok) {
        alert("邮箱格式不正确，请重新输入！");

        return false;
    }
    else
    {

        return true;
    }

}