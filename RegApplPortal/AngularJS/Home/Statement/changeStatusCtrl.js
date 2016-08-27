(function () {
	'use strict';

	angular
		.module('statement')
		.controller('changeStatusCtrl', ['$uibModalInstance', '$document', '$parse', '$timeout', 'modalOptions', 'model', 'departmentName', 'ajaxService', changeStatusCtrl]);

	function changeStatusCtrl($uibModalInstance, $document, $parse, $timeout, modalOptions, model, departmentName, ajaxService) {
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

		self.departmentName = angular.copy(departmentName);
		self.directory = $parse('directories')(_model);
		self.status = $parse('status')(_model);
		self.users = $parse('users')(_model);
		self.currentUser = $parse('currentUser')(_model);
		self.file = $parse('file')(_model);
		self.statement = $parse('statement')(_model);
		self.roles = $parse('roles')(_model);
		self.currentUser = $parse('currentUser')(_model);
		self.update = update;
		self.close = close;
		self.validationComplete = validationComplete;
		self.isReview = isReview;
		self.userList = userList;
		self.statusList = statusList;
		self.checkAssignedToUserID = checkAssignedToUserID;
		self.checkExecuteToDate = checkExecuteToDate;
		self.isDisabledStatus = false;
		self.onChangeStatus = onChangeStatus;
		console.log(self.status);
		if (self.status.id == 0)
		{
			self.status.userID = self.currentUser.id;
			self.status.statementID = self.statement.id;
		}

		// все звездочки исчезли?
		function validationComplete() {
			return $('.cs_required').length == 0;
		}

		// флк на изменение статуса
		function onChangeStatus() {
			if (self.status.id == 0) {
				self.isDisabledStatus = self.status.statusID == 6 ? true : false;
				self.status.assignedToUserID = self.status.statusID == 6 ? self.statement.responsibleID : null;
			}
		}

		function update() { 

			if (!!self.filesCount) {
				$timeout(function () {
					self.uploadFilesElement.trigger('click');
				}, 0, false);
			} 

			$uibModalInstance.close(self.status);
		}
		// Если статус «на исполнении», то требовать заполнение «исполнить до»
		function checkExecuteToDate(statusID) {
			return (statusID == 2);
		}
		// Если выбраны статусы: на исполнении, у ответственного, у координатора, у куратора то требовать заполнение поля «назначено на».
		function checkAssignedToUserID(statusID) {
			return (statusID == 2 || statusID == 4 || statusID == 5 || statusID == 8);
		}

		// формирование списка статусов по роли пользователя
		function statusList() {			
			if (self.status.id == 0) {

				var curator = _.filter(self.directory.refStatus, function (v) { return v.code == 3 || v.code == 4 || v.code == 5 });
				var registrator = _.filter(self.directory.refStatus, function (v) { return v.code == 1 || v.code == 3 });
				var coordinator = _.filter(self.directory.refStatus, function (v) { return v.code == 2 || v.code == 3 || v.code == 8 || v.code == 5 || v.code == 7 });
				var responsible = _.filter(self.directory.refStatus, function (v) { return v.code == 2 || v.code == 3 || v.code == 5 });
				var executive = _.filter(self.directory.refStatus, function (v) { return v.code == 3 || v.code == 4 || v.code == 6 });

				var value = [];

				_.each(self.currentUser.roles, function (i) {

					switch (i.name) {
						case "Administrator":
							value = _.union(value, self.directory.refStatus);
							break;
						case "Registrator":
							value = _.union(value, registrator);
							break;
						case "Coordinator":
							value = _.union(value, coordinator);
							break;
						case "Сurator":
							value = _.union(value, curator);
							break;
						case "Responsible":
							value = _.union(value, responsible);
							break;
						case "Executive":
							value = _.union(value, executive);
							break;
						default: break;
					}

				});

				var result = _.uniq(value, function (t) {
					return t.code;
				});

				return result;
			}
			else
				return self.directory.refStatus;				
		}

		// формирование списка пользователей для статуса
		function userList()
		{
			if (self.status.id == 0) {
				var value = '';
				switch (self.status.statusID) {
					case 1: value = null; break;
					case 2: value = 'Executive'; break;
					case 3: return self.users; break;
					case 4: value = 'Responsible'; break;
					case 5: value = 'Coordinator'; break;
					case 6: value = 'Responsible'; break;
					case 7: value = null; break;
					case 8: value = 'Сurator'; break;
					default: value = null; break;
				}

				var item = _.filter(self.users, function (val) {
					var item = _.some(val.roles, function (v) {
						return v.name == value;
					});
					return item ? val : null;
				});
				return item;
			}
			else
				return self.users;
		}

		function close(result) {
			if (self.status.id == 0)
			{				
				if (!confirm('Продолжить без сохранения?')) {
					return;
				}
			}

			$uibModalInstance.dismiss('cancel');
		}

		function isReview() {
			return self.status.id != 0;
		}
	}
})();

