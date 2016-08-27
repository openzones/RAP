(function () {
	'use stict';

	angular
		.module('common.directoryUsersItem', [])
		.filter('directoryUsersItem', directoryUsersItem);

	function directoryUsersItem() {
		return filter;

		function filter(input, directory, propertyName) {
			
			var directoryItem = _.find(directory, function (directoryItem) {
				return directoryItem.id == input;
			});

			var customDirectory = {};
			if (directoryItem != null)
			{
				customDirectory = {
					id: directoryItem.id,
					label: (directoryItem.lastname + " " + directoryItem.firstname.substring(0, 1).toUpperCase() + ". " + (directoryItem.secondname != null ? directoryItem.secondname.substring(0, 1).toUpperCase() + ". " : null))
				};
			}

			var result = (!propertyName) ?
				customDirectory :
				_.propertyOf(customDirectory)(propertyName);

			return result;
		};
	}
})();