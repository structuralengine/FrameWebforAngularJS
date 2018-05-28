const gameInstance = UnityLoader.instantiate('gameContainer', 'unity/Build/unity.json', { onProgress: UnityProgress });

// Unity→Html
function receiveUnity(str) {
    // Unity から受け取った str を AngularJS に受け渡す方法がわからない
    console.log(str);
}
// Html→Unity
function sendUnity(str) {
    gameInstance.SendMessage('Button', 'UpdateTarget', str);
}
