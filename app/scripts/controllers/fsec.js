'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:FsecCtrl
 * @description
 * # FsecCtrl
 * Controller of the webframe
 */
angular.module('webframe')
    .controller('FsecCtrl', ['$scope', '$window', '$lowdb',
        function ($scope, $window, $lowdb) {
            $scope.testFsec = function () {
                alert('メニュー「fsec」');
            };
        }
    ]);

