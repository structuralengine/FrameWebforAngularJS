'use strict';

/**
 * @ngdoc service
 * @name webframe.defineConfig
 * @description
 * # defineConfig
 * Factory in the webframe
 */

angular.module('webframe')
    .factory('defineConfig', ['HtHelper', 'htSpeedInput',
        function (HtHelper, htSpeedInput) { 

            const NUM = 6;

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