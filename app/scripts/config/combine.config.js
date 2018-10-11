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

            return {
                'DEFINEケース': {
                    en: 'Define Case',
                    data: 'type1',
                    items: {
                        '1': {
                            'column': {
                                data: '1',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        '2': {
                            'column': {
                                data: '2',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        '3': {
                            'column': {
                                data: '3',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        '4': {
                            'column': {
                                data: '4',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        '5': {
                            'column': {
                                data: '5',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        '6': {
                            'column': {
                                data: '6',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                       
                    }
                },
            };
        }
]);