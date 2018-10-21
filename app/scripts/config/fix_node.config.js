'use strict';

/**
 * @ngdoc service
 * @name webframe.fixNodeConfig
 * @description
 * # fixNodeConfig
 * Factory in the webframe
 */

angular.module('webframe')
    .factory('fixNodeConfig', ['HtHelper', 'htSpeedInput',
        function (HtHelper, htSpeedInput) { 

            return {
                '節点':{
                    en:'node',
                    data:'node',
                    items:{
                        '':{
                            items:{
                                '':{
                                    'column':{
                                        data:'n',
                                        type:'numeric',
                                        format:'0.0000'
                                    }
                                }
                            }
                        }
                    }
                },
                'タイプ1': {
                    en: 'type 1',
                    data: 'type1',
                    items: {
                        '変位拘束': {
                            items: {
                                'X': {
                                    'column': {
                                        data: 'tx1',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                },
                                'Y': {
                                    'column': {
                                        data: 'ty1',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                },
                                'Z': {
                                    'column': {
                                        data: 'tz1',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                }
                            }
                        },
                        'モーメント拘束': {
                            items: {
                                'X': {
                                    'column': {
                                        data: 'rx1',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                },
                                'Y': {
                                    'column': {
                                        data: 'ry1',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                },
                                'Z': {
                                    'column': {
                                        data: 'rz1',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                }
                            }
                        },
                    }
                },
                'タイプ2': {
                    en: 'type 2',
                    data: 'type2',
                    items: {
                        '変位拘束': {
                            items: {
                                'X': {
                                    'column': {
                                        data: 'tx2',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                },
                                'Y': {
                                    'column': {
                                        data: 'ty2',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                },
                                'Z': {
                                    'column': {
                                        data: 'tz2',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                }
                            }
                        },
                        'モーメント拘束': {
                            items: {
                                'X': {
                                    'column': {
                                        data: 'rx2',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                },
                                'Y': {
                                    'column': {
                                        data: 'ry2',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                },
                                'Z': {
                                    'column': {
                                        data: 'rz2',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                }
                            }
                        },
                    }
                },
                'タイプ3': {
                    en: 'type 3',
                    data: 'type3',
                    items: {
                        '変位拘束': {
                            items: {
                                'X': {
                                    'column': {
                                        data: 'tx3',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                },
                                'Y': {
                                    'column': {
                                        data: 'ty3',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                },
                                'Z': {
                                    'column': {
                                        data: 'tz3',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                }
                            }
                        },
                        'モーメント拘束': {
                            items: {
                                'X': {
                                    'column': {
                                        data: 'rx3',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                },
                                'Y': {
                                    'column': {
                                        data: 'ry3',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                },
                                'Z': {
                                    'column': {
                                        data: 'rz3',
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