
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



function AuthServices($http, $state, $rootScope, MessageServices, $q, UserServices) {
    //var def = $q.defer();

    var service = {
        login: login, register: register, getProfile: getProfile
    };


    function login(user) {
        var data = "grant_type=password&username=" + user.Email + "&password=" + user.Password;
        NProgress.start();
        data = {};
        data.email = user.Email;
        data.username = user.Email;
        data.password = user.Password;
        $http({
            method: 'POST',
            url: '/account/login',
            data: data
        }).then(function successCallback(response) {
            var result = response.data;
            sessionStorage.setItem("AuthToken", result.token);
            sessionStorage.setItem("UserRoles", result.roles);
            sessionStorage.setItem("TokenType", "bearer");
            sessionStorage.setItem("UserName", user.Email);
            if (result.roles[0] != null || result.roles[1] != undefined) {
                var state = result.roles[0];
                NProgress.done();
                $state.go(state);
            } else {
                MessageServices.error("Anda Tidak Memiliki Akses");
                NProgress.done();
                $state.go("/");
            }
         
        }, function errorCallback(response) {
            MessageServices.error(response.data);
            NProgress.done();
        });
    }


    function register(model) {
        var def = $q.defer();
        $http({
            method: 'Post',
            url: '/account/register',
            data: model
        }).then(function successCallback(response) {
            MessageServices.success("Register Success, Silahkan Login");
            def.resolve(response);
        }, function errorCallback(response) {
            MessageServices.error(response.data);
            def.reject();
            });
        return def.promise;
    }


    function getProfile() {
        var def = $q.defer();
        $http({
            method: 'Get',
            url: '/account/PetugasProfile',
            headers: UserServices.getHeaders()
        }).then(function successCallback(response) {
            def.resolve(response.data);
        }, function errorCallback(response) {
            MessageServices.error(response.data);
            def.reject();
            });
        return def.promise;
    }



    return service;
}


;