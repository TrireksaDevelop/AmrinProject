angular.module("home.controllers", [])
    .controller('LoginController', LoginController)
    .controller('RegisterController', RegisterController)
    ;

function LoginController($scope, AuthServices) {
    sessionStorage.clear();
    $scope.Login = function(model)
    {
        AuthServices.login(model);
    }
}

function RegisterController(UserServices) {
    var res = UserServices.getToken();
    var res1 = UserServices.getUser();
}

;