'use strict'

/**
 * @ngdoc service
 * @name webframe.fix_member
 * @description
 * # fix_member
 * Factory in the webframe.
 */
angular.module('webframe')
    .factory('Fix_member', ['$lowdb', '$injector', '$filter', 'LowResource', 'fixMemberDefaults', 'fixMemberConfig', 'HtHelper',
       function ($lowdb, $injector, $filter, LowResource, fixMemberDefaults, fixMemberConfig, HtHelper) {

            let Fix_member = LowResource({
                'table': 'fix_members',
                'defaultEntries': fixMemberDefaults
            });

            Fix_member.afterChange = function (changes, source) {
                let hot = this;
                changes.forEach(function (change) {
                    let [row, prop, oldVal, newVal] = change;
                    let fix_member = hot.getSourceDataAtRow(row);
                });
                let jsonObj = $lowdb.get('fix_members').value();
                SendDataToUnity('fix_members', jsonObj)
            };

            _.mixin(Fix_member, HtHelper);
            Fix_member.htInit(fixMemberConfig);
            
            return Fix_member;
       } 
    ]);