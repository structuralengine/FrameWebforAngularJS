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
                '荷重': {
                    'items': {
                        '番号': {
                            'items': {
                                '': {
                                    'column': {
                                        'data': 'no',
                                        'type': 'numeric',
                                        'format': '0'
                                    }
                                }
                            }
                        }
                    }
                },
                '部材荷重': {
                    items: {
                        '部材1': {
                            'items': {
                                '': {
                                    'column': {
                                        'data': 'm1',
                                        'type': 'numeric',
                                        'format': '0'
                                    }
                                }
                            }
                        },
                        '部材2': {
                            'items': {
                                '': {
                                    'column': {
                                        'data': 'm2',
                                        'type': 'numeric',
                                        'format': '0'
                                    }
                                }
                            }
                        },
                        '方向': {
                            'items': {
                                '': {
                                    'column': {
                                        'data': 'direction'
                                    }
                                }
                            }
                        },
                        'マーク': {
                            'items': {
                                '': {
                                    'column': {
                                        'data': 'mark'
                                    }
                                }
                            }
                        },
                        'L1': {
                            'items': {
                                '': {
                                    'column': {
                                        'data': 'L1',
                                        'type': 'numeric',
                                        'format': '0.000'
                                    }
                                }
                            }
                        },
                        'L2': {
                            'items': {
                                '': {
                                    'column': {
                                        'data': 'L2',
                                        'type': 'numeric',
                                        'format': '0.000'
                                    }
                                }
                            }
                        },
                        'P1': {
                            'items': {
                                '': {
                                    'column': {
                                        'data': 'P1',
                                        'type': 'numeric',
                                        'format': '0.00'
                                    }
                                }
                            }
                        },
                        'P2': {
                            'items': {
                                '': {
                                    'column': {
                                        'data': 'P2',
                                        'type': 'numeric',
                                        'format': '0.00'
                                    }
                                }
                            }
                        }
                    }
                },
                '節点荷重': {
                    items: {
                        '節点': {
                            'items':{
                                '':{
                                    'column': {
                                        data: 'n',
                                        type: 'numeric',
                                        'format': '0'
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
                                        'format': '0.00'
                                    }
                                },
                                'Y': {
                                    'column': {
                                        data: 'ty',
                                        type: 'numeric',
                                        'format': '0.00'
                                    }
                                },
                                'Z': {
                                    'column': {
                                        data: 'tz',
                                        type: 'numeric',
                                        'format': '0.00'
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
                                        'format': '0.00'
                                    }
                                },
                                'Y': {
                                    'column': {
                                        data: 'ry',
                                        type: 'numeric',
                                        'format': '0.00'
                                    }
                                },
                                'Z': {
                                    'column': {
                                        data: 'rz',
                                        type: 'numeric',
                                        'format': '0.00'
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }
]);