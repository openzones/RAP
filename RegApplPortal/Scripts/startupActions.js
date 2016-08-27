(function () {
	'use strict';

	// добавление endsWith в IE
	if (typeof String.prototype.endsWith !== 'function') {
		String.prototype.endsWith = function (suffix) {
			return this.indexOf(suffix, this.length - suffix.length) !== -1;
		};
	}
})();