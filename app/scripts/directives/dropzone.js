'use strict';

/**
 * @ngdoc directive
 * @name webframe.directive:dropzone
 * @description
 * # dropzone
 */
angular.module('webframe')
  .directive('dropzone', function () {
    return {
      restrict: 'A',
      link: function(scope, element, attrs) {

        let receive = scope.$eval(attrs.receive) || angular.noop;

        let dz = new Dropzone(element[0], {
          url: '#',
          autoProcessQueue: false,
          clickable: [attrs.clickable || 'a[dropzone]'],
          acceptedFiles: attrs.extensions || '.json',
          addedfile: function(file) {
            receive(file);
          },
        });
      }
    };
  });
