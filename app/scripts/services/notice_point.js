'use strict'

/**
 * @ngdoc service
 * @name webframe.notice_point
 * @description
 * # notice_point
 * Factory in the webframe.
 */
angular.module('webframe')
    .factory('Notice_point', ['$lowdb', '$injector', '$filter', 'LowResource', 'noticePointDefaults', 'noticePointConfig', 'HtHelper',
       function ($lowdb, $injector, $filter, LowResource, noticePointDefaults, noticePointConfig, HtHelper) {

            let Notice_point = LowResource({
                'table': 'notice_points',
                'defaultEntries': noticePointDefaults
            });

            Notice_point.afterChange = function (changes, source) {
                let hot = this;
                changes.forEach(function (change) {
                    let [row, prop, oldVal, newVal] = change;
                    let notice_point = hot.getSourceDataAtRow(row);
                });
                SendDataToUnity('notice_points');
            };

            _.mixin(Notice_point, HtHelper);
            Notice_point.htInit(noticePointConfig);
            
            return Notice_point;
       } 
    ]);