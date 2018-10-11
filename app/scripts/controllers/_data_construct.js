// --------------------------------------------------------------
// #5　計算ボタンクリック時に送信するJSONデータの整形を行います。
// 戻り値: 整形後のJSONデータ 
// --------------------------------------------------------------
function DataConstruct() {

    // この変数でJSON整形を行ってください。
    const storage = localStorage.getItem('webframe.2');
    const data = JSON.parse(storage);

    let json = '';

    json += '"node":';
    json += nodeJson(data);
    json += ',';

    json += '"member":';
    json += memberJson(data);

    // 整形したJSONデータを戻り値にセットしてください。
    return json;
}

// node データの整形
function nodeJson(json) {

    if (!('nodes' in json)) {
        return '{}';
    }

    const data = json.nodes;

    let node = {};
    let id = 0;

    for (var i in data) {

        id++;

        const x_ = ('x' in data[i]) ? parseFloat(data[i].x) : NaN;
        const y_ = ('y' in data[i]) ? parseFloat(data[i].y) : NaN;
        const z_ = ('z' in data[i]) ? parseFloat(data[i].z) : NaN;

        if (isNaN(x_) && isNaN(y_) && isNaN(z_)) {
            continue;
        }

        const _x = ($.isNumeric(x_)) ? x_ : 0.0;
        const _y = ($.isNumeric(y_)) ? y_ : 0.0;
        const _z = ($.isNumeric(z_)) ? z_ : 0.0;

        const obj = { x: _x, y: _y, z: _z };
        node[id] = obj;
    };
    const str = JSON.stringify(node) 
    return str;
}

// member データの整形
function memberJson(json) {

    if (!('members' in json)) {
        return '{}';
    }

    const data = json.members;

    let member = {};
    let id = 0;

    for (var i in data) {

        id++;

        const ni_ = ('ni' in data[i]) ? parseInt(data[i].ni) : NaN;
        const nj_ = ('nj' in data[i]) ? parseInt(data[i].nj) : NaN;
        const e_ = ('e' in data[i]) ? parseInt(data[i].e) : NaN;

        if (isNaN(ni_) && isNaN(nj_) && isNaN(e_)) {
            continue;
        }

        const _ni = ($.isNumeric(ni_)) ? ni_.toString() : '';
        const _nj = ($.isNumeric(nj_)) ? nj_.toString() : '';
        const _e = ($.isNumeric(e_)) ? e_.toString() : '';

        const obj = { ni: _ni, nj: _nj, e: _e, cg: 0 };
        member[id] = obj;
    };
    const str = JSON.stringify(member)
    return str;
}


// --------------------------------------------------------------
// $httpでのリクエスト送信処理
// --------------------------------------------------------------
function HttpSendRequest($http) {
    
    // テスト用アカウントでアクセス
    var userName = 'test1105';
    var password = 'test1105';

    // JSONの整形
    var data = DataConstruct();
    
    var json = 'inp_grid='
        + '{'
        + '"username":' + JSON.stringify(userName) + ','
        + '"password":' + JSON.stringify(password) + ','
        + data
        + '}';
    
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
