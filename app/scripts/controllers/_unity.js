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
            SendDataToUnity('');
            break;
        case 'GetInputMode':// 入力モードを要求された
            SendModeToUnity(location.hash);
            break;
    }
}

function SendDataToUnity(mode_name) {
    const sendJson = DataConstruct(mode_name);
    gameInstance.SendMessage('ExternalConnect', 'ReceiveData', sendJson);
}


// Html->Unityへキャプチャを行うための信号を送る
function SendCaptureToUnity() {
    gameInstance.SendMessage('ExternalConnect','SendCapture');
}

function SendModeToUnity(templateUrl) {

    let mode_name = templateUrl + ''; //toString() と同じことしてる

    mode_name = mode_name.replace('#!/', '')
    mode_name = mode_name.replace('views/', '')
    mode_name = mode_name.replace('.html', '')
    mode_name.trim();
        
    if (mode_name == '') {
        mode_name = 'node'; // デフォルトの設定
    }

    gameInstance.SendMessage('ExternalConnect', 'ChengeMode', mode_name);
}
