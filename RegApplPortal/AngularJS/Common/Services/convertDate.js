(function () {
	'use strict';

	angular
		.module('common.convertDate', [])
		.factory('convertDate', ['$parse', convertDate]);

	function convertDate($parse) {

		return {
			convert: convertDates
		};

		function convertDates(obj) {
			for (var key in obj) {
				if (!obj.hasOwnProperty(key)) continue;

				var value = obj[key];
				var typeofValue = typeof (value);

				if (typeofValue === 'object') {
					// If it is an object, check within the object for dates.
					convertDates(value);
				} else if (typeofValue === 'string') {
					if (/^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}/.test(value)) {
						obj[key] = new Date(value);
					}
				}
			}
			return obj;
		}
	}
})();