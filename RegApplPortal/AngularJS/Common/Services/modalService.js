(function () {
	'use strict';

	angular
		.module('common.modalService', [])
		.service('modalService', ['$uibModal', modalService]);

	function modalService($uibModal) {
		var self = this;

		var modalDefaults = {
			backdrop: true,
			keyboard: true,
			modalFade: true,
			animation: false,
			templateUrl: 'AngularJS/Common/Partials/default.html'
		};

		var modalOptions = {
			closeButtonText: 'Отмена',
			actionButtonText: 'ОК',
			headerText: 'Продолжить?',
			bodyText: 'Выполнить это действие?'
		};

		self.showModal = showModal;
		self.show = show;



		function showModal(customModalDefaults, customModalOptions) {
			if (!customModalDefaults) {
				customModalDefaults = {};
			}

			customModalDefaults.backdrop = 'static';

			return self.show(customModalDefaults, customModalOptions);
		}

		function show(customModalDefaults, customModalOptions) {
			//Create temp objects to work with since we're in a singleton service
			var tempModalDefaults = {};
			var tempModalOptions = {};

			//Map angular-ui modal custom defaults to modal defaults defined in service
			angular.extend(tempModalDefaults, modalDefaults, customModalDefaults);

			//Map modal.html $scope custom properties to defaults defined in service
			angular.extend(tempModalOptions, modalOptions, customModalOptions);

			if (!tempModalDefaults.controller) {
				tempModalDefaults.controller = function ($scope, $uibModalInstance) {
					$scope.modalOptions = tempModalOptions;
					$scope.modalOptions.ok = function (result) {
						$uibModalInstance.close(result);
					};
					$scope.modalOptions.close = function (result) {
						$uibModalInstance.dismiss('cancel');
					};
				};

				tempModalDefaults.controller.$inject = ['$scope', '$uibModalInstance'];
			}

			return $uibModal.open(tempModalDefaults).result;
		}
	}
})();