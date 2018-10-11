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

            return {
                '部材荷重': {
                    en: 'type 1',
                    data: 'type1',
                    items: {
                        '部材1': {
                            'column': {
                                data: 'm1',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        '部材2': {
                            'column': {
                                data: 'm2',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        '方向': {
                            'column': {
                                data: 'direction',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        'マーク': {
                            'column': {
                                data: 'mark',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
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
                        'P1': {
                            'column': {
                                data: 'P1',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        'P2': {
                            'column': {
                                data: 'P2',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                    },
                },
                '節点荷重': {
                    en: 'type 1',
                    data: 'type1',
                    items: {
                        '節点': {
                            'column': {
                                data: 'n',
                                type: 'numeric',
                                'format': '0.0000'
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