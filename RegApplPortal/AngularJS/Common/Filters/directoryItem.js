(function () {
	'use stict';

	angular
		.module('common.directoryItem', [])
		.filter('directoryItem', directoryItem);



	function directoryItem() {
		return filter;

		function filter(input, directory, propertyName) {
			
			var directoryItem = _.find(directory, function (directoryItem) {
				return directoryItem.id == input;
			});

			var result = (!propertyName) ?
				directoryItem :
				_.propertyOf(directoryItem)(propertyName);

			return result;
		};
	}
})();