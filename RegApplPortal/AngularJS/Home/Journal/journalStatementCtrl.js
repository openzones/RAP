(function () {
	'use strict';

	angular
		.module('journalStatement')
		.controller('journalStatementCtrl', ['$rootScope', '$parse', 'JournalStatementService', journalStatementCtrl]);

	function journalStatementCtrl($rootScope, $parse, JournalStatementService) {
		var self = this;

		if ($parse('modelState.hasErrors')(model)) {
			$rootScope._errorMessage = $parse('modelState.errorMessage')(model) || 'Произошла непредвиденная ошибка.';
			return;
		}

		self.model = angular.copy(model);
		self.users = $parse('model.users')(self);
		self.directory = $parse('model.directories')(self);
		var journalStatementService = new JournalStatementService(self.model);

		self.getStatement = journalStatementService.getStatement;
		self.statementsGrid = journalStatementService.statementsGrid;
		self.treatmentPeriod = journalStatementService.treatmentPeriod;
		self.periodResponsible = journalStatementService.periodResponsible;
		self.createDateFilter = journalStatementService.createDateFilter;
		self.removeFilter = journalStatementService.removeFilter;
		self.createDateFromFilter = journalStatementService.createDateFromFilter;
		self.createDateToFilter = journalStatementService.createDateToFilter;
		self.lastDateFilter = journalStatementService.lastDateFilter;
		self.lastDateFromFilter = journalStatementService.lastDateFromFilter;
		self.lastDateToFilter = journalStatementService.lastDateToFilter;
		self.removeLastStatusFilter = journalStatementService.removeLastStatusFilter;
		self.createDateFromValue = journalStatementService.createDateFromValue;
		self.createDateToValue = journalStatementService.createDateToValue;
		self.lastDateFromValue = journalStatementService.lastDateFromValue;
		self.lastDateToValue = journalStatementService.lastDateToValue;
	}
})();