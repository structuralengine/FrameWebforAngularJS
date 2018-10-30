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
        json += construct(data, 'nodes', node_item, 'float');
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
        json += construct(data, 'panels', panel_item, 'float');
        json += ',';
    }
    if (mode == 'elements' || mode == '') {
        json += '"element":';
        json += elementJson(data);
        json += ',';
    }
    if (mode == 'joints' || mode == '') {
        json += '"joint":';
        json += jointJson(data);
        json += ',';
    }
    if (mode == 'notice_points' || mode == '') {
        json += '"notice_point":';
        json += noticePointJson(data);
        json += ',';
    }
    if (mode == 'fix_members' || mode == '') {
        json += '"fix_member":';
        json += fixMemberJson(data);
        json += ',';
    }
    if (mode == 'loads' || mode == '') {
        json += '"load":';
        json += loadJson(data);
        json += ',';
    }

    //最後の , を削除する
    json = json.slice(0, -1);

    console.log(json);

    return json;
}


const node_item = ['x', 'y', 'z'];
const panel_item = ['no1', 'no2', 'no3', 'area', 'e'];

//JSONデータを受け取って空欄に0を追加する（全て空ならNULLを返す）
function addZero(json, array1, array2, type){
    
    var flag = true;
    for(var i = 0; i < array1.length; i++) if(array1[i] in json){
        flag = false;
        break;
    }
    if(flag) return false;
    var dic = {};
    for(var i in array1){
        if(!(array1[i] in json)) continue;
        var x = json[array1[i]];
        var y;
        switch(type){
            case 'int': y = parseInt(x); break;
            case 'float': y = parseFloat(x); break;
            case 'string': y = String(x);
        }
        dic[array2[i]] = array1[i] in json ? y : 0.0;
    }
    return dic;
}

//整形用関数
function construct(json, name, item, type){

    if(!(name in json)) return '{}';
    const data = json[name];
    let dic = {};

    for(var i in data){
        var obj = addZero(data[i], item, item, type)
        if(!obj) continue;
        dic[parseInt(i) + 1] = obj;
    }
    return JSON.stringify(dic);
}

// member データの整形
function memberJson(json) {

    if (!('members' in json)) return '{}';

    const item = ['ni', 'nj', 'e'];
    const data = json.members;
    let dic = {};

    for (var i = 0; i < data.length; i++) {
        var x = addZero(data[i], item, item, 'int');
        if(!x) continue;
        dic[i] = x;
        dic[i] = addZero(dic[i], item, item, 'string');
    }
    return JSON.stringify(dic);
}

//fix_nodeデータの整形
function fixNodeJson(json){
    if(!('fix_nodes' in json)) return '{}';

    const item = ['tx', 'ty', 'tz', 'rx', 'ry', 'rz'];
    const data = json['fix_nodes'];
    let dic = {};

    for(var i in data){
        if(!('n' in data[i])) continue;
        for(var j = 0; j < 3; j++){
            var x;
            if(x = addZero(data[i], addStr(item, j + 1), item, 'float')){
                dic[j + 1] = [];
                dic[j + 1].push(x);
            }
        }
    }
    return JSON.stringify(dic);
}

//elementデータの整形
function elementJson(json){

    if(!('elements' in json)) return '{}';

    const item1 = ['E', 'G', 'Xp'];
    const item2 = ['A', 'J', 'Iy', 'Iz'];
    const data = json['elements'];
    let dic = {};

    for(var i in data){
        var obj = {};
        for(var j = 0; j < 3; j++){
            const x = addZero(data[i], item1 + item2, item1 + addStr(item2, i + 1), 'float');
            if(!x) continue;
            obj[i + 1] = x;
        }
        if(obj.size) dic[parseInt(i) + 1] = obj;
    }
    return JSON.stringify(dic);
}

//jointデータの整形
function jointJson(json){

    if(!('joints' in json))　return '{}';

    const item = ['xi', 'yi', 'zi', 'xj', 'yj', 'zj'];
    const data = json['joints'];
    let dic = {'1':[], '2':[], '3':[]};

    for(var i in data){
        if(!('no' in data[i])) continue;

        for(var j = 1; j <= 3; j++){
            const x = addZero(data[i], addStr(item, j), item, 'int');
            if(!x) continue;
            x['m'] = data[i]['no'];
            dic[j].push(x);
        }
    }
    return JSON.stringify(dic);
}

//notice_pointデータの整形
function noticePointJson(json){

    if(!('notice_points' in json)) return '{}';

    const item = mkNPList();
    const data = json['notice_points'];
    let dic = [];

    for(var i in data){
        if(!('no' in data[i])) continue;
        var obj = {};
        const x = addZero(data[i], item, item, 'float');
        if(!x) continue;
        obj['m'] = data[i]['no'];
        obj['Points'] = [];
        for(var j in x) obj['Points'].push(x[j]);

        dic.push(obj);
    }
    return JSON.stringify(dic);
}

//fix_membersデータの整形
function fixMemberJson(json){

    if(!('fix_members' in json)) return '{}';

    const data = json['fix_members'];
    const item = ['x', 'y', 'z', 'r'];
    let dic = {'1':[], '2':[], '3':[]};

    for(var i in data){
        if(!('no' in data[i])) continue;
        for(var j = 1; j <= 3; j++){
            var obj = addZero(data[i], addStr(item, j), item, 'float');
            if(!obj) continue;
            obj['m'] = data[i]['no'];
            dic[j].push(obj);
        }
    }
    return JSON.stringify(dic);
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


//配列の全要素の末尾にstrを付加する
function addStr(array, str){
    for(var i in array) array[i] = array[i] + str;
    return array;
}

function mkNPList(){
    var npv = [];
    for(var i = 0; i < 12; i++) npv[i] = 'L' + (i+1);
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
