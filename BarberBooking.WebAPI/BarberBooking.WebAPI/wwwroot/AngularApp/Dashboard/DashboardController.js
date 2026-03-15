angular.module('dashboardApp', [])
.controller('DashboardController', ['$timeout','$http', function($timeout,$http) {
    var vm = this;

    // ── SignalR ──
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/bookingHub")  // must match app.MapHub<BookingHub>("/bookingHub")
        .withAutomaticReconnect()
        .build();

    //We match the event "ReceiveBooking" with the related method. 
    //When the Backend calls Clients.All.SendAsync("ReceiveBooking"), this method will be executed in the Frontend.
    connection.on("ReceiveBooking", function () {
        playBell();
        getAllBookings();
    });

    connection.start()
        .then(function () { console.log("Connected!"); })
        .catch(function (err) { console.error("Connection failed:", err); });


    // ── Navigation ──
    vm.currentPage = 'home';

    vm.navigate = function(page) 
    { 
        vm.currentPage = page; 
        if(page=='bookings'){
           getAllBookings();
        }
        if(page=='profile'){
           getMyPerson();
        }
        if(page=='newbooking'){
          window.location.href = "../Booking/bookingForm.html";
        }
    };

    function getAllBookings()
    {
        $http({
            method: 'GET',
            url: 'https://localhost:7080/api/Booking'
        }).then(function successCallback(response) {
              vm.bookings=response.data;
        }, function errorCallback(error) {
              alert('Failed to load all bookings');
              console.log(error.data)
        });
    }

    function getMyPerson()
    {
        $http({
            method: 'GET',
            url: 'https://localhost:7080/api/Person/63E32E67-D753-4D72-D50B-08DE7C617713'
        }).then(function successCallback(response) {
              vm.myProfile=response.data.value;
        }, function errorCallback(error) {
              alert('Failed to load Profile');
              console.log(error.data)
        });
    }

    // ── Stats ──
    /*vm.stats = {
      total:     vm.bookings.length,
      confirmed: vm.bookings.filter(b => b.status === 'Confirmed').length,
      pending:   vm.bookings.filter(b => b.status === 'Pending').length,
      cancelled: vm.bookings.filter(b => b.status === 'Cancelled').length
    };

    // ── Profile ──
    vm.profile = {
      firstName: 'Jane',
      lastName:  'Doe',
      email:     'jane.doe@reservio.com',
      phone:     '+1 555 000 9999',
      bio:       'Passionate about delivering exceptional booking experiences.',
      role:      'Administrator',
      language:  'English'
    };*/

    vm.savedToast = false;
    vm.saveProfile = function() {
      vm.savedToast = true;
      $timeout(function() { vm.savedToast = false; }, 3000);
    };

    // ── Badge helper ──
    vm.badgeClass = function(status) {
      return {
        'badge-confirmed': status === 'Confirmed',
        'badge-pending':   status === 'Pending',
        'badge-cancelled': status === 'Cancelled'
      };
    };

    // ── Search filter ──
    vm.search = '';
    vm.filterStatus = '';
    vm.searchFilter = function(booking) {
      var q = (vm.search || '').toLowerCase();
      var matchSearch = !q ||
        booking.id.toLowerCase().includes(q) ||
        booking.firstName.toLowerCase().includes(q) ||
        booking.lastName.toLowerCase().includes(q)  ||
        booking.service.toLowerCase().includes(q)   ||
        booking.email.toLowerCase().includes(q);
      var matchStatus = !vm.filterStatus || booking.status === vm.filterStatus;
      return matchSearch && matchStatus;
    };

    // ── Actions ──
    vm.changeStatus = function(id,status) {
        var bookingToUpdate = vm.bookings.filter(b => b.id==id)[0];
        bookingToUpdate.status = status;
        bookingToUpdate.state = 2;
        vm.saveBooking(bookingToUpdate);
    };

    vm.saveBooking=function (booking) {
        $http({
            method: 'PUT',
            url: 'https://localhost:7080/api/Booking',
            data: booking
        }).then(function successCallback(response) {
              alert('Action Completed Successfully')
        }, function errorCallback(error) {
              alert('Failed to update booking');
              console.log(error.data)
        });
    }

    function playBell() {
        var context = new AudioContext();
        var oscillator = context.createOscillator();
        var gainNode = context.createGain();

        oscillator.connect(gainNode);
        gainNode.connect(context.destination);

        oscillator.type = 'sine';
        oscillator.frequency.value = 830;       // bell pitch in Hz

        gainNode.gain.setValueAtTime(1, context.currentTime);
        gainNode.gain.exponentialRampToValueAtTime(0.001, context.currentTime + 1.5); // fade out

        oscillator.start(context.currentTime);
        oscillator.stop(context.currentTime + 1.5);
    }
    function refreshStats() {
      vm.stats.confirmed = vm.bookings.filter(b => b.status === 'Confirmed').length;
      vm.stats.pending   = vm.bookings.filter(b => b.status === 'Pending').length;
      vm.stats.cancelled = vm.bookings.filter(b => b.status === 'Cancelled').length;
    }

}]);