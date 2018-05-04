'use strict';

/**
 * @ngdoc service
 * @name webframe.Member
 * @description
 * # Member
 * Factory in the webframe.
 */
angular.module('webframe')
  .factory('Member', ['$injector', '$filter', 'LowResource', 'memberDefaults', 'memberConfig', 'HtHelper',
    function ($injector, $filter, LowResource, memberDefaults, memberConfig, HtHelper) {

      let primaryKey = 'm_no';
      let g_no_column = 2;

      let Member = LowResource({
        'table': 'members',
        'primaryKey': primaryKey,
        'foreignKeys': {
          'children': {
            DesignPoint: primaryKey,
          },
        },
        'defaultEntries': memberDefaults,
      });

      Member.afterChange = function (changes, source) {
        let hot = this;
        changes.forEach(function (change) {
          let [row, prop, oldVal, newVal] = change;
          let member = hot.getSourceDataAtRow(row);
        });
      };

      _.mixin(Member, HtHelper);
      Member.htInit(memberConfig);

      return Member;
    }
  ]);
