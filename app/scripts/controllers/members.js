'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:MembersCtrl
 * @description
 * # MembersCtrl
 * Controller of the webframe
 */
angular.module('webframe')
  .controller('MembersCtrl', ['$scope', '$filter', '$q', 'Member', 'memberDefaults',
    function ($scope, $filter, $q, Member, memberDefaults) {
      let ctrl = this;

      $scope.$on('reload', function (e) {
        init();
      });

      function init() {
        let members = Member.query();

        if (members.length == 0) {
          Member.createDefaultEntries();
          members = Member.query();
        }

        ctrl.members = members;
        ctrl.settings = Member.settings;
      }

      init();
    }
  ]);
