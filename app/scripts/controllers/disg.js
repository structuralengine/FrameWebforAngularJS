'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:DisgCtrl
 * @description
 * # DisgCtrl
 * Controller of the webframe
 */
angular.module('webframe')
    .controller('DisgCtrl', ['$scope', '$window', '$lowdb',
        function ($scope, $window, $lowdb) {
            $scope.testDisg = function () {
                alert('メニュー「disg」');
            };
        }
    ]);

