angular.module('bookingApp', [])
      .controller('BookingController', ['$scope','$http', function($scope,$http) {

        var vm = this;
        // ── Dropdown data ──
        $http({
              method: 'GET',
              url: 'https://localhost:7080/api/Person/getAll'
        }).then(function successCallback(response) {
                vm.Persons=response.data;
        }, function errorCallback(error) {
          console.log(error)
        });
        //$scope.personOptions = [1, 2, 3, 4, 5, 6, 7, 8];

        $scope.timeSlots = [
          '09:00 AM', '09:30 AM',
          '10:00 AM', '10:30 AM',
          '11:00 AM', '11:30 AM',
          '12:00 PM', '12:30 PM',
          '01:00 PM', '02:00 PM',
          '03:00 PM', '04:00 PM',
          '05:00 PM', '06:00 PM'
        ];

        $scope.services = [
          'Hair Cut',
          'Shaving'
        ];

        // ── Form model ──
        vm.Booking  = {};
        $scope.submitted = false;

        // ── Submit handler ──
        $scope.submitForm = function(form) {
          if (form.$invalid) {
            // Touch all fields to reveal errors
            angular.forEach(form, function(field) {
              if (typeof field === 'object' && field.hasOwnProperty('$modelValue')) {
                field.$setTouched();
              }
            });
            return;
          }
          $scope.submitted = true;

            console.log('Booking submitted:', $scope.booking);
        };

        // ── Reset ──
        vm.resetForm = function() {
          vm.Booking   = {};
          $scope.submitted = false;
          vm.bookingSuccess=null;
        };

        vm.trackEntityChanges = function(booking) {
          console.log(booking)
          if(booking.state==0||!booking.state ){
            booking.state=1;
          }
        };

        vm.submit =function(form)
        {
          if (form.$invalid) {
            // Touch all fields to reveal errors
            angular.forEach(form, function(field) {
              if (typeof field === 'object' && field.hasOwnProperty('$modelValue')) {
                field.$setTouched();
              }
            });
            return;
          }
          $http({
              method: 'POST',
              url: 'https://localhost:7080/api/Booking',
              data:vm.Booking
          }).then(function successCallback(response) {
                  vm.bookingSuccess = true;
                  $scope.submitted = true;
          }, function errorCallback(error) {
                  $scope.submitted = true;
                  vm.bookingSuccess = false;
                  vm.errorMessage = error.data;
                  console.log(error)
          });
        }

      }]);