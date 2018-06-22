'use strict';

/**
 * @ngdoc service
 * @name webframe.elementConfig
 * @description
 * # elementConfig
 * Factory in the webframe.
 */
angular.module('webframe')
    .factory('elementConfig', ['HtHelper', 'htSpeedInput',
        function (HtHelper, htSpeedInput) {

            return {
                '弾性係数': {
                    en: 'Elastic',
                    items: {
                        'E': {
                            items: {
                                '(kN/mm2)': {
                                    'column': {
                                        data: 'E',
                                        type: 'numeric',
                                        'format': '0.00'
                                    }
                                }
                            }
                        }
                    }
                },
                'せん断弾性係数': {
                    en: 'Shearing',
                    items: {
                        'G': {
                            items: {
                                '(kN/mm2)': {
                                    'column': {
                                        data: 'G',
                                        type: 'numeric',
                                        'format': '0.00'
                                    }
                                }
                            }
                        }                        
                    }
                },
                '膨張係数': {
                    en: 'Thermal',
                    items: {
                        '': {
                            items: {
                                '': {
                                    'column': {
                                    data: 'Xp',
                                    type: 'numeric',
                                    'format': '0.00000'
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
                        '断面積': {
                            en: 'Area',
                            items: {
                                'A(m2)': {
                                    'column': {
                                        data: 'A1',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                }
                            }
                        },
                        'ねじり定数': {
                            en: 'Torsional',
                            items: {
                                'J(m4)': {
                                    'column': {
                                        data: 'J1',
                                        type: 'numeric',
                                        'format': '0.000000'
                                    }
                                }
                            }
                        },
                        '断面二次モーメント': {
                            en: 'inertia',
                            items: {
                                'Iy(m4)': {
                                    'column': {
                                        data: 'Iy1',
                                        type: 'numeric',
                                        'format': '0.000000'
                                    }
                                },
                                'Iz(m4)': {
                                    'column': {
                                        data: 'Iz1',
                                        type: 'numeric',
                                        'format': '0.000000'
                                    }
                                }
                            }
                        },
                    },
                },
                'タイプ2': {
                    en: 'type 2',
                    data: 'type2',
                    items: {
                        '断面積': {
                            en: 'Area',
                            items: {
                                'A(m2)': {
                                    'column': {
                                        data: 'A2',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                }
                            }
                        },
                        'ねじり定数': {
                            en: 'Torsional',
                            items: {
                                'J(m4)': {
                                    'column': {
                                        data: 'J2',
                                        type: 'numeric',
                                        'format': '0.000000'
                                    }
                                }
                            }
                        },
                        '断面二次モーメント': {
                            en: 'inertia',
                            items: {
                                'Iy(m4)': {
                                    'column': {
                                        data: 'Iy2',
                                        type: 'numeric',
                                        'format': '0.000000'
                                    }
                                },
                                'Iz(m4)': {
                                    'column': {
                                        data: 'Iz2',
                                        type: 'numeric',
                                        'format': '0.000000'
                                    }
                                }
                            }
                        },
                    },
                },
                'タイプ3': {
                    en: 'type 3',
                    data: 'type3',
                    items: {
                        '断面積': {
                            en: 'Area',
                            items: {
                                'A(m2)': {
                                    'column': {
                                        data: 'A3',
                                        type: 'numeric',
                                        'format': '0.0000'
                                    }
                                }
                            }
                        },
                        'ねじり定数': {
                            en: 'Torsional',
                            items: {
                                'J(m4)': {
                                    'column': {
                                        data: 'J3',
                                        type: 'numeric',
                                        'format': '0.000000'
                                    }
                                }
                            }
                        },
                        '断面二次モーメント': {
                            en: 'inertia',
                            items: {
                                'Iy(m4)': {
                                    'column': {
                                        data: 'Iy3',
                                        type: 'numeric',
                                        'format': '0.000000'
                                    }
                                },
                                'Iz(m4)': {
                                    'column': {
                                        data: 'Iz3',
                                        type: 'numeric',
                                        'format': '0.000000'
                                    }
                                }
                            }
                        },
                    },
                },
            };
        }
    ]);
