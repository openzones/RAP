(function () {
	'use strict';

	angular
		.module('statement', ['ui.grid', 'ui.grid.resizeColumns', 'ui.grid.selection', 'ui.grid.autoResize', 'ui.mask'])
		.component('datepickerPopup', datepickerPopup)
		.component('selectUi', selectUI)
		.component('fileUploader', fileUploader)
		.component('validator', validator);

	angular
		.module('common.view', ['statement']);
})();