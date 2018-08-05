

angular.module("home.services", [])
    .factory('AuthServices', AuthServices)
    .factory('MessageServices', MessageServices)
    ;

function MessageServices() {
    function error(message) {
        new PNotify({
            title: 'ERROR',
            text: message,
            type: 'error',
            styling: 'bootstrap3'
        });
    }

    function info(message) {
        new PNotify({
            title: 'INFO',
            text: message,
            type: 'info',
            styling: 'bootstrap3'
        });
    }

    function warning(message) {
        new PNotify({
            title: 'WARNING',
            text: message,
            type: 'warning',
            styling: 'bootstrap3'
        });
    }

    function success(message) {
        new PNotify({
            title: 'SUCCESS',
            text: message,
            type: 'success',
            styling: 'bootstrap3'
        });
    }

    return {
        error: error, success: success, warning: warning, info: info
    }

}



function AuthServices($http, $state, $rootScope, MessageServices, $q) {
    var def = $q.defer();



    function login(user) {
      //  var data = "grant_type=password&username=" + user.Email + "&password=" + user.Password;

        var data = {};
        data.email = user.Email;
        data.username = user.Email;
        data.password = user.Password;
        $http({
            method: 'POST',
          
            url: '/account/login',
            data: data
        }).then(function successCallback(response) {
            sessionStorage.setItem("AuthToken", response.data);
            sessionStorage.setItem("TokenType", "bearer");
            sessionStorage.setItem("UserName", user.Email);
            $state.go('admin');
        }, function errorCallback(response) {
            MessageServices.error(response.data);
        });
    }


    function register(registerModel) {

    }


    return {
        login: login, register: register
    }
}


;