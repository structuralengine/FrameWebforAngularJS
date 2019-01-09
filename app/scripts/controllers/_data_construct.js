// --------------------------------------------------------------
// #5　計算ボタンクリック時に送信するJSONデータの整形を行います。
// 戻り値: 整形後のJSONデータ 
// --------------------------------------------------------------

function AllDataConstruct() {
    //const storage = localStorage.getItem('webframe.2');
    const storage = localStorage.getItem('webframe.2');
    return DataConstruct('', storage);
}

function DataConstruct(_mode, _json) {

    const mode = (_mode + '').trim();

    // 引数を整形する
    let data = {};
    if (mode != '') {
        if (mode == 'loads') {
            data = _json;
        } else {
            data[mode] = _json;
        }
    } else {
        data = JSON.parse(_json)
    }

    // Unityや計算サーバーが解釈できる JSONを生成する
    let json = '';

    if (mode == 'nodes' || mode == '') {
        json += '"node":';
        json += construct(data, 'nodes', node_item, 'float');
        json += ',';
    }
    if (mode == 'members' || mode == '') {
        json += '"member":';
        json += memberJson(data);
        json += ',';
    }
    if (mode == 'elements' || mode == '') {
        json += '"element":';
        json += elementJson(data);
        json += ',';
    }
    if (mode == 'fix_nodes' || mode == '') {
        json += '"fix_node":';
        json += fixNodeJson(data);
        json += ',';
    }
    if (mode == 'panels' || mode == '') {
        json += '"panel":';
        json += construct(data, 'panels', panel_item, 'float');
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
    const keys = Object.keys(data);
    let dic = {};
   
    for (var i = 0; i < keys.length; i++) {
        const key = keys[i];
        const tmp = data[key];
        var obj = addZero(tmp, item, item, type)
        if(!obj) continue;
        dic[i+1] = obj;
    }

    return JSON.stringify(dic);
}

// member データの整形
function memberJson(json) {

    if (!('members' in json)) return '{}';

    const item = ['ni', 'nj', 'e'];
    const data = json.members;
    const keys = Object.keys(data);
    let dic = {};

    for (var i = 0; i < keys.length; i++) {
        const key = keys[i];
        const tmp = data[key];
        var x = addZero(tmp, item, item, 'int');
        if(!x) continue;
        dic[i+1] = x;
        dic[i+1] = addZero(dic[i+1], item, item, 'string');
    }
    return JSON.stringify(dic);
}

//fix_nodeデータの整形
function fixNodeJson(json){
    if(!('fix_nodes' in json)) return '{}';

    const item = ['tx', 'ty', 'tz', 'rx', 'ry', 'rz'];
    const data = json['fix_nodes'];
    const keys = Object.keys(data);
    let dic = {};

    for (var i = 1; i <= 3; i++) {
        var obj = [];
        for (var j = 0; j < keys.length; j++) {
            const key = keys[j];
            const tmp = data[key];
            if (!('n' in tmp)) continue;
            var x = addZero(tmp, addStr(item, i), item, 'float');
            if (!x) continue;
            x['n'] = tmp['n'];
            obj.push(x);
        }
        if (obj.length) {
            dic[i] = obj;
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
    const keys = Object.keys(data);
    let dic = {};

    for (var i = 1; i <= 3; i++) {
        var items1 = item1.concat(item2);
        var items2 = item1.concat(addStr(item2, i));
        var obj = {};
        for (var j = 0; j < keys.length; j++) {
            const key = keys[j];
            const x = addZero(data[key], items2, items1, 'float');
            if (!x) continue;
            for (var k in item2) {
                if (item2[k] in x) {
                    obj[j + 1] = x;
                    break;
                }
            }
        }
        if (Object.keys(obj).length) { 
            dic[i] = obj;
        }
    }
    return JSON.stringify(dic);
}

//jointデータの整形
function jointJson(json){

    if(!('joints' in json))　return '{}';

    const item = ['xi', 'yi', 'zi', 'xj', 'yj', 'zj'];
    const data = json['joints'];
    const keys = Object.keys(data);
    let dic = {};

    for (var i = 1; i <= 3; i++) {
        var obj = [];
        for (var j = 0; j < keys.length; j++) {
            const key = keys[j];
            const tmp = data[key];
            if (!('no' in tmp)) continue;
            var x = addZero(tmp, addStr(item, i), item, 'int');
            if(!x) continue;
            x['m'] = tmp['no'];
            obj.push(x);
        }
        if (obj.length) {
            dic[i] = obj;
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
    const keys = Object.keys(data);
    let dic = {};

    for (var i = 1; i <= 3; i++) {
        var obj = [];
        for (var j = 0; j < keys.length; j++) {
            const key = keys[j];
            const tmp = data[key];
            if (!('no' in tmp)) continue;
            var x = addZero(tmp, addStr(item, i), item, 'float');
            if(!x) continue;
            x['m'] = tmp['no'];
            obj.push(x);
        }
        if (obj.length) {
            dic[i] = obj;
        }
    }
    return JSON.stringify(dic);
}

//loadデータの整形
function loadJson(json){

    if (!('loads' in json) || !('load_names' in json)) return '{}';

    const member_str_item = ['m1', 'm2', 'direction'];
    const member_item = ['L1', 'L2', 'P1', 'P2'];
    const node_item = ['tx', 'ty', 'tz', 'rx', 'ry', 'rz'];
    const name_item = ['fn', 'fm', 'el', 'jo'];
    const name_item_full = ['fix_node', 'fix_member', 'element', 'joint'];
    const data = json['loads'];
    const name_data = json['load_names'];
    let dic = {};

    if ('load_names' in json) {
        for (var i in name_data) {
            
            if (!('no' in name_data[i])) continue;
            var no = name_data[i]['no']
            if (no==null) continue;
            
            var obj = addZero(name_data[i], name_item, name_item_full, 'int');
            if(!obj){
                obj = {};
                for(var j = 0; j < name_item.length; j++){
                    obj[name_item_full[j]] = 1;
                }
            }
            obj['load_node'] = [];
            obj['load_member'] = [];
            dic[no] = obj;
        }
    }

    if ('loads' in json) {

        for (var i in data) {
            
            if(!('no' in data[i])) continue;
            var no = data[i]['no'];

            var obj = addZero(data[i], node_item, node_item, 'float');
            if(obj){
                obj['n'] = data[i]['n']
                dic[no]['load_node'].push(obj);
            }

            if ('direction' in data[i] &&
                'mark' in data[i] &&
                ('m1' in data[i] || 'm2' in data[i])) {

                obj = addZero(data[i], member_item, member_item, 'float');
                if (obj) {
                    let i1 = parseInt(String(data[i]['m1']));
                    let i2 = parseInt(String(data[i]['m2']));
                    if (isNaN(i1) && isNaN(i2)) continue;
                    if (isNaN(i1)) i1 = i2;
                    if (isNaN(i2)) i2 = i1;
                    if (i1 > i2) {
                        const i3 = i1;
                        i1 = i2;
                        i2 = i3;
                    }
                    for (var j = i1; j < i2 + 1; j++) {
                        var tmp = {};
                        Object.assign(tmp, obj);
                        tmp['m'] = j.toString();
                        tmp['direction'] = String(data[i]['direction']);
                        tmp['mark'] = parseInt(data[i]['mark']);
                        dic[no]['load_member'].push(tmp);
                    }
                }
            }    
        }
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

// リクエスト送信用のユーザーID・パス
var userName = '';
var password = '';


// --------------------------------------------------------------
// $httpでのリクエスト送信処理
// --------------------------------------------------------------
function HttpSendRequest($http) {
    
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
