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

            return {
                'タイプ1': {
                    en: 'type 1',
                    data: 'type1',
                    items: {
                        '変位拘束': {
                            items: {
                                'X': {
                                    'column': {
                                        data: 'x1',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                },
                                'Y': {
                                    'column': {
                                        data: 'y1',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                },
                                'Z': {
                                    'column': {
                                        data: 'z1',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                }
                            }
                        },
                        '回転拘束': {
                            items: {
                                'R': {
                                    'column': {
                                        data: 'r1',
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
                                        data: 'x2',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                },
                                'Y': {
                                    'column': {
                                        data: 'y2',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                },
                                'Z': {
                                    'column': {
                                        data: 'z2',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                }
                            }
                        },
                        '回転拘束': {
                            items: {
                                'R': {
                                    'column': {
                                        data: 'r2',
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
                                        data: 'x3',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                },
                                'Y': {
                                    'column': {
                                        data: 'y3',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                },
                                'Z': {
                                    'column': {
                                        data: 'z3',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                }
                            }
                        },
                        '回転拘束': {
                            items: {
                                'R': {
                                    'column': {
                                        data: 'r3',
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