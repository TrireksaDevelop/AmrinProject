angular.module("app", ['ngRoute', 'ui.router',
    'admin.routes',
    'admin.services',
    'admin.controllers',
    'home.routes',
    'home.services',
    'home.controllers',
    'petugas.routes',
    'petugas.services',
    'petugas.controllers'

])
    .directive('fileModel', ['$parse', function ($parse) {

        return {

            restrict: 'A',

            link: function (scope, element, attrs) {

                var model = $parse(attrs.fileModel);

                var modelSetter = model.assign;



                element.bind('change', function () {

                    scope.$apply(function () {

                        modelSetter(scope, element[0].files[0]);

                        var canvas = element.parent().find('img');

                        var reader = new FileReader();

                        reader.onload = function (e) {

                            canvas.attr('src', e.target.result)



                        };

                        reader.readAsDataURL(element[0].files[0]);

                    });

                });

            }

        };

    }])



    .directive('fileCanvas', ['$parse', function ($parse) {

        return {

            restrict: 'A',

            link: function (scope, element, attrs) {

                var model = $parse(attrs.fileCanvas);

                element.bind('click', function () {

                    var input = element[0].querySelector("input");

                    input.click();

                });





            }

        };

    }])



    .directive("messageLink", function () {

        return {

            restrict: "A",

            link: function (scope, elem, attrs) {

                $(elem).click(function () {

                    $().JqueryFunction();

                });

            }

        }

    });
    ;