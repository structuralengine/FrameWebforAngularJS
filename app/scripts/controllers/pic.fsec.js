'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:PicFsecCtrl
 * @description
 * # PicFsecCtrl
 * Controller of the webframe
 */
angular.module('webframe')
    .controller('PicFsecCtrl', ['$scope', '$window', '$lowdb',
        function ($scope, $window, $lowdb) {
            $scope.testPicFsec = function () {
                alert('メニュー「pic.fsec」');
            };
        }
    ]);

