(function () {
    'use strict';

    angular
        .module('app', [])
        .controller('MainController', MainController);

    MainController.$inject = ['$scope', '$http'];

    function MainController($scope, $http) {
        $scope.title = 'Bank Account';
        var appSettings = {
            apiUrl: 'https://localhost:44301/api/'
        };

        $scope.deposit = deposit;
        $scope.withdraw = withdraw;

        activate();

        function activate() {
            var guid = '4939209E-8CAA-4722-AC0D-31A1B15462DD';
            getBankAccountHeader(guid)
                .then(function (bankAccountHeader) {
                    $scope.status = bankAccountHeader.Status;
                    $scope.balance = bankAccountHeader.Balances[0].Amount + ' ' + bankAccountHeader.Balances[0].CurrencyISOCode;
                });
        }

        function getBankAccountHeader(guid) {
            var url = appSettings.apiUrl + 'bank-account' + '/' + guid;
            return $http.get(url).then(function (response) {
                return response.data;
            });
        }    

        function putDeposit() {
            var guid = '4939209E-8CAA-4722-AC0D-31A1B15462DD';
            var url = appSettings.apiUrl + 'bank-account/deposite/' + guid;
            var updateAmount = {
                currencyISOCode: 'PLN',
                amount: 1.0
            };
            return $http.put(url, updateAmount).then(function (response) {
                return response.data;
            });
        }   

        function putWithdraw() {
            var guid = '4939209E-8CAA-4722-AC0D-31A1B15462DD';
            var url = appSettings.apiUrl + 'bank-account/withdraw/' + guid;
            var updateAmount = {
                currencyISOCode: 'PLN',
                amount: 1.0
            };
            return $http.put(url, updateAmount).then(function (response) {
                return response.data;
            });
        }   

        function deposit() {
            putDeposit()
                .then(function (bankAccountHeader) {
                    $scope.status = bankAccountHeader.Status;
                    $scope.balance = bankAccountHeader.Balances[0].Amount + ' ' + bankAccountHeader.Balances[0].CurrencyISOCode;
                });
        }

        function withdraw() {
            putWithdraw()
                .then(function (bankAccountHeader) {
                    $scope.status = bankAccountHeader.Status;
                    $scope.balance = bankAccountHeader.Balances[0].Amount + ' ' + bankAccountHeader.Balances[0].CurrencyISOCode;
                });
        }
    }
})();
