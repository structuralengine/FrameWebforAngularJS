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
                                        data: 'X',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                },
                                'Y': {
                                    'column': {
                                        data: 'Y',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                },
                                'Z': {
                                    'column': {
                                        data: 'Z',
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
                                        data: 'R',
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
                                        data: 'X',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                },
                                'Y': {
                                    'column': {
                                        data: 'Y',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                },
                                'Z': {
                                    'column': {
                                        data: 'Z',
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
                                        data: 'R',
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
                                        data: 'X',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                },
                                'Y': {
                                    'column': {
                                        data: 'Y',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                },
                                'Z': {
                                    'column': {
                                        data: 'Z',
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
                                        data: 'R',
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