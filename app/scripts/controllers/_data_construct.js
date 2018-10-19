// --------------------------------------------------------------
// #5　計算ボタンクリック時に送信するJSONデータの整形を行います。
// 戻り値: 整形後のJSONデータ 
// --------------------------------------------------------------

const nodev = ['x', 'y', 'z'];
const fix_nodev = fnlist();
const elementv = ['E', 'G', 'Xp', 'A1', 'J1', 'Iy1', 'Iz1', 'A2', 'J2', 'Iy2', 'Iz2', 'A3', 'J3', 'Iy3', 'Iz3'];
const panelv = ['no1', 'no2', 'no3', 'area', 'e'];
const jointv = jointlist();
const notice_pointv = nplist();
const fix_memberv = nplist();
const loadv = ['m1', 'm2', 'direction', 'mark', 'L1', 'L2', 'P1', 'P2', 'n', 'tx', 'ty', 'tz', 'rx', 'ry', 'rz'];
const combinev = ['1', '2', '3', '4', '5', '6'];

function DataConstruct(_mode) {

    const mode = (_mode + '').trim();

    // この変数でJSON整形を行ってください。
    const storage = localStorage.getItem('webframe.2');
    const data = JSON.parse(storage);

    let json = '';

    if (mode == 'nodes' || mode == '') {
        json += '"node":';
        json += cnst(data, 'nodes', nodev);
        json += ',';
    }
    if (mode == 'fix_nodes' || mode == '') {
        json += '"fix_node":';
        json += cnst(data, 'fix_nodes', fix_nodev);
        json += ',';
    }
    if (mode == 'members' || mode == '') {
        json += '"member":';
        json += memberJson(data);
        json += ',';
    }
    if (mode == 'panels' || mode == '') {
        json += '"panel":';
        json += cnst(data, 'panels', panelv);
        json += ',';
    }
    if (mode == 'elements' || mode == '') {
        json += '"element":';
        json += cnst(data, 'elements', elementv);
        json += ',';
    }
    if (mode == 'joints' || mode == '') {
        json += '"joint":';
        json += cnst(data, 'joints', jointv);
        json += ',';
    }
    if (mode == 'notice_points' || mode == '') {
        json += '"notice_point":';
        json += cnst(data, 'notice_points', notice_pointv);
        json += ',';
    }
    if (mode == 'fix_members' || mode == '') {
        json += '"fix_member":';
        json += cnst(data, 'fix_member', fix_memberv);
        json += ',';
    }
    if (mode == 'loads' || mode == '') {
        json += '"load":';
        json += cnst(data, 'loads', loadv);
        json += ',';
    }
    if (mode == 'combines' || mode == '') {
        json += '"combine":';
        json += cnst(data, 'combines', combinev);
        json += ',';
    }
    //最後の , を削除する
    json = json.slice(0, -1);
    // 整形したJSONデータを戻り値にセットしてください。
    console.log(json);
    return json;
}


//整形用関数
function cnst(json, name, array){

    if(!(name in json)){
        return '{}';
    }

    const data = json[name];
    let dic = {};

    for(var i in data){

        var x = [];
        for(var j in array) x[j] = (array[j] in data[i]) ? parseFloat(data[i][array[j]]) : NaN;

        var flag = 1;
        for(var j in array) flag *= isNaN(x[j]);
        if(flag) continue;

        var y = [];
        for(var j in array) y[j] = ($.isNumeric(x[j])) ? x[j] : 0.0;

        var obj = {};
        for(var j in array) obj[array[j]] = y[j];

        dic[i + 1] = obj;

    }

    const str = JSON.stringify(dic);
    return str;

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
    const str = JSON.stringify(node);
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

function fnlist(){
    var c1 = ['t', 'r'];
    var c2 = ['x', 'y', 'z'];
    var fnv = [];
    var n = 0;
    for(var i = 0; i < 3; i++){
        for(var j = 0; j < 2; j++){
            for(var k = 0; k < 3; k++){
                fnv[n] = c1[j] + c2[k] + (i+1);
                n++;
            }
        }
    }
    return fnv;
}

function jointlist(){
    var jointv = [];
    var n = 0;
    for (var i = 0; i < 3; i++){
        for(var j = 0; j < 2; j++){
            for(var k = 0; k < 3; k++){
                jointv[n] = nodev[k] + (j+1) + (i+1);
                n++;
            }
        }
    }
    return jointv;
}

function nplist(){
    var npv = [];
    for(var i = 0; i < 12; i++){
        npv[i] = 'L' + (i+1);
    }
    return npv;
}


// --------------------------------------------------------------
// $httpでのリクエスト送信処理
// --------------------------------------------------------------
function HttpSendRequest($http) {
    
    // テスト用アカウントでアクセス
    var userName = 'test1105';
    var password = 'test1105';

    // JSONの整形
    var data = DataConstruct('');
    
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
