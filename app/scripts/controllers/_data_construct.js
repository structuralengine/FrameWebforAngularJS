// --------------------------------------------------------------
// #5　計算ボタンクリック時に送信するJSONデータの整形を行います。
// 引数: json : 整形前のJSONデータ
// 戻り値: 整形後のJSONデータ 
// --------------------------------------------------------------
function DataConstruct(json) {

    // 整形したJSONデータを戻り値にセットしてください。
    
    return json;
}

// --------------------------------------------------------------
// $httpでのリクエスト送信処理
// --------------------------------------------------------------
function HttpSendRequest($http, json) {
    
    json = DataConstruct(json);

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
