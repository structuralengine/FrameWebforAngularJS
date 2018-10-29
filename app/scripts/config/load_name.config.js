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
            return {
                '割増': {
                    en: 'p1',
                    'column': {
                        'data': 'coef',
                        'type': 'numeric',
                        'format': '0.0000'
                    },
                    items: {
                        '係数': {}
                    }
                },
                '荷重': {
                    en: 'p2',
                    'column': {
                        'data': 'no',
                        'type': 'numeric',
                        'format': '0'
                    },
                    items: {
                        'No': {}
                    }
                },
                '記号': {
                    en: 'p3',
                    'column': {
                        'data': 'sign'
                    },
                    items: {
                        '': {}
                    }
                },
                '名称': {
                    en: 'p4',
                    'column': {
                        'data': 'name',
                        'width':'300px'
                    },
                    items: {
                        '': {}
                    }
                },
                '構造系条件': {
                    en: 'p5',
                    items: {
                        '支点': {
                            'column': {
                                data: 'fn',
                                type: 'numeric',
                                'format': '0'
                            }
                        },
                        'バネ': {
                            'column': {
                                data: 'fm',
                                type: 'numeric',
                                'format': '0'
                            }
                        },    
                        '断面': {
                            'column': {
                                data: 'fsec',
                                type: 'numeric',
                                'format': '0'
                            }
                        },   
                        '結合': {
                            'column': {
                                data: 'fsec',
                                type: 'numeric',
                                'format': '0'
                            }
                        }  
                    }
                }
            };

        }
]);