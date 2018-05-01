'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:BasicInformationCtrl
 * @description
 * # BasicInformationCtrl
 * Controller of the webframe
 */
angular.module('webframe')
  .controller('BasicInformationCtrl', ['$scope', function ($scope) {
    $scope.ngPopupConfig = {
      modelName: 'myNgPopup',
      width: $scope.inputWidth,
      height: $scope.inputHeight,
      templateUrl: '../views/members.html',
      resizable: true,
      draggable: true,
      position: { top: 500, left: 500 },
      onOpen: function () {
        /*Some Logic...*/
      }
    }
  }]);