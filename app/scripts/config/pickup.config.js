'use strict';

/**
 * @ngdoc service
 * @name webframe.pickupConfig
 * @description
 * # pickupConfig
 * Factory in the webframe
 */

angular.module('webframe')
    .factory('pickupConfig', ['HtHelper', 'htSpeedInput',
        function (HtHelper, htSpeedInput) { 

            const NUM = 10;

            function item(n){
                var json = {};
                for (var i = 0; i < n; i++){
                    json[String(i + 1)] = { 'column':{'data':String(i + 1), 'type':'numeric', 'format':'0'} };
                }
                return json;
            }

            return { 'ケース':{ 'data':'case', 'items':item(NUM) } };

        }
]);