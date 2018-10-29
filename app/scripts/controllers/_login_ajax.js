// --------------------------------------------------------------
// #7a　ユーザーIDとパスワードでログインを行います。
// --------------------------------------------------------------
function Login() {
    var userid = $('#loginId').val();
    var pass = $('#loginPass').val();
    
    $('#myModal').modal('hide');

    var url = 'http://structuralengine.com/my-module/get_points_balance.php?id='+userid+'&ps='+pass;
    
    $.ajax({
        type:'GET',
        url: url,
        dataType:'json',
        timeout: 10000,
        headers: {'Content-Type': 'application/x-www-form-urlencoded'},
        crossDomain: true,
        cache: false,
        context: userid
    }).then(
        LoginSuccess,
        LoginError
    );
}

// --------------------------------------------------------------
// 成功時の処理
// --------------------------------------------------------------
function LoginSuccess(data)
{
    //ログイン失敗
    if ('error' in data) {
        $('#myModal').modal('show');
        alert("ユーザー名、またはパスワードが間違っています。");

    //ログイン成功
    } else {
        var result = 'ようこそ '+ this + ' さん、あなたの保有ポイントは ' + data["purchase_value"] + 'ポイントです';
        $("#loginStatus").text(result);
        $("#loginStatus").show();
    }
}

// --------------------------------------------------------------
// 終了時の処理
// --------------------------------------------------------------
function LoginError () {
    $('#myModal').modal('show');
    alert("接続に失敗しました。再度送信してください。");
}