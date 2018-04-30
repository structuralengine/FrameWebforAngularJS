'use strict';

/**
 * @ngdoc service
 * @name webframe.Member
 * @description
 * # Member
 * Factory in the webframe.
 */
angular.module('webframe')
  .factory('Member', ['LowResource', 'memberConfig',
    function ( LowResource, memberConfig) {

      let Member = LowResource({
        'table': 'members',
      });

      return Member;
    }
  ]);