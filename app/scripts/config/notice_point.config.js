'use strict';

/**
 * @ngdoc service
 * @name webframe.noticePointConfig
 * @description
 * # noticePointConfig
 * Factory in the webframe
 */
angular.module('webframe')
    .factory('noticePointConfig', ['HtHelper', 'htSpeedInput',
        function (HtHelper, htSpeedInput) { 

            return {
                'i端からの距離': {
                    en: 'Ｄistance from the Edge i',
                    data: 'type1',
                items: {
                        'L1': {
                            'column': {
                                data: 'L1',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        'L2': {
                            'column': {
                                data: 'L2',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        'L3': {
                            'column': {
                                data: 'L3',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        'L4': {
                            'column': {
                                data: 'L4',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        'L5': {
                            'column': {
                                data: 'L5',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        'L6': {
                            'column': {
                                data: 'L6',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        'L7': {
                            'column': {
                                data: 'L7',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        'L8': {
                            'column': {
                                data: 'L8',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        'L9': {
                            'column': {
                                data: 'L9',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        'L10': {
                            'column': {
                                data: 'L10',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        'L11': {
                            'column': {
                                data: 'L11',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        'L12': {
                            'column': {
                                data: 'L12',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        }
                    }
                },
                
            };
        }
]);