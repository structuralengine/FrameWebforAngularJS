// --------------------------------------------------------------
// #5　計算ボタンクリック時に送信するJSONデータの整形を行います。
// 戻り値: 整形後のJSONデータ 
// --------------------------------------------------------------

function AllDataConstruct() {
    //const storage = localStorage.getItem('webframe.2');
    const storage = localStorage.getItem('webframe.2');
    return DataConstruct('', storage);
}

function DataConstruct(_mode, _jsonObj) {

    const mode = (_mode + '').trim();

    // 引数を整形する
    let jsonStr = '';
    if (mode != '') {
        jsonStr = JSON.stringify(_jsonObj); 
        jsonStr = '{"'+ mode + '":' + jsonStr + '}'
    } else {
        jsonStr = _jsonObj;
    }
    const data = JSON.parse(jsonStr);

    // Unityや計算サーバーが解釈できる JSONを生成する
    let json = '';

    if (mode == 'nodes' || mode == '') {
        json += '"node":';
        json += nodeJson(data);
        json += ',';
    }
    if (mode == 'fix_nodes' || mode == '') {
        json += '"fix_node":';
        json += fixNodeJson(data);
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
        json += elementJson(data);
        json += ',';
    }
    if (mode == 'joints' || mode == '') {
        json += '"joint":';
        json += jointJson(data, jointv);
        json += ',';
    }
    if (mode == 'notice_points' || mode == '') {
        json += '"notice_point":';
        json += noticePointJson(data, notice_pointv);
        json += ',';
    }
    if (mode == 'fix_members' || mode == '') {
        json += '"fix_member":';
        json += fixMemberJson(data, fix_memberv);
        json += ',';
    }
    if (mode == 'loads' || mode == '') {
        json += '"load":';
        json += loadJson(data);
        json += ',';
    }

    //最後の , を削除する
    json = json.slice(0, -1);

    return json;
}


const nodev = ['x', 'y', 'z'];
const fix_nodev = fnlist();
const panelv = ['no1', 'no2', 'no3', 'area', 'e'];
const jointv = jointlist();
const notice_pointv = nplist();
const fix_memberv = fmlist();


//空欄に0を追加する
function compromise(json, array1, array2){
    var flag = true;
    for(var i in array1) if(array1[i] in json){
        flag = false;
        break;
    }
    if(flag) return null;
    var dic = {};
    for(var i in array1){
        dic[array2[i]] = array1[i] in json ? parseFloat(json[array1[i]]) : 0.0;
    }
    return dic;
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

        dic[String(parseInt(i) + 1)] = obj;

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

//fix_nodeデータの整形
function fixNodeJson(json){
    if(!('fix_nodes' in json)) return '{}';

    const array = ['tx', 'ty', 'tz', 'rx', 'ry', 'rz'];
    var item = [];
    item[1] = ['tx1', 'ty1', 'tz1', 'rx1', 'ry1', 'rz1'];
    item[2] = ['tx2', 'ty2', 'tz2', 'rx2', 'ry2', 'rz2'];
    item[3] = ['tx3', 'ty3', 'tz3', 'rx3', 'ry3', 'rz3'];
    const fix_node_item = [item[1], item[2], item[3]];
    const data = json['fix_nodes'];
    let dic = {};

    for(var i in data){
        if(!('n' in data[i])) continue;
        for(var j = 0; j < 3; j++){
            var x;
            if(x = compromise(data[i], fix_node_item[j], array)){
                dic[j + 1] = [];
                dic[j + 1].push(x);
            }
        }
    }
    return JSON.stringify(dic);
}

//elementデータの整形
function elementJson(json){

    const array = ['E', 'G', 'Xp', 'A1', 'J1', 'Iy1', 'Iz1',
        'A2', 'J2', 'Iy2', 'Iz2', 'A3', 'J3', 'Iy3', 'Iz3'];
    const item = ['E', 'G', 'Xp', 'A', 'J', 'Iy', 'Iz'];

    if(!('elements' in json)) return '{}';

    const data = json['elements'];
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
        for(var j = 0; j < 3; j++){
            obj[String(j + 1)] = {};
            for(var k = 0; k < 3; k++) obj[String(j + 1)][item[k]] = y[k];
            for(var k = 0; k < 4; k++) obj[String(j + 1)][item[k + 3]] = y[4 * j + k + 3];
        }

        dic[String(parseInt(i) + 1)] = obj;

    }

    const str = JSON.stringify(dic);
    return str;

}

//jointデータの整形
function jointJson(json, array){

    const item = ['xi', 'yi', 'zi', 'xj', 'yj', 'zj'];

    if(!('joints' in json)){
        return '{}';
    }

    const data = json['joints'];
    let dic = {};

    for(var i in data){

        var x = [];
        for(var j in array) x[j] = (array[j] in data[i]) ? parseFloat(data[i][array[j]]) : NaN;

        var flag = 1;
        for(var j in array) flag *= isNaN(x[j]);
        if(flag) continue;

        var y = [];
        for(var j in array) y[j] = ($.isNumeric(x[j])) ? x[j] : 0.0;

        var obj = [];
        for(var j = 0; j < 3; j++){
            var flag = false;
            for(var k = 0; k < 6; k++) if(y[6 * j + k]) flag = true;
            if(!flag) continue;

            obj[j] = {};
            obj[j]['m'] = String(j + 1);
            for(var k = 0; k < 6; k++) obj[j][item[k]] = y[6 * j + k];
        }

        dic[String(parseInt(i) + 1)] = obj;

    }

    const str = JSON.stringify(dic);
    return str;

}

//notice_pointデータの整形
function noticePointJson(json, array){

    if(!('notice_points' in json)){
        return '{}';
    }

    const data = json['notice_points'];
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
        obj['m'] = String(parseInt(i) + 1);
        obj['Points'] = [];
        for(var j = 0; j < array.length; j++) obj['Points'][j] = y[j];

        dic[String(parseInt(i) + 1)] = obj;

    }

    const str = JSON.stringify(dic);
    return str;

}

//fix_membersデータの整形
function fixMemberJson(json, array){

    const item = ['tx', 'ty', 'tz', 'tr'];

    if(!('fix_members' in json)){
        return '{}';
    }

    const data = json['fix_members'];
    let dic = {};

    for(var i in data){

        var x = [];
        for(var j in array) x[j] = (array[j] in data[i]) ? parseFloat(data[i][array[j]]) : NaN;

        var flag = 1;
        for(var j in array) flag *= isNaN(x[j]);
        if(flag) continue;

        var y = [];
        for(var j in array) y[j] = ($.isNumeric(x[j])) ? x[j] : 0.0;

        var obj = [];
        for(var j = 0; j < 3; j++){
            flag = 0;
            for(var k = 0; k < 4; k++) if(y[4 * j + k]) flag = 1;
            if(!flag) continue;
            
            obj[j] = {};
            obj[j]['m'] = String(parseInt(j) + 1);
            for(var k = 0; k < 4; k++) obj[j][item[k]] = y[4 * j + k];
        }

        dic[String(parseInt(i))] = obj;

    }

    const str = JSON.stringify(dic);
    return str;

}

//loadデータの整形
function loadJson(json){

    const array = ['no', 'm1', 'm2', 'direction', 'mark',
        'L1', 'L2', 'P1', 'P2', 'n', 'tx', 'ty', 'tz', 'rx', 'ry', 'rz'];
    const itemName = ['fn', 'fm', 'fsec', 'joint'];
    const item = ['fix_node', 'fix_member', 'element', 'joint'];
    if( !('loads' in json && 'load_names' in json) ) return '{}';

    const data1 = json['loads'];
    const data2 = json['load_names'];
    let dic = {};

    for(var i in data2){

        var x2 = [];
        for(var j = 0; j < itemName.length; j++){
            x2[j] = (itemName[j] in data2[i]) ? parseInt(data2[i][itemName[j]]) : NaN;
        }

        var flag = 1;
        for(var j in array) flag *= isNaN(x2[j]);
        if(flag) continue;

        var y2 = [];
        for(var j in itemName) y2[j] = ($.isNumeric(x2[j])) ? x2[j] : 1;

        dic[String(parseInt(i) + 1)] = {};
        for(var j = 0; j < 4; j++){
            dic[String(parseInt(i) + 1)][item[j]] = y2[j];
        }

    }

    for(var i in data1){

        if(!('n' in data1[i] && 'm1' in data1[i] && 'm2' in data1[i]
            && 'direction' in data1[i] && 'mark' in data1[i] && 'no' in data1[i])) continue;
        if(!(String(data1[i]['no']) in dic)) continue;

        var x1 = [];
        for(var j = 0; j < 4; j++) x1[array[j]] = String(data1[i][array[j]]);
        x1['mark'] = parseInt(data1[i]['mark']);
        x1['n'] = String(data1[i]['n']);
        for(var j = 5; j < array.length; j++) {
            if(array[j] != 'n') x1[array[j]] = (array[j] in data1[i]) ? parseFloat(data1[i][array[j]]) : NaN;
        }

        var flag = 1;
        for(var j in array) flag *= isNaN(x1[array[j]]);
        if(flag) continue;

        var y1 = [];
        for(var j = 0; j < 4; j++) y1[array[j]] = x1[array[j]];
        y1['mark'] = x1['mark'];
        x1['n'] = x1['n'];
        for(var j = 5; j < array.length; j++){
            if(array[j] != 'n') y1[array[j]] = ($.isNumeric(x1[array[j]])) ? x1[array[j]] : 0.0;
        }

        var obj1 = {};
        var obj2 = {};
        for(var j = 1; j < 9; j++) obj1[array[j]] = y1[array[j]];
        for(var j = 9; j < array.length; j++) obj2[array[j]] = y1[array[j]];

        dic[(y1['no'])]['load_node'] = obj1;
        dic[(y1['no'])]['load_member'] = obj2;

    }

    const str = JSON.stringify(dic);
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

function fmlist(){
    var array = ['x', 'y', 'z', 'r'];
    var fmv = [];
    var n = 0;
    for(var i = 0; i < 3; i++){
        for(var j = 0; j < 4; j++){
            fmv[n] = array[j] + (i + 1);
            n++;
        }
    }
    return fmv;
}


// --------------------------------------------------------------
// $httpでのリクエスト送信処理
// --------------------------------------------------------------
function HttpSendRequest($http) {
    
    // テスト用アカウントでアクセス
    var userName = 'test1105';
    var password = 'test1105';

    // JSONの整形
    var data = AllDataConstruct();
    
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
