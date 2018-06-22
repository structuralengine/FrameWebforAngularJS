'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:FixNodesCtrl
 * @description
 * # FixNodesCtrl
 * Controller of the webframe
 */
angular.module('webframe')
    .controller('FixNodesCtrl', ['$scope', '$window', '$lowdb',
        function ($scope, $window, $lowdb) {
            $scope.testunity = function () {

                const collection = $lowdb;
                let sendJson = '{';

                sendJson += '"node":';
                
                sendJson += JSON.stringify(collection.get('nodes').value());

                sendJson += ',"member":';
                sendJson += JSON.stringify(collection.get('members').value());

                sendJson += ',"panel":';
                sendJson += JSON.stringify(collection.get('panels').value());

                sendJson += ',"element":';
                sendJson += JSON.stringify(collection.get('elements').value());

                sendJson += '}';
                
                console.log(sendJson);
                $window.SendUnity(sendJson);
            };
        }
    ]);

