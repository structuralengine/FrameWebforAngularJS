'use strict';

/**
 * @ngdoc service
 * @name webframe.fixMemberConfig
 * @description
 * # fixMemberConfig
 * Factory in the webframe
 */
angular.module('webframe')
    .factory('fixMemberConfig', ['HtHelper', 'htSpeedInput',
        function (HtHelper, htSpeedInput) { 

            function json(str, n){
                var item = {};
                item['column'] = 
                    {'data':str + n, 'type':'numeric', 'format':'0.0000'};
                return item;
            }

            var item = {};
            item['部材No'] = {
                'items':{
                    '':{
                        'items':{
                            '':{
                                'column':{
                                    'data':'no',
                                    'type':'numeric',
                                    'format':'0'
                                }
                            }
                        }
                    }
                }
            };
            for(var i = 1; i <= 3; i++){
                item['タイプ' + i] = {
                    'data':'type' + i,
                    'items':{
                        '変位拘束':{
                            'items':{'X':json('x', i), 'Y':json('y', i), 'Z':json('z', i)}
                        },
                        '回転拘束':{
                            'items':{'R':json('r', i)}
                        }
                    }
                }
            }

            return item;
        }
]);