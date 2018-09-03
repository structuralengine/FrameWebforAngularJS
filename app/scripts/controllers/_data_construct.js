// --------------------------------------------------------------
// #5　計算ボタンクリック時に送信するJSONデータの整形を行います。
// 戻り値: 整形後のJSONデータ 
// --------------------------------------------------------------
function DataConstruct() {

    // この変数でJSON整形を行ってください。
    var json = '';
    
    // 整形したJSONデータを戻り値にセットしてください。
    return json;
}

// --------------------------------------------------------------
// $httpでのリクエスト送信処理
// --------------------------------------------------------------
function HttpSendRequest($http) {
    
    // テスト用アカウントでアクセス
    var userName = 'test1105';
    var password = 'test1105';

    var storage = localStorage.getItem('webframe.2').replace('{', '');  // 最初の { を消す

    // JSONの整形
    var json = DataConstruct();
    
    // DataConstruct関数内でJSONの整形が完結する場合は、以下の処理はコメントアウトしてください。
    json = 'inp_grid=' + '{' + '"username":' + JSON.stringify(userName) + ',"password":' + JSON.stringify(password)+','+storage;
    
    $http({
        method:'POST',
        url: 'http://structuralengine.com/FrameWeb/api/Web_Api.py',
        data:json,
        headers: {'Content-Type': 'application/x-www-form-urlencoded'}
      })
      .then(function onSuccess(response) {
        console.log( response.data );

        // グレーアウトしているタブメニューを活性化
        EnableMenu();
      }, function onError(response) {
        
        //通信に失敗
        console.log(response.data);
      });
}

// --------------------------------------------------------------
// グレーアウトしてあるメニューをアクティブにする
// --------------------------------------------------------------
function EnableMenu() {
    document.getElementById('enable_disg_id').className='enable_link';
    document.getElementById('enable_reac_id').className='enable_link';
    document.getElementById('enable_fsec_id').className='enable_link';
    document.getElementById('enable_comb_id').className='enable_link';
    document.getElementById('enable_pic_id').className='enable_link';
}
