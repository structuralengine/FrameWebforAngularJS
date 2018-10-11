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
                'タイプ1': {
                    en: 'type 1',
                    data: 'type1',
                    items: {
                        '変位拘束': {
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
                        'モーメント拘束': {
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
                        'モーメント拘束': {
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
                        'モーメント拘束': {
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
                    }
                }
            };
        }
]);