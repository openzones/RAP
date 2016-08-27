(function () {
	'use strict';

	angular
		.module('journalStatement')
		.controller('customGridDateFilter', ['$uibModalInstance', '$document', '$parse', '$timeout', 'modalOptions', 'model', 'departmentName', 'ajaxService', customGridDateFilter]);

	function customGridDateFilter($uibModalInstance, $document, $parse, $timeout, modalOptions, model, departmentName, ajaxService) {
		var ctrl = this;

		console.log('filter name', departmentName);

		ctrl.title = 'Select Dates ' + departmentName + '...';
		ctrl.minDate = new Date();
		ctrl.maxDate = new Date();
		ctrl.customDateFilterForm;

		ctrl.filterDate = (departmentName.indexOf('From') !== -1) ? angular.copy(ctrl.minDate) : angular.copy(ctrl.maxDate);

		function setDateToStartOfDay(date) {
			return new Date(date.getFullYear(), date.getMonth(), date.getDate());
		}

		function setDateToEndOfDay(date) {
			return new Date(date.getFullYear(), date.getMonth(), date.getDate(), 23, 59, 59);
		}

		ctrl.filterDateChanged = function () {
			ctrl.filterDate = (departmentName.indexOf('From') !== -1) ? setDateToStartOfDay(ctrl.filterDate) : setDateToEndOfDay(ctrl.filterDate);
			console.log('new filter date', ctrl.filterDate);
		};

		ctrl.setFilterDate = function (date) {
			$uibModalInstance.close(date);
		};

		ctrl.cancelDateFilter = function () {
			$uibModalInstance.dismiss();
		};
	}
})();

