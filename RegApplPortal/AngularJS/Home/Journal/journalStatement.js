(function () {
	'use strict';

	angular
		.module('journalStatement', ['ui.grid', 'ui.grid.resizeColumns', 'ui.grid.selection'])

	angular
		.module('common.view', ['journalStatement']);
})();