(function () {
	'use strict';

	angular
		.module('app', [
			'ui.select',
			'ui.uploader',
			'ui.bootstrap',
			'ngSanitize',
			'common.view',
			'common.convertDate',
			'common.ajaxService',
			'common.modalService',
			'common.directoryItem',
			'common.directoryUsersItem'
		]);
})();