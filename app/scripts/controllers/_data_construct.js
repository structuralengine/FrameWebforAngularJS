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
    flag = false;
    for(var i in array1){
        if (!(array1[i] in json)) {
            continue;
        }
        var x = json[array1[i]];
        var y;
        switch(type){
            case 'int':{
                y = parseInt(x); 
                if (!isNaN(y)) {
                    flag = true;
                    dic[array2[i]] = array1[i] in json ? y : 0;
                } else {
                    dic[array2[i]] = 0;
                }
                break;
            }
            case 'float':{
                y = parseFloat(x);
                if (!isNaN(y)) {
                    flag = true;
                    dic[array2[i]] = array1[i] in json ? y : 0.0;
                } else {
                    dic[array2[i]] = 0.0;
                }
                break;
            }
            case 'string':{
                y = String(x);
                if (y.length > 0) {
                    flag = true;
                    dic[array2[i]] = array1[i] in json ? y : 1;
                } else {
                    dic[array2[i]] = '';
                }
            }
        }
    }
    if (flag == true) {
        return dic;
    }
    return false;
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
    let dic = {'1':[], '2':[], '3':[]};

    for(var i in data){
        if(!('n' in data[i])) continue;
        for(var j = 0; j < 3; j++){
            var x = addZero(data[i], addStr(item, j + 1), item, 'float');
            if (!x) continue;
            x['n'] = data[i]['n'];
            dic[j + 1].push(x);
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

    for (var i in data) {
        var obj = {};
        
        for (var j = 0; j < 3; j++) {
            var items1 = item1.concat(item2);
            var items2 = item1.concat(addStr(item2, i + 1));
            const x = addZero(data[i], items1, items2, 'float');
            if (!x) continue;
            obj[i + 1] = x;
        }
        if (Object.keys(obj).length) { 
            dic[parseInt(i) + 1] = obj;
        }
    }
    return JSON.stringify(dic);
}

//jointデータの整形
function jointJson(json){

    if(!('joints' in json))　return '{}';

    const item = ['x1', 'y1', 'z1', 'x2', 'y2', 'z2'];
    const data = json['joints'];
    let dic = {'1':[], '2':[], '3':[]};

    for(var i in data){
        if(!('no' in data[i])) continue;

        for(var j = 1; j <= 3; j++){
            var x = addZero(data[i], addStr(item, j), item, 'int');
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

    // return '{}';

    if( !('loads' in json && 'load_names' in json) ) return '{}';

    const member_str_item = ['m1', 'm2', 'direction'];
    const member_item = ['L1', 'L2', 'P1', 'P2'];
    const node_item = ['tx', 'ty', 'tz', 'rx', 'ry', 'rz'];
    const name_item = ['fn', 'fm', 'fsec', 'joint'];
    const name_item_full = ['fix_node', 'fix_member', 'element', 'joint'];
    const data = json['loads'];
    const name_data = json['load_names'];
    let dic = {};

    for(var i in name_data){
        if(!('no' in name_data[i])) continue;
        var obj = addZero(name_data[i], name_item, name_item_full, 'int');
        if(!obj){
            obj = {};
            for(var j = 0; j < name_item.length; j++){
                obj[name_item_full[j]] = 1;
            }
        }
        obj['load_node'] = [];
        obj['load_member'] = [];
        dic[name_data[i]['no']] = obj;
    }

    for(var i in data){
        if(!('no' in data[i] && 
            'direction' in data[i] &&
            'mark' in data[i] &&
            'n' in data[i]) ||
            !('m1' in data[i] || 'm2' in data[i])) continue;

        var obj = addZero(data[i], node_item, node_item, 'float');
        obj['n'] = data[i]['n']
        dic[data[i]['no']]['load_node'].push(obj);

        obj = addZero(data[i], member_item, member_item, 'float');
        obj['m'] = String(data[i]['m1']);
        obj['direction'] = String(data[i]['direction']);
        obj['mark'] = parseInt(data[i]['mark']);        
        dic[data[i]['no']]['load_member'].push(obj);        
    }
    return JSON.stringify(dic);
}


//配列の全要素の末尾にstrを付加する
function addStr(array, str){
    var v = [];
    for(var i in array) v[i] = array[i] + str;
    return v;
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
