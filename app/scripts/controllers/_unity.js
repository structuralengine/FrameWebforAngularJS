const gameInstance = UnityLoader.instantiate('gameContainer', 'unity/Build/unity.json', { onProgress: UnityProgress });

// Unity→Html (UnityからJS内でイベント発火)
function ReceiveUnity(str) {
    // Unity から受け取った str を AngularJS に受け渡す方法がわからない
    alert(str);
}
// Html→Unity (JSからUnity内でイベント発火)
function SendUnity(str) {
    gameInstance.SendMessage('MainFrameManager', 'ReceiveAngular', str);
}
