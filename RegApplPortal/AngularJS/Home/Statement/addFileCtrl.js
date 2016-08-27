(function () {
	'use strict';

	angular
		.module('statement')
		.controller('addFileCtrl', ['$uibModalInstance', '$document', '$parse', '$timeout', 'modalOptions', 'model', 'departmentName', 'ajaxService', addFileCtrl]);

	function addFileCtrl($uibModalInstance, $document, $parse, $timeout, modalOptions, model, departmentName, ajaxService) {
		var self = this;

		$document.ready(function () {
			$timeout(function () {
				self.inputElement = angular.element(document.querySelector('#input-element'));
				self.selectFilesElement = angular.element(document.querySelector('#select-files'));
				self.uploadFilesElement = angular.element(document.querySelector('#upload-files'));

			}, 50);
		});


		var _model = angular.copy(model);

		self.modalOptions = angular.copy(modalOptions);
		self.filesCount = 0;
		self.isDisabledUploadFile = isDisabledUploadFile;

		self.departmentName = angular.copy(departmentName);
		self.statement = $parse('statement')(_model);
		self.directory = $parse('directories')(_model);

		self.file = $parse('file')(_model);
		self.update = update;
		self.close = close;
		self.file.statementID = self.statement.id;

		if (self.statement.statementStatuses.length == 1)
			self.file.statementStatusID = self.statement.statementStatuses[0].id;
		else
			if (self.statement.statementStatuses.length > 1)
				self.file.statementStatusID = self.statement.statementStatuses[self.statement.statementStatuses.length - 1].id;
			else
				self.file.statementStatusID = 0;

		function update() {

			if (!!self.filesCount) {
				$timeout(function () {
					self.uploadFilesElement.trigger('click');
				}, 0, false);
			}

			$uibModalInstance.close(self.statement.id);
		}

		function close(result) {
			if (!confirm('Продолжить без сохранения?')) {
				return;
			}

			$uibModalInstance.dismiss('cancel');
		}

		function isDisabledUploadFile() {
			return (self.filesCount > 0 && !!self.file.nominationID && $('.cs_required').length == 0);
		}
	}
})();

