'use strict';

/**
 * @ngdoc service
 * @name webframe.combineConfig
 * @description
 * # combineConfig
 * Factory in the webframe
 */
angular.module('webframe')
    .factory('combineConfig', ['HtHelper', 'htSpeedInput',
        function (HtHelper, htSpeedInput) {

            const NUM = 21;

            function item(n) {
                var json = {};
                for (var i = 0; i < n; i++) {
                    json[String(i + 1)] = { 'column': { 'data': String(i + 1), 'type': 'numeric', 'format': '0.00' } };
                }
                return json;
            }

            return { 'ケース': { 'data': 'case', 'items': item(NUM) } };

        }
    ]);