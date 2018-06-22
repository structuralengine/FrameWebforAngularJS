'use strict';

/**
 * @ngdoc function
 * @name webframe.controller:ElementsCtrl
 * @description
 * # ElementsCtrl
 * Controller of the webframe
 */
angular.module('webframe')
    .controller('ElementsCtrl', ['$scope', '$filter', '$q', 'Element', 'elementDefaults',
        function ($scope, $filter, $q, Element, elementDefaults) {
            let ctrl = this;

            $scope.$on('reload', function (e) {
                init();
            });

            function init() {
                let elements = Element.query();

                if (elements.length == 0) {
                    Element.createDefaultEntries();
                    elements = Element.query();
                }

                ctrl.elements = elements;
                ctrl.settings = Element.settings;
            }

            init();
        }
    ]);