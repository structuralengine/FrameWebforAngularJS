'use strict';

/**
 * @ngdoc directive
 * @name webframe.directive:ngShowAuth
 * @description
 * # ngShowAuth
 */
angular.module('webframe')
  .directive('ngShowAuth', ['Auth', '$timeout', function (Auth, $timeout) {
    'use strict';

    return {
      restrict: 'A',
      link: function(scope, el) {
        el.addClass('ng-cloak'); // hide until we process it

        function update() {
          // sometimes if ngCloak exists on same element, they argue, so make sure that
          // this one always runs last for reliability
          $timeout(function () {
            el.toggleClass('ng-cloak', !Auth.$getAuth());
          }, 0);
        }

        Auth.$onAuthStateChanged(update);
        update();
      }
    };
  }]);
