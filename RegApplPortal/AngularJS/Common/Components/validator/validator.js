var validator = {
	templateUrl: 'AngularJS/Common/Components/validator/validator.html',
	bindings: {
		check: '=',
		hint: '@'
	},
	controller: function () {
		this.hint = this.hint || "данное поле необходимо заполнить";
	}
};