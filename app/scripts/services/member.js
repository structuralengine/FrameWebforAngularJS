'use strict';

/**
 * @ngdoc service
 * @name webframe.Member
 * @description
 * # Member
 * Factory in the webframe.
 */
angular.module('webframe')
  .factory('Member', ['$lowdb', '$injector', '$filter', 'LowResource', 'memberDefaults', 'memberConfig', 'HtHelper',
    function ($lowdb, $injector, $filter, LowResource, memberDefaults, memberConfig, HtHelper) {

      let Member = LowResource({
        'table': 'members',
        'defaultEntries': memberDefaults
      });

      Member.afterChange = function (changes, source) {
        let hot = this;
        changes.forEach(function (change) {
          let [row, prop, oldVal, newVal] = change;
          let member = hot.getSourceDataAtRow(row);
        });
        let jsonObj = $lowdb.get('members').value();
        SendDataToUnity('members', jsonObj)
      };

      _.mixin(Member, HtHelper);
      Member.htInit(memberConfig);

      return Member;
    }
  ]);
