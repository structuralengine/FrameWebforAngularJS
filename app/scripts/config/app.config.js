'use strict';

/**
 * @ngdoc service
 * @name webframe.appConfig
 * @description
 * # appConfig
 * Constant in the webframe.
 */
angular.module('webframe')
    .constant('appConfig', {
        db: {
            'source': 'webframe.2',
            'defaults': {
                nodes: {},
                fix_nodes: [],
                members: [],
                elements: [],
                joints: [],
                notice_points: [],
                fix_members: [],
                loads: [],
                combines: []
            },
        },
        CalculationPrint: {
            calculatePage: 'Web_Api.py',
            server: {
                url: 'http://structuralengine.com/FrameWeb/api'
            },
        },
    });