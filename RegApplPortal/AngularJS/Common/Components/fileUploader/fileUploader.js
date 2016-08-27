var fileUploader = {
	templateUrl: 'AngularJS/Common/Components/fileUploader/fileUploader.html',
	bindings: {
		requestModel: '=',
		concurrency: '=',
		isDisabled: '=ngDisabled',
		filesCount: '=',
		hideButtons: '=',
		url: '@',
		multiple: '=',
		extensions: '@'
	},
	controller: ['$rootScope', '$timeout', '$log', '$document', 'uiUploader', function ($rootScope, $timeout, $log, $document, uiUploader) {
		var self = this;

		$document.ready(function () {
			self.inputElement = angular.element(document.querySelector('#input-element'));
			self.inputElement.on('change', function (event) {
				self.change(event.target);
			});
		});

		self.files = [];

		self.initialize = initialize;
		self.remove = remove;
		self.clean = clean;
		self.upload = upload;
		self.change = change;
		self.select = select;

		function initialize() {
			uiUploader.removeAll();
			self.filesCount = 0;
		}

		function remove(file) {
			uiUploader.removeFile(file);
			self.filesCount -= 1;
		}

		function clean() {
			uiUploader.removeAll();
			self.filesCount = 0;
		}

		function upload() {
			uiUploader.startUpload({
				url: self.url,
				concurrency: self.concurrency || 1,
				data: self.requestModel,
				onCompleted: function (file, response) {
					var _response = angular.fromJson(response);

					$timeout(function () {
						if (_response.HasErrors) {
							var message = 'Ошибка при загрузке файла: ' + file.name + ' .';

							$rootScope.addAlert(message, 'danger');
							$log.error(_response.Message, file.name, _response);

							return;
						}

						$rootScope.addAlert('Файл успешно загружен: ' + file.name + ' .', 'success');
					});
				},
				onCompletedAll: function (files) {
					$timeout(function () {
						clean();
					});
				}
			});
		}

		function change(target) {
			$timeout(function () {
				var files = target.files;
				uiUploader.addFiles(files);
				self.files = uiUploader.getFiles();
				self.filesCount = self.files.length;
				target.value = null;
			});
		}

		function select() {
			self.inputElement.trigger('click');
		}
	}]
};