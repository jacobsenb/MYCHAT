var app = angular.module('MyChat', ['ngRoute', 'ngResource', 'ui.bootstrap']);
app.config(function ($routeProvider) {

    $routeProvider.when("/Session", {
        controller: "SessionController",
        templateUrl: "/Views/Session.html"
    });

    $routeProvider.otherwise({ redirectTo: "/Views/Index.html" })

});