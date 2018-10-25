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
            SendDataToUnity('');
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

function SendDataToUnity(mode_name) {

    // JSONの整形
    const data = DataConstruct(mode_name);
    const sendJson = '{' + data + '}';

    if (mode_name == '') {
        console.log('ReceiveData');
        console.log(sendJson);
        gameInstance.SendMessage('ExternalConnect', 'ReceiveData', sendJson);
    } else {
        console.log('ReceiveModeData');
        console.log(sendJson);
        gameInstance.SendMessage('ExternalConnect', 'ReceiveModeData', mode_name, sendJson);
    }
}


// Html->Unityへキャプチャを行うための信号を送る
function SendCaptureToUnity() {
    console.log('SendCapture');
    gameInstance.SendMessage('ExternalConnect','SendCapture');
}

function SendModeToUnity(templateUrl) {

    let mode_name = templateUrl + ''; //toString() と同じことしてる

    mode_name = mode_name.replace('#!/', '')
    mode_name = mode_name.replace('views/', '')
    mode_name = mode_name.replace('.html', '')
    mode_name.trim();
        
    if (mode_name == '') {
        mode_name = 'nodes'; // デフォルトの設定
    }

    console.log('ChengeMode');
    console.log(mode_name);
    gameInstance.SendMessage('ExternalConnect', 'ChengeMode', mode_name);
}
