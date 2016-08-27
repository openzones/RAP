(function () {
	'use strict';

	angular
		.module('statement')
		.factory('StatementService', ['$rootScope', '$window', '$parse', '$timeout', 'ajaxService', 'modalService', 'convertDate', 'uiGridConstants', statementService]);

	function statementService($rootScope, $window, $parse, $timeout, ajaxService, modalService, convertDate, uiGridConstants) {
		var StatementService = function (model) {

			var self = this;			

			self.model = model;
			self.changeStatus = changeStatus;
			self.addStatus = addStatus;
			self.addFile = addFile;
			self.getFile = getFile;
			self.treatmentPeriod = treatmentPeriod;
			self.periodResponsible = periodResponsible;
			self.validationComplete = validationComplete;
			self.blockButtons = blockButtons;
			self.isShowExecution = isShowExecution;
			self.viewStatus = viewStatus;
			self.saveExecution = saveExecution;
			self.validatorMedDocumentType = validatorMedDocumentType;
			self.validationReasonID = validationReasonID;
			self.validationExpertiseID = validationExpertiseID;

			console.log(self.model);
			var _modalDefaults = {
				templateUrl: 'AngularJS/Common/Partials/changeStatus.html',
				size: 'lg',
				controller: 'changeStatusCtrl',
				controllerAs: 'ctrl'
			};

			var _modalFileDefaults = {
				templateUrl: 'AngularJS/Common/Partials/addFile.html',
				size: 'lg',
				controller: 'addFileCtrl',
				controllerAs: 'ctrl'
			};

			var _modalOptions = {
				closeButtonText: 'Отмена',
				actionButtonText: 'Сохранить',
				headerText: 'Работа с заявлением'
			};
		
			var _directories = $parse('model.directories')(self);
			var _statement = $parse('model.statement')(self);

			_statement.incidentDate = new Date(_statement.incidentDate);

			function updateStatement(id) {
				ajaxService.ajax('Home/GetStatement?id=' + id, 'POST', null, function (response) {
	
					var item = convertDate.convert(response);
					self.model.statement = item;
					_statement = item;										
					self.statusGrid.data = $parse('statementStatuses')(item);
					self.fileGrid.data = $parse('files')(item);
				});
			}

			// маски для медицинских документов
			function validatorMedDocumentType(id) {
				if (id == 3)
					return "999999999";  // /^\d{9}$/.test(unifiedPolicyNumber);
				if (id == 4 || id == 7)
					return "9999999999999999";  // /^\d{16}$/.test(unifiedPolicyNumber);
				if (id == 6)
					return "****************"; // /^\w{16}$/.test(unifiedPolicyNumber);
				if (id == 8)
					return null; // /^\w{50}$/.test(unifiedPolicyNumber);
			};

			// все звездочки исчезли?
			function validationComplete() {
				return $('.cs_required').length == 0;
			}
			// блок кнопок при первом сохранении
			function blockButtons() {
				return (_statement.id == 0);
			}
			// вывод блока "Исполнение"
			function isShowExecution() {
				return (_statement.lastStatementStatusID == 2 || _statement.lastStatementStatusID == 3 || _statement.lastStatementStatusID == 6 || _statement.lastStatementStatusID == 7);
			}

			// сохранение блока "Исполнение"
			function saveExecution() {
				ajaxService.ajax('Home/SaveExecution', 'POST', _statement.execution, function (result) {
					console.log(result);

					var statementID = $parse('statementID')(result);

					if (!statementID) {
						return;
					}

					$rootScope.addAlert('Изменения успешно сохранены.', 'success');
					updateStatement(statementID);
				});
			}

			// сохранение нового обращения или изменение обращения
			function changeStatus() {
				console.log(_statement);
				if (_statement.id == 0)
				{
					var resolve = {
						modalOptions: function () {
							return _modalOptions;
						},
						departmentName: function () {
							return "Редактирование статуса";
						},
						model: function () {
							return self.model;
						}
					};

				_modalDefaults.resolve = resolve;

				modalService.showModal(_modalDefaults, _modalOptions)
					.then(function (result) {
						_statement.statementStatuses.push(result);

						ajaxService.ajax('Home/SaveStatementAll', 'POST', _statement, function (result) {
							var statementID = $parse('statementID')(result);

							if (!statementID) {
								return;
							}

							$rootScope.addAlert('Изменения успешно сохранены.', 'success');
							console.log(statementID);
							updateStatement(statementID);

							$timeout(function () { addFile(); }, 200);							
						});
					});
				}
				else
				{
					var ok = confirm("Вы уверены, что хотите сохранить обращение?");
					if (ok)
						{						

						ajaxService.ajax('Home/SaveStatement', 'POST', _statement, function (result) {
							console.log(result);
							$rootScope.addAlert('Изменения успешно сохранены.', 'success');
							updateStatement(result);
						});
					}
				}
			}
			// срок обработки
			function treatmentPeriod() {
				if (!_statement.createDate)
					return { 'color': 'grey' };

				var date = getDayDelta(_statement.createDate, new Date());

				if (date < 20)
					return { 'color': 'green' };
				else if (date > 20 && date < 30)
					return { 'color': 'yellow' };
				else
					return { 'color': 'red' };
			}

			// разница между датами
			function getDayDelta(incomingDate, todayDate) {
				var date1 = new Date(incomingDate);
				var date2 = new Date(todayDate);
				var timeDiff = Math.abs(date2.getTime() - date1.getTime());
				var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));

				return diffDays;
			}

			// срок на ответственном
			function periodResponsible() {
				if (!_statement.lastStatusDate)
					return { 'color': 'grey' };

				var date = getDayDelta(_statement.lastStatusDate, new Date());

				if (date < 2)
					return { 'color': 'green' };
				else
					return { 'color': 'red' };
			}

			// добавление статуса
			function addStatus() {
				var resolve = {
					modalOptions: function () {
						return _modalOptions;
					},
					departmentName: function () {
						return "Добавить статус";
					},
					model: function () {
						return self.model;
					}
				};

				_modalDefaults.resolve = resolve;

				modalService.showModal(_modalDefaults, _modalOptions)
					.then(function (result) {

						ajaxService.ajax('Home/SaveStatus', 'POST', result, function (result) {
							console.log(result);

							var statementID = $parse('statementID')(result);

							if (!statementID) {
								return;
							}

							$rootScope.addAlert('Изменения успешно сохранены.', 'success');
							updateStatement(statementID);
							$timeout(function () { addFile(); }, 500);
						});
					});
			}

			function viewStatus(input)
			{
				var models = angular.copy(self.model);				

				var item = _.find(models.statement.statementStatuses, function (val) {
					return val.id == input;
				});

				models.status = item;

				var resolve = {
					modalOptions: function () {
						return _modalOptions;
					},
					departmentName: function () {
						return "Добавить статус";
					},
					model: function () {
						return models;
					}
				};

				_modalDefaults.resolve = resolve;

				modalService.showModal(_modalDefaults, _modalOptions);
			}

			// добавление файла
			function addFile() {
				var resolve = {
					modalOptions: function () {
						return _modalOptions;
					},
					departmentName: function () {
						return "Добавить файл";
					},
					model: function () {
						return self.model;
					}
				};

				_modalFileDefaults.resolve = resolve;

				modalService.showModal(_modalFileDefaults, _modalOptions)
					.then(function (result) {
						$rootScope.addAlert('Изменения успешно сохранены.', 'success');
						$timeout(function () {
							updateStatement(result);
						}, 1000);
					});				
			}

			// настройка grid
			self.statusGrid = {
				enableRowSelection: true,
				enableRowHeaderSelection: false,
				multiSelect: false,
				useExternalFiltering: false,
				data: $parse('statementStatuses')(_statement),
				columnDefs: getStatusGridColumnDefs(),
				rowTemplate: getStatusGridRowTemplate()
			};
			self.fileGrid = {
				enableRowSelection: true,
				enableRowHeaderSelection: false,
				multiSelect: false,
				useExternalFiltering: false,
				data: $parse('files')(_statement),
				columnDefs: getFilesGridColumnDefs(),
				rowTemplate: getFileGridRowTemplate()
			};
			// настройка строк grid
			function getStatusGridRowTemplate() {
				return '<div class="ui-grid-cell" style="cursor: pointer;"' +
							'ng-dblclick="grid.appScope.stCtrl.viewStatus(row.entity.id)"' +
							'ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"' +
							'ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name"' +
							'ui-grid-cell>' +
						'</div>';
			}
			// настройка строк grid
			function getFileGridRowTemplate() {
				return '<div class="ui-grid-cell" style="cursor: pointer;"' +
							'ng-dblclick="grid.appScope.stCtrl.getFile(row.entity.id)"' +
							'ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"' +
							'ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name"' +
							'ui-grid-cell>' +
						'</div>';
			}
			// настройка колонок status grid
			function getStatusGridColumnDefs() {
				return [
					{
						name: 'userID',
						field: 'userID',
						displayName: 'Автор',
						cellFilter: 'directoryItem : grid.appScope.stCtrl.model.users : \'fullname\'',
						cellClass: 'text-center',
						headerCellClass: 'text-center',
						enableColumnMenu: false,
						width: '20%'
					},
					{
						name: 'statusDate',
						field: 'statusDate',
						displayName: 'Дата статуса',
						cellFilter: 'date : \'dd-MM-yyyy HH:mm:ss\'',
						cellClass: 'text-center',
						headerCellClass: 'text-center',
						enableColumnMenu: false,
						sort: {
							direction: uiGridConstants.ASC,
							priority: 0,
						},
						width: '15%'
					},
					{
						name: 'statusID',
						field: 'statusID',
						displayName: 'Статус',
						cellFilter: 'directoryItem : grid.appScope.stCtrl.directory.refStatus : \'name\'',
						cellClass: 'text-center',
						headerCellClass: 'text-center',
						enableColumnMenu: false,
						width: '10%'
					},
					{
						name: 'comment',
						field: 'comment',
						displayName: 'Комментарий',
						cellTemplate: '<div uib-tooltip="{{row.entity.comment}}" tooltip-append-to-body="true">{{row.entity.comment}}</div>',
						cellClass: 'text-center',
						headerCellClass: 'text-center',
						enableColumnMenu: false,
						width: '20%'
					},
					{
						name: 'assignedToUserID',
						field: 'assignedToUserID',
						displayName: 'Назначен на',
						cellFilter: 'directoryItem : grid.appScope.stCtrl.model.users : \'fullname\'',
						cellClass: 'text-center',
						headerCellClass: 'text-center',
						enableColumnMenu: false,
						width: '20%'
					},
					{
						name: 'executeToDate',
						field: 'executeToDate',
						displayName: 'Исполнить до',
						cellFilter: 'date : \'dd-MM-yyyy HH:mm:ss\'',
						cellClass: 'text-center',
						headerCellClass: 'text-center',
						enableColumnMenu: false,
						width: '15%'
					}
				];
			}

			// заполнение files grid
			function getFilesGridColumnDefs()
			{
				return [					
					{
						name: 'attachmentDate',
						field: 'attachmentDate',
						displayName: 'Дата прикрепления',
						cellFilter: 'date : \'dd-MM-yyyy HH:mm:ss\'',
						cellClass: 'text-center',
						headerCellClass: 'text-center',
						enableColumnMenu: false,
						width: '15%',
						sort: {
							direction: uiGridConstants.ASC,
							priority: 0,
						},
					},
					{
						name: 'userID',
						field: 'userID',
						displayName: 'Автор',
						cellFilter: 'directoryItem : grid.appScope.stCtrl.model.users : \'fullname\'',
						cellClass: 'text-center',
						headerCellClass: 'text-center',
						enableColumnMenu: false,
						width: '20%'
					},
					{
						name: 'fileName',
						field: 'fileName',
						displayName: 'Название',
						cellClass: 'text-center',
						headerCellClass: 'text-center',
						enableColumnMenu: false,						
						width: '20%'
					},
					{
						name: 'nominationID',
						field: 'nominationID',
						displayName: 'Наименование',
						cellFilter: 'directoryItem : grid.appScope.stCtrl.directory.refNomination : \'name\'',
						cellClass: 'text-center',
						headerCellClass: 'text-center',
						enableColumnMenu: false,
						width: '15%'
					},
					{
						name: 'comment',
						field: 'comment',
						displayName: 'Комментарий',
						cellTemplate: '<div uib-tooltip="{{row.entity.comment}}" tooltip-append-to-body="true">{{row.entity.comment}}</div>',
						cellClass: 'text-center',
						headerCellClass: 'text-center',
						enableColumnMenu: false,
						width: '30%'
					}
				];
			}

			// вытащить файл
			function getFile(fileId) {
				$window.location.href = 'Home/GetFile?fileId=' + fileId;
			}

			// Поле «причина обращения» запретить координатору проставлять какой-либо статус, пока не заполнит данное поле.
			function validationReasonID() {
				return _.some(self.model.currentUser.roles, function (item) {
					return item.id == 3;
				});
			}

			// Сделать поле «формат экспертизы» обязательным для заполнения на роль «ответственный», запретить проставлять какой-либо статус, пока поле не будет заполнено
			function validationExpertiseID() {
				return _.some(self.model.currentUser.roles, function (item) {
					return item.id == 5;
				});
			}

		}

		return StatementService;
	}
})();