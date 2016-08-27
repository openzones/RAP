(function () {
	'use strict';

	angular
		.module('statement')
		.controller('statementCtrl', ['$rootScope', '$parse', 'StatementService', statementCtrl]);

	function statementCtrl($rootScope, $parse, StatementService) {
		var self = this;

		if ($parse('modelState.hasErrors')(model)) {
			$rootScope._errorMessage = $parse('modelState.errorMessage')(model) || 'Произошла непредвиденная ошибка.';
			return;
		}		

		var statementService = new StatementService(model);

		self.model = statementService.model;
		self.statusGrid = statementService.statusGrid;
		self.fileGrid = statementService.fileGrid;
		self.directory = $parse('model.directories')(self);		
		self.changeStatus = statementService.changeStatus;
		self.addStatus = statementService.addStatus;
		self.addFile = statementService.addFile;
		self.getFile = statementService.getFile;
		self.treatmentPeriod = statementService.treatmentPeriod;
		self.periodResponsible = statementService.periodResponsible;
		self.validationComplete = statementService.validationComplete;
		self.blockButtons = statementService.blockButtons;
		self.isShowExecution = statementService.isShowExecution;
		self.viewStatus = statementService.viewStatus;
		self.saveExecution = statementService.saveExecution;
		self.validatorMedDocumentType = statementService.validatorMedDocumentType;
		self.validationReasonID = statementService.validationReasonID;
		self.validationExpertiseID = statementService.validationExpertiseID;
	}
})();