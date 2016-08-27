(function () {
	'use strict';

	angular
		.module('journalStatement')
		.factory('JournalStatementService', ['$rootScope', '$parse', '$window', '$timeout', 'ajaxService', 'modalService', 'uiGridConstants', 'convertDate', journalStatementService]);

	function journalStatementService($rootScope, $parse, $window, $timeout, ajaxService, modalService, uiGridConstants, convertDate) {
		var JournalStatementService = function (model) {
			var self = this;

			var _model = angular.copy(model);
			console.log(_model);
			self.getStatement = getStatement;
			self.treatmentPeriod = treatmentPeriod;
			self.periodResponsible = periodResponsible;
			self.createDateFilter = createDateFilter;
			self.removeFilter = removeFilter;
			self.createDateFromFilter = createDateFromFilter;
			self.createDateToFilter = createDateToFilter;
			self.lastDateFilter = lastDateFilter;
			self.lastDateFromFilter = lastDateFromFilter;
			self.lastDateToFilter = lastDateToFilter;
			self.removeLastStatusFilter = removeLastStatusFilter;
			self.createDateFromValue = createDateFromValue;
			self.createDateToValue = createDateToValue;
			self.lastDateFromValue = lastDateFromValue;
			self.lastDateToValue = lastDateToValue;
			self.users = $parse('users')(_model);

			// настройка grid
			self.statementsGrid = {
				enableFiltering: true,
				enableRowSelection: true,
				enableRowHeaderSelection: false,
				multiSelect: false,
				useExternalFiltering: true,
				data: $parse('journal')(_model),
				columnDefs: getColumnDefs(),
				rowTemplate: getRowTemplate(),
				minRowsToShow: 15,
				onRegisterApi: statementsGridApi
			};
			// настройка строк grid
			function getRowTemplate() {
				return '<div class="ui-grid-cell" style="cursor: pointer;"' +
							'ng-dblclick="grid.appScope.journalCtrl.getStatement(row.entity.id)"' +
							'ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"' +
							'ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name"' +
							'ui-grid-cell>' +
						'</div>';
			}
			// настройка колонок grid
			function getColumnDefs() {
				return [
					{
						name: 'statementID',
						field: 'id',
						displayName: '№',
						cellClass: 'text-center',
						headerCellClass: 'text-center',
						enableColumnMenu: false,
						sort: {
							direction: uiGridConstants.DESC,
							priority: 0,
						},
						width: '5%'
					},
					{
						field: 'id',
						displayName: 'Срок обработки',
						cellTemplate: '<div class="glyphicon glyphicon-certificate" ng-style="grid.appScope.journalCtrl.treatmentPeriod(row.entity.createDate)"></div>',
						cellClass: 'text-center',
						headerCellClass: 'text-center',
						enableColumnMenu: false,
						enableFiltering: false,
						width: '5%'
					},
					{
						field: 'id',
						displayName: 'Срок на ответственном',
						cellTemplate: '<div class="glyphicon glyphicon-certificate" ng-style="grid.appScope.journalCtrl.periodResponsible(row.entity.lastStatusDate)"></div>',
						cellClass: 'text-center',
						headerCellClass: 'text-center',
						enableColumnMenu: false,
						enableFiltering: false,
						width: '5%'
					},
					{
						name: 'lastname',
						field: 'lastname',
						displayName: 'Фамилия',
						cellClass: 'text-center',
						headerCellClass: 'text-center',
						enableColumnMenu: false,
						width: '10%'
					},
					{
						name: 'firstname',
						field: 'firstname',
						displayName: 'Имя',
						cellClass: 'text-center',
						headerCellClass: 'text-center',
						enableColumnMenu: false,
						width: '10%'
					},
					{
						name: 'secondname',
						field: 'secondname',
						displayName: 'Отчество',
						cellClass: 'text-center',
						headerCellClass: 'text-center',
						enableColumnMenu: false,
						width: '10%'
					},
					{
						name: 'createDate',
						field: 'createDate',
						displayName: 'Дата создания',
						filterHeaderTemplate: '<div class="col-md-6"><button class="btn btn-default date-time-filter-buttons" style="width:90%;padding:inherit;" ng-click="grid.appScope.journalCtrl.createDateFilter(\'From\')" uib-tooltip="{{grid.appScope.journalCtrl.createDateFromValue() | date: \'dd.MM.yyyy\' }}">От</button>' +
							'<i ng-click="grid.appScope.journalCtrl.removeFilter(\'From\')" ng-if="grid.appScope.journalCtrl.createDateFromFilter()" class="ui-grid-icon-cancel cancel-custom-date-range-filter" ui-grid-one-bind-aria-label="aria.removeFilter" aria-label="Remove Filter">&nbsp;</i></div>' +
							'<div class="col-md-6"><button class="btn btn-default date-time-filter-buttons" style="width:90%;padding:inherit;" ng-click="grid.appScope.journalCtrl.createDateFilter(\'To\')" uib-tooltip="{{grid.appScope.journalCtrl.createDateToValue() | date: \'dd.MM.yyyy\' }}">До</button>' +
							'<i ng-click="grid.appScope.journalCtrl.removeFilter(\'To\')" ng-if="grid.appScope.journalCtrl.createDateToFilter()" class="ui-grid-icon-cancel cancel-custom-date-range-filter" ui-grid-one-bind-aria-label="aria.removeFilter" aria-label="Remove Filter">&nbsp;</i></div>',
						cellFilter: 'date : \'dd-MM-yyyy\'',
						cellClass: 'text-center',
						headerCellClass: 'text-center',
						enableColumnMenu: false,
						enableFiltering: true,
						enableCellEdit: false,
						width: '15%',
						filters: [
						  {
							condition: uiGridConstants.filter.GREATER_THAN_OR_EQUAL
						  },
						  {
							condition: uiGridConstants.filter.LESS_THAN_OR_EQUAL
						  }
						],
					},
					{
						name: 'reasonID',
						field: 'reasonID',
						displayName: 'Причина обращения',
						cellFilter: 'directoryItem : grid.appScope.journalCtrl.directory.refReason : \'name\'',
						cellClass: 'text-center',
						headerCellClass: 'text-center',
						enableColumnMenu: false,
						width: '10%',
						filter: {
							type: uiGridConstants.filter.SELECT,
							selectOptions: getSelectOptions($parse('directories.refReason')(_model))
						}
					},
					{
						name: 'expertiseID',
						field: 'expertiseID',
						displayName: 'Формат экспертизы',
						cellFilter: 'directoryItem : grid.appScope.journalCtrl.directory.refExpertise : \'name\'',
						cellClass: 'text-center',
						headerCellClass: 'text-center',
						enableColumnMenu: false,
						width: '10%',
						filter: {
							type: uiGridConstants.filter.SELECT,
							selectOptions: getSelectOptions($parse('directories.refExpertise')(_model))
						}
					},
					{
						name: 'subjectInsuranceID',
						field: 'subjectInsuranceID',
						displayName: 'Регион страхования',
						cellFilter: 'directoryItem : grid.appScope.journalCtrl.directory.refSubjectInsurance : \'name\'',
						cellClass: 'text-center',
						headerCellClass: 'text-center',
						enableColumnMenu: false,
						width: '15%',
						filter: {
							type: uiGridConstants.filter.SELECT,
							selectOptions: getSelectOptions($parse('directories.refSubjectInsurance')(_model))
						}
					},
					{
						name: 'lastStatusDate',
						field: 'lastStatusDate',
						displayName: 'Дата последнего статуса',
						filterHeaderTemplate: '<div class="col-md-6"><button class="btn btn-default date-time-filter-buttons" style="width:90%;padding:inherit;" ng-click="grid.appScope.journalCtrl.lastDateFilter(\'From\')" uib-tooltip="{{grid.appScope.journalCtrl.lastDateFromValue() | date: \'dd.MM.yyyy\' }}">От</button>' +
							'<i ng-click="grid.appScope.journalCtrl.removeLastStatusFilter(\'From\')" ng-if="grid.appScope.journalCtrl.lastDateFromFilter()" class="ui-grid-icon-cancel cancel-custom-date-range-filter" ui-grid-one-bind-aria-label="aria.removeFilter" aria-label="Remove Filter">&nbsp;</i></div>' +
							'<div class="col-md-6"><button class="btn btn-default date-time-filter-buttons" style="width:90%;padding:inherit;" ng-click="grid.appScope.journalCtrl.lastDateFilter(\'To\')" uib-tooltip="{{grid.appScope.journalCtrl.lastDateToValue() | date: \'dd.MM.yyyy\' }}">До</button>' +
							'<i ng-click="grid.appScope.journalCtrl.removeLastStatusFilter(\'To\')" ng-if="grid.appScope.journalCtrl.lastDateToFilter()" class="ui-grid-icon-cancel cancel-custom-date-range-filter" ui-grid-one-bind-aria-label="aria.removeFilter" aria-label="Remove Filter">&nbsp;</i></div>',
						cellFilter: 'date : \'dd-MM-yyyy\'',
						cellClass: 'text-center',
						headerCellClass: 'text-center',
						enableColumnMenu: false,
						width: '15%',
						filters: [
						  {
						  	condition: uiGridConstants.filter.GREATER_THAN_OR_EQUAL
						  },
						  {
						  	condition: uiGridConstants.filter.LESS_THAN_OR_EQUAL
						  }
						],
					},
					{
						name: 'lastStatementStatusID',
						field: 'lastStatementStatusID',
						displayName: 'Последний статус',
						cellFilter: 'directoryItem : grid.appScope.journalCtrl.directory.refStatus : \'name\'',
						cellClass: 'text-center',
						headerCellClass: 'text-center',
						enableColumnMenu: false,
						width: '15%',
						filter: {
							type: uiGridConstants.filter.SELECT,
							selectOptions: getSelectOptions($parse('directories.refStatus')(_model))
						}
					},
					{
						name: 'curatorID',
						field: 'curatorID',
						displayName: 'Куратор',
						cellFilter: 'directoryUsersItem : grid.appScope.journalCtrl.users : \'label\'',
						cellClass: 'text-center',
						headerCellClass: 'text-center',
						enableColumnMenu: false,
						width: '10%',
						filter: {							
							type: uiGridConstants.filter.SELECT,
							selectOptions: getUsersSelectOptions($parse('users')(_model))
						}
					},
					{
						name: 'responsibleID',
						field: 'responsibleID',
						displayName: 'Ответственный',
						cellFilter: 'directoryUsersItem : grid.appScope.journalCtrl.users : \'label\'',
						cellClass: 'text-center',
						headerCellClass: 'text-center',
						enableColumnMenu: false,
						width: '12%',
						filter: {
							type: uiGridConstants.filter.SELECT,
							selectOptions: getUsersSelectOptions($parse('users')(_model))
						}
					},
					{
						name: 'executiveID',
						field: 'executiveID',
						displayName: 'Исполнитель',
						cellFilter: 'directoryUsersItem : grid.appScope.journalCtrl.users : \'label\'',
						cellClass: 'text-center',
						headerCellClass: 'text-center',
						enableColumnMenu: false,
						width: '12%',
						filter: {
							type: uiGridConstants.filter.SELECT,
							selectOptions: getUsersSelectOptions($parse('users')(_model))
						}
					},
					{
						name: 'clientID',
						field: 'clientID',
						displayName: 'ID клиента на портале',
						cellClass: 'text-center',
						headerCellClass: 'text-center',
						enableColumnMenu: false,
						width: '15%'
					}
				];
			}

			var _modalDefaults = {
				templateUrl: 'AngularJS/Common/Partials/customDateFilter.html',
				size: 'md',
				controller: 'customGridDateFilter',
				controllerAs: 'custom'
			};

			var _modalOptions = {
				closeButtonText: 'Отмена',
				actionButtonText: 'Сохранить',
				headerText: 'Работа с заявлением'
			};

			function removeFilter(item) {
				if (item == "From")
					self.createDateFrom = null;
				if (item == "To")
					self.createDateTo = null;

				var filters = getStatementsGridFilter();

				var filter = {
					createDateFrom: self.createDateFrom,
					createDateTo: self.createDateTo
				};

				if (filters != null)
					filter = _.extend(filter, filters);

				$timeout(function () {
					getStatementList(filter, updateStatementsGridData);
				}, 500);
			}
			
			function removeLastStatusFilter(item) {
				if (item == "From")
					self.lastStatusDateFrom = null;
				if (item == "To")
					self.lastStatusDateTo = null;

				var filters = getStatementsGridFilter();

				var filter = {
					lastStatusDateFrom: self.lastStatusDateFrom,
					lastStatusDateTo: self.lastStatusDateTo
				};

				if (filters != null)
					filter = _.extend(filter, filters);

				$timeout(function () {
					getStatementList(filter, updateStatementsGridData);
				}, 500);
			}

			function createDateFromFilter() {
				return self.createDateFrom ? true : false;
			}

			function createDateToFilter() {
				return self.createDateTo ? true : false;
			}

			function createDateFromValue() {
				return self.createDateFrom ? self.createDateFrom : null;
			}

			function createDateToValue() {
				return self.createDateFrom ? self.createDateTo : null;
			}

			function lastDateFromFilter() {
				return self.lastStatusDateFrom ? true : false;
			}

			function lastDateToFilter() {
				return self.lastStatusDateTo ? true : false;
			}

			function lastDateFromValue() {
				return self.lastStatusDateFrom ? self.lastStatusDateFrom : null;
			}

			function lastDateToValue() {
				return self.lastStatusDateTo ? self.lastStatusDateTo : null;
			}

			// модальное окно для даты последнего статуса
			function lastDateFilter(filterName) {
				var resolve = {
					modalOptions: function () {
						return _modalOptions;
					},
					departmentName: function () {
						return filterName;
					},
					model: function () {
						return self.model;
					}
				};

				_modalDefaults.resolve = resolve;

				modalService.showModal(_modalDefaults, _modalOptions)
					.then(function (result) {
						if (filterName == "From")
							self.lastStatusDateFrom = result;
						if (filterName == "To")
							self.lastStatusDateTo = result;

						var filters = getStatementsGridFilter();

						var filter = {
							lastStatusDateFrom: self.lastStatusDateFrom,
							lastStatusDateTo: self.lastStatusDateTo
						};

						if (filters != null)
							filter = _.extend(filter, filters);

						$timeout(function () {
							getStatementList(filter, updateStatementsGridData);
						}, 500);
					});
			}

			// модальное окно для даты создания
			function createDateFilter(filterName) {
				var resolve = {
					modalOptions: function () {
						return _modalOptions;
					},
					departmentName: function () {
						return filterName;
					},
					model: function () {
						return self.model;
					}
				};

				_modalDefaults.resolve = resolve;

				modalService.showModal(_modalDefaults, _modalOptions)
					.then(function (result) {
						if (filterName == "From")
							self.createDateFrom = result;
						if (filterName == "To")
							self.createDateTo = result;

						var filters = getStatementsGridFilter();

						var filter = {
							createDateFrom: self.createDateFrom,
							createDateTo: self.createDateTo
						};

						if (filters != null)
							filter = _.extend(filter, filters);

						$timeout(function () {
							getStatementList(filter, updateStatementsGridData);
						}, 500);

					});
			}

			// срок обработки
			function treatmentPeriod(item) {
				if (!item)
					return { 'color': 'grey' };

				var date = getDayDelta(item, new Date());

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
			function periodResponsible(item) {
				if (!item)
					return {'color': 'grey'};

				var date = getDayDelta(item, new Date());

				if (date < 2)
					return {'color':'green'};
				else
					return {'color':'red'};
			}

			// зайти в выбранную в журнале заявку
			function getStatement(id) {
				$window.location.href = 'Home/Statement?id=' + id;
			}

			function getSelectOptions(directory) {
				var options = _.map(directory, function (directoryItem) {
					return {
						value: directoryItem.id,
						label: directoryItem.name
					};
				});

				var result = _.sortBy(options, 'label');
				return result;
			}

			function getUsersSelectOptions(directory) {
				var options = _.map(directory, function (directoryItem) {
					return {
						value: directoryItem.id,
						label: directoryItem.lastname + " " + directoryItem.firstname.substring(0, 1).toUpperCase() + ". " + (directoryItem.secondname != null ? directoryItem.secondname.substring(0, 1).toUpperCase() + ". " : null)
					};
				});

				var result = _.sortBy(options, 'label');
				return result;
			}

			// обновление таблицы
			function updateStatementsGridData(newValue) {
				self.statementsGrid.data = newValue;
			}

			// Grid Api
			function statementsGridApi(gridApi) {
				self.statementsGridApi = gridApi;
				var filterTimeout;
				console.log(gridApi);
				self.statementsGridApi.core.on.filterChanged(null, function () {
					if (filterTimeout) {
						$timeout.cancel(filterTimeout);
					}

					var filter = getStatementsGridFilter();
					console.log(filter);
					filterTimeout = $timeout(function () {
						getStatementList(filter, updateStatementsGridData);
					}, 500);
				});
			}

			function getStatementsGridFilter() {
				var result = {};

				self.statementsGridApi.grid.columns.forEach(function (column) {
					if (column.filters && column.filters[0].term) {
						result[column.name] = column.filters[0].term;
					};
				});

				return result;
			}

			function getStatementList(filter, callback) {
				console.log(callback);
				ajaxService.ajax('Home/SearchJournal', 'POST', filter, callback);
			}
		}

		return JournalStatementService;
	}
})();