'use strict';

/**
 * @ngdoc service
 * @name webframe.loadConfig
 * @description
 * # loadConfig
 * Factory in the webframe
 */
angular.module('webframe')
    .factory('loadConfig', ['HtHelper', 'htSpeedInput',
        function (HtHelper, htSpeedInput) { 
            const name = ['no', 'm1', 'm2', 'direction', 'mark', 'L1', 'L2', 'P1', 'P2', 'n', 'tx', 'ty', 'tz', 'rx', 'ry', 'rz'];

            function column(str){
                var dic = {'items':{'':{'column':{'data':str, 'type':'numeric', 'format':'0.0000'} }} };
                return dic;
            }

            return {
                '部材No.': {
                    'items': {
                        '': column('no')
                    }
                },
                '部材荷重': {
                    items: {
                        '部材1': column('m1'),
                        '部材2': column('m2'),
                        '方向': column('direction'),
                        'マーク': column('mark'),
                        'L1': column('L1'),
                        'L2': column('L2'),
                        'P1': column('P1'),
                        'P2': column('P2')
                    },
                },
                '節点荷重': {
                    items: {
                        '節点': {
                            'items':{
                                '':{
                                    'column': {
                                        data: 'n',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                }
                            }
                        },
                        '荷重': {
                            items: {
                                'X': {
                                    'column': {
                                        data: 'tx',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                },
                                'Y': {
                                    'column': {
                                        data: 'ty',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                },
                                'Z': {
                                    'column': {
                                        data: 'tz',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                }
                            }
                        },
                        'モーメント': {
                            items: {
                                'X': {
                                    'column': {
                                        data: 'rx',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                },
                                'Y': {
                                    'column': {
                                        data: 'ry',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                },
                                'Z': {
                                    'column': {
                                        data: 'rz',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                }
                            }
                        },
                    }
                }
            };
        }
]);