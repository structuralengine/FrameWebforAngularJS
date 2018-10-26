'use strict'

/**
 * @ngdoc service
 * @name webframe.pickup
 * @description
 * # pickup
 * Factory in the webframe.
 */

angular.module('webframe')
    .factory('Pickup', ['$lowdb', '$injector', '$filter', 'LowResource', 'pickupDefaults', 'pickupConfig', 'HtHelper',
       function ($lowdb, $injector, $filter, LowResource, pickupDefaults, pickupConfig, HtHelper) {

            let Pickup = LowResource({
                'table': 'pickups',
                'defaultEntries': pickupDefaults
            });

            Pickup.afterChange = function (changes, source) {
                let hot = this;
                changes.forEach(function (change) {
                    let [row, prop, oldVal, newVal] = change;
                    let pickup = hot.getSourceDataAtRow(row);
                });
                /* // this table is not send unity
                let jsonObj = $lowdb.get('pickups').value();
                SendDataToUnity('pickups', jsonObj)
                */
            };

            _.mixin(Pickup, HtHelper);
            Pickup.htInit(pickupConfig);
            
            return Pickup;
       } 
    ]);