(function () {
	'use strict';

	angular
		.module('app')
		.controller('mainCtrl', ['$rootScope', mainCtrl]);

	function mainCtrl($rootScope) {
		var self = this;

		$rootScope._errorMessage = null;
		$rootScope._alerts = [];

		$rootScope.addAlert = function (message, type) {
			$rootScope._alerts.unshift({ message: message || 'Непридвиденная ошибка.', type: type });
		};

		$rootScope.closeAlert = function (index) {
			$rootScope._alerts.splice(index, 1);
		};

	}
})();