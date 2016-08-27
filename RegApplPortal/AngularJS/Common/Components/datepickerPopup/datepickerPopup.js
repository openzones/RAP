var datepickerPopup = {
	templateUrl: 'AngularJS/Common/Components/datepickerPopup/datepickerPopup.html',
	bindings: {
		model: '=',
		minDate: '=',
		maxDate: '=',
		disabledState: '=',
	},
	controller: function () {
		var self = this;

		if (!!this.model) {
			this.model = new Date(this.model);
		}
		this.popupOpened = false;
		this.dateOptions = {
			formatYear: 'yy',
			maxDate: this.maxDate,
			minDate: this.minDate,
			initDate: new Date(),
		}
	}
};