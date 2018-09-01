'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:CombFsecCtrl
 * @description
 * # CombFsecCtrl
 * Controller of the webframe
 */
angular.module('webframe')
    .controller('CombFsecCtrl', ['$scope', '$window', '$lowdb',
        function ($scope, $window, $lowdb) {
            $scope.testCombFsec = function () {
                alert('メニュー「comb.fsec」');
            };
        }
    ]);

