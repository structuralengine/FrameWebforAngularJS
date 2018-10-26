'use strict'

/**
 * @ngdoc service
 * @name webframe.joint
 * @description
 * # joint
 * Factory in the webframe.
 */
angular.module('webframe')
    .factory('Joint', ['$lowdb', '$injector', '$filter', 'LowResource', 'jointDefaults', 'jointConfig', 'HtHelper',
       function ($lowdb, $injector, $filter, LowResource, jointDefaults, jointConfig, HtHelper) {

            let Joint = LowResource({
                'table': 'joints',
                'defaultEntries': jointDefaults
            });

            Joint.afterChange = function (changes, source) {
                let hot = this;
                changes.forEach(function (change) {
                    let [row, prop, oldVal, newVal] = change;
                    let joint = hot.getSourceDataAtRow(row);
                });
                let jsonObj = $lowdb.get('joints').value();
                SendDataToUnity('joints', jsonObj)
            };

            _.mixin(Joint, HtHelper);
            Joint.htInit(jointConfig);
            
            return Joint;
       } 
    ]);