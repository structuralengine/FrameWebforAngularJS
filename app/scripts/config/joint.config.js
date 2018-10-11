'use strict';

/**
 * @ngdoc service
 * @name webframe.jointConfig
 * @description
 * # jointConfig
 * Factory in the webframe
 */
angular.module('webframe')
    .factory('jointConfig', ['HtHelper', 'htSpeedInput',
        function (HtHelper, htSpeedInput) { 

            return {
                'タイプ1': {
                    en: 'type 1',
                    data: 'type1',
                    items: {
                        'x1': {
                            'column': {
                                data: 'x11',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        'y1': {
                            'column': {
                                data: 'y11',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        'z1': {
                            'column': {
                                data: 'z11',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        'x2': {
                            'column': {
                                data: 'x21',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        'y2': {
                            'column': {
                                data: 'y21',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        'z2': {
                            'column': {
                                data: 'z21',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        }
                    }
                },
                'タイプ2': {
                    en: 'type 2',
                    data: 'type2',
                    items: {
                        'x1': {
                            'column': {
                                data: 'x12',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        'y1': {
                            'column': {
                                data: 'y12',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        'z1': {
                            'column': {
                                data: 'z12',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        'x2': {
                            'column': {
                                data: 'x22',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        'y2': {
                            'column': {
                                data: 'y22',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        'z2': {
                            'column': {
                                data: 'z22',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        }
                    }
                },
                'タイプ3': {
                    en: 'type 3',
                    data: 'type3',
                    items: {
                        'x1': {
                            'column': {
                                data: 'x13',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        'y1': {
                            'column': {
                                data: 'y13',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        'z1': {
                            'column': {
                                data: 'z13',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        'x2': {
                            'column': {
                                data: 'x23',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        'y2': {
                            'column': {
                                data: 'y23',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        },
                        'z2': {
                            'column': {
                                data: 'z23',
                                type: 'numeric',
                                'format': '0.0000'
                            }
                        }
                    }
                }
            };
        }
]);