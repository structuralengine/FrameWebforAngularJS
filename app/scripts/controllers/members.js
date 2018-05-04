'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:MembersCtrl
 * @description
 * # MembersCtrl
 * Controller of the webframe
 */
angular.module('webframe')
  .controller('MembersCtrl', ['$scope', '$filter', '$q', 'Member', 'memberDefaults', 'sectionShapeDefaults', 'conditionDefaults',
    function ($scope, $filter, $q, Member, memberDefaults, sectionShapeDefaults, conditionDefaults) {
      let ctrl = this;

      ctrl.shapes = sectionShapeDefaults;
      ctrl.conditions = conditionDefaults;

      $scope.$on('reload', function (e) {
        init();
      });

      function init() {
        let members = Member.query();

        if (members.length == 0) {
          Member.createDefaultEntries();
          members = Member.query();
        }

        ctrl.members = $filter('orderBy')(members, 'm_no');
        ctrl.settings = Member.settings;
      }

      init();
    }
  ]);
