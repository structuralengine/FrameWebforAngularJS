'use strict';

/**
 * @ngdoc service
 * @name webframe.loadNameConfig
 * @description
 * # loadNameConfig
 * Factory in the webframe
 */
angular.module('webframe')
    .factory('loadNameConfig', ['HtHelper', 'htSpeedInput',
        function (HtHelper, htSpeedInput) { 

            function column(str){
                return { 'column':{'data':str, 'type':'numeric', 'format':'0.0000'} };
            }
            function space(dic){
                return {'items':{'':dic}};
            }

            return {
                '割増係数':space(column('coef')),
                '荷重No.':space(column('no')),
                '記号':space(column('sign')),
                '名称':space(column('name')),
                '構造系条件':{ 'items':{ '支点':column('fn'), 'バネ':column('fm'), '断面':column('fsec'), '結合':column('joint') } }
            };

        }
]);