angular.module('reservApp', [])
      .controller('LoginController', LoginCtrl);

    function LoginCtrl($timeout) {
      var vm = this;

      vm.credentials = { username: '', password: '' };
      vm.rememberMe  = false;
      vm.loading     = false;
      vm.submitted   = false;
      vm.serverError = null;

      vm.login = login;

      // ── Login handler ─────────────────────────────────────────
      function login() {
        vm.submitted   = true;
        vm.serverError = null;

        // Client-side guard
        if (!vm.credentials.username || !vm.credentials.password) return;

        vm.loading = true;

        // Simulated API call — replace with $http.post('/api/auth/login', vm.credentials)
        $timeout(function () {
          vm.loading = false;

          // Demo: treat "admin / password" as valid
          if (vm.credentials.username === 'admin' && vm.credentials.password === 'password') {
            // TODO: store token, redirect → e.g. $window.location.href = '/dashboard';
              window.location.href = "../Dashboard/dashboard.html";
          } else {
            vm.serverError = 'Invalid username or password. Please try again.';
          }
        }, 1800);
      }
    }