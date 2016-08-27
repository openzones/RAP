(function () {
	'use strict';

	angular
		.module('common.ajaxService', [])
		.factory('ajaxService', ['$http', '$log', '$parse', '$rootScope', ajaxService]);

	function ajaxService($http, $log, $parse, $rootScope) {
		$rootScope._loading = 0;

		return {
			ajax: ajax
		};

		function ajax(url, method, data, callback) {
			$rootScope._ajaxErrorMessage = null;
			$rootScope._loading++;

			var request = {
				method: method || 'GET',
				url: url,
				data: data
			};

			$http(request)
				.then(
					function (response) {
						if (angular.isFunction(callback)) {
							callback(response.data);
						}
					},
					function (err) {
						var message = $parse('data.Message')(err) || 'Неизвестная ошибка.';
						console.log(message);
						$log.error(message, err);
						$rootScope.addAlert(message, 'danger');
					}
				)
				.then(
					function () {
						$rootScope._loading--;
					}
				);
		}
	}
})();