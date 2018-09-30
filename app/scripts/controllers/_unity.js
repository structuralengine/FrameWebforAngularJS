'use strict';
/**
 * # _unity.js
 *
 */
const gameInstance = UnityLoader.instantiate('gameContainer', 'unity/Build/unity.json', { onProgress: UnityProgress });

// Unity→Html (UnityからJS内でイベント発火)
function ReceiveUnity(str) {

    switch (str) {
        case 'GetInputJSON':
            // 入力データを要求された
            SendJsonToUnity();
            break;
        
        case 'GetInputMode':
            // 入力モードを要求された
            SendInputMode(location.pathname);
            break;
    }
}

function SendJsonToUnity() {
    let sendJson = DataConstruct();
    //SendUnity('input json @' + storage);
}


// Html→Unity (JSからUnity内でイベント発火)
function SendUnity(str) {
    SendJsonToUnity();
//   gameInstance.SendMessage('MainFrameManager', 'ReceiveAngular', str);
}

// Html->Unityへキャプチャを行うための信号を送る
function SendCapture() {
    gameInstance.SendMessage('Capture', 'Execute');
}

function SendInputMode(templateUrl) {
    console.log(templateUrl);
    switch (templateUrl) {
        case 'views/login.html':
            SendUnity('input mode change @ login');
            break;
        case 'views/account.html':
            SendUnity('input mode change @ account');
            break;
        case 'views/nodes.html':
            SendUnity('input mode change @ node');
            break;
        case 'views/fix_nodes.html':
            SendUnity('input mode change @ fix_nodes');
            break;
        case 'views/members.html':
            SendUnity('input mode change @ member');
            break;
        case 'views/panels.html':
            SendUnity('input mode change @ panel');
            break;
        case 'views/elements.html':
            SendUnity('input mode change @ elements');
            break;
        case 'views/joints.html':
            SendUnity('input mode change @ joints');
            break;
        case 'views/notice_points.html':
            SendUnity('input mode change @ notice_points');
            break;
        case 'views/fix_members.html':
            SendUnity('input mode change @ fix_members');
            break;
        case 'views/loads.html':
            SendUnity('input mode change @ loads');
            break;
        case 'views/combines.html':
            SendUnity('input mode change @ combines');
            break;
        case 'views/disg.html':
            SendUnity('input mode change @ disg');
            break;
        case 'views/fsec.html':
            SendUnity('input mode change @ fsec');
            break;
        case 'views/comb.fsec.html':
            SendUnity('input mode change @ comb.fsec');
            break;
        case 'views/pic.fsec.html':
            SendUnity('input mode change @ pic.fsec');
            break;
        case 'views/reac.html':
            SendUnity('input mode change @ reac');
            break;
    }
}
