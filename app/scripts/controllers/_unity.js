'use strict';
/**
 * # _unity.js
 *
 */
const gameInstance = UnityLoader.instantiate('gameContainer', 'unity/Build/unity.json', { onProgress: UnityProgress });

// Unity→Html (UnityからJS内でイベント発火)
function ReceiveUnity(str) {
    switch (str) {
        case 'GetInputJSON':// 入力データを要求された
            console.log('Called!! - GetInputJSON');
            SendAllDataToUnity();
            break;
        case 'GetInputMode':// 入力モードを要求された
            console.log('Called!! - GetInputMode');
            SendModeToUnity(location.hash);
            break;
        default:
            console.log('Called!! - UnknownMessage');
            console.log(str);
            break;
    }
}
// Unity→Html (UnityからJS内でイベント発火)
function ReceiveUnitySelectItemChenge(id) {
    console.log('Called!! - Receive Unity Select Item Chenge');
    console.log(id);
}

function SendAllDataToUnity() {

    // JSONの整形
    const data = AllDataConstruct();
    const sendJson = '{' + data + '}';
    
    gameInstance.SendMessage('ExternalConnect', 'ReceiveData', sendJson);
}

function SendDataToUnity(mode_name, jsonObj) {

    // JSONの整形
    const data = DataConstruct(mode_name, jsonObj);
    const sendJson = '{' + data + '}';

    gameInstance.SendMessage('ExternalConnect', 'ReceiveModeData', sendJson);
}


// Html->Unityへキャプチャを行うための信号を送る
function SendCaptureToUnity() {
    console.log('SendCapture');
    gameInstance.SendMessage('ExternalConnect','SendCapture');
}

function SendModeToUnity(templateUrl) {

    if (typeof templateUrl == 'undefined')
        return;
    
    console.log('ChengeMode');

    let mode_name = GetModeName(templateUrl);
        
    if (mode_name == '') {
        mode_name = 'nodes'; // デフォルトの設定
    }

    console.log(mode_name);
    gameInstance.SendMessage('ExternalConnect', 'ChengeMode', mode_name);
}

function GetModeName(templateUrl) {

    let mode_name = templateUrl + ''; //toString() と同じことしてる

    mode_name = mode_name.replace('#!/', '')
    mode_name = mode_name.replace('views/', '')
    mode_name = mode_name.replace('.html', '')
    return mode_name.trim();  
}

function SendSelectItemToUnity(id) {
    console.log('SendSelectItemToUnity');
    const mode_name = GetModeName(location.hash);
    gameInstance.SendMessage('ExternalConnect', 'SelectItemChange', mode_name, id);
}