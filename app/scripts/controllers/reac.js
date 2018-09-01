'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:ReacCtrl
 * @description
 * # ReacCtrl
 * Controller of the webframe
 */
angular.module('webframe')
    .controller('ReacCtrl', ['$scope', '$window', '$lowdb',
        function ($scope, $window, $lowdb) {
            $scope.testReac = function () {
                alert('メニュー「reac」');
            };
        }
    ]);

