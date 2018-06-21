'use strict';

/**
 * @ngdoc service
 * @name webframe.webframeRef
 * @description
 * # webframeRef
 * Factory in the webframe.
 */
angular.module('webframe')
  .factory('webframeRef', ['Ref',
    function (Ref) {
      return Ref.child('webframe');
    }
  ]);
