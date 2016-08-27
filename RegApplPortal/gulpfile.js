/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var bower = require('gulp-bower');
var concat = require('gulp-concat');
var karma = require('gulp-karma');
var clean = require('gulp-clean');
var replace = require('gulp-replace');

gulp.task('default', function () {
	// place code for your default task here
});

gulp.task('bower', function () {
	return bower('./bower_components');
});

gulp.task('clean', function () {
	return gulp
		.src([
			'Scripts/angular',
			'Scripts/angular-bootstrap',
			'Scripts/angular-ui-grid',
			'Scripts/angular-ui-mask',
			'Scripts/angular-ui-select',
			'Scripts/angular-ui-uploader',
			'Scripts/angular-ui-validate',
			'Scripts/angular-sanitize',
			'Scripts/bootstrap',
			'Scripts/underscore',
			'Scripts/moment',
			'Scripts/jquery',
			'Content/angular-ui-grid',
			'Content/angular-ui-select',
			'Content/bootstrap'
		], {
			read: false
		})
        .pipe(clean());
});


gulp.task('copyLibs', ['bower'], function () {

	// angular
	gulp
		.src([
			'bower_components/angular/angular.js',
			'bower_components/angular/angular.min.js',
			'bower_components/angular/angular.min.js.map'
		])
        .pipe(gulp.dest('Scripts/angular'));

	// angular-sanitize
	gulp
		.src([
			'bower_components/angular-sanitize/angular-sanitize.js',
			'bower_components/angular-sanitize/angular-sanitize.min.js',
			'bower_components/angular-sanitize/angular-sanitize.min.js.map'
		])
	.pipe(gulp.dest('Scripts/angular-sanitize'));

	// angular-i18n
	gulp
	.src(['bower_components/angular-i18n/angular-locale_ru-ru.js'])
	.pipe(gulp.dest('Scripts/angular-i18n'));

	// jquery
	gulp
		.src(['bower_components/jquery/dist/*'])
        .pipe(gulp.dest('Scripts/jquery'));

	// jquery-ui
	gulp
		.src(['bower_components/jquery-ui/jquery-ui.js',
			'bower_components/jquery-ui/jquery-ui.min.js'
		])
		.pipe(gulp.dest('Scripts/jquery-ui'));

	// jquery-migrate
	gulp
		.src(['bower_components/jquery-migrate/jquery-migrate.js',
			'bower_components/jquery-migrate/jquery-migrate.min.js'
		])
		.pipe(gulp.dest('Scripts/jquery-migrate'));

	// underscore
	gulp
		.src([
			'bower_components/underscore/underscore.js',
			'bower_components/underscore/underscore-min.js',
			'bower_components/underscore/underscore-min.map'
		])
		.pipe(gulp.dest('Scripts/underscore'));

	// moment
	gulp
		.src(['bower_components/moment/moment.js'])
        .pipe(gulp.dest('Scripts/moment'));

	// angular-ui-bootstrap
	gulp
		.src([
			'bower_components/angular-bootstrap/ui-bootstrap-tpls.js',
			'bower_components/angular-bootstrap/ui-bootstrap-tpls.min.js',
			'bower_components/angular-bootstrap/ui-bootstrap.js',
			'bower_components/angular-bootstrap/ui-bootstrap.min.js'
		])
		.pipe(gulp.dest('Scripts/angular-bootstrap'));

	// angular-ui-grid
	gulp
		.src([
			'bower_components/angular-ui-grid/ui-grid.js',
			'bower_components/angular-ui-grid/ui-grid.min.js'
		])
		.pipe(gulp.dest('Scripts/angular-ui-grid'));

	gulp
		.src([
			'bower_components/angular-ui-grid/ui-grid.css',
			'bower_components/angular-ui-grid/ui-grid.min.css',
			'bower_components/angular-ui-grid/ui-grid.eot',
			'bower_components/angular-ui-grid/ui-grid.svg',
			'bower_components/angular-ui-grid/ui-grid.ttf',
			'bower_components/angular-ui-grid/ui-grid.woff'
		])
        .pipe(gulp.dest('Content/angular-ui-grid'));

	// angular-ui-mask
	gulp
		.src(['bower_components/angular-ui-mask/dist/*'])
		.pipe(gulp.dest('Scripts/angular-ui-mask'));

	// angular-ui-select
	gulp
		.src([
			'bower_components/angular-ui-select/dist/select.js',
			'bower_components/angular-ui-select/dist/select.min.js',
			'bower_components/angular-ui-select/dist/select.min.js.map'
		])
		.pipe(gulp.dest('Scripts/angular-ui-select'));

	gulp
		.src([
			'bower_components/angular-ui-select/dist/select.css',
			'bower_components/angular-ui-select/dist/select.min.css',
			'bower_components/angular-ui-select/dist/select.min.css.map'
		])
        .pipe(gulp.dest('Content/angular-ui-select'));

	// angular-ui-uploader
	gulp
		.src(['bower_components/angular-ui-uploader/dist/*'])
		.pipe(gulp.dest('Scripts/angular-ui-uploader'));

	// angular-ui-validate
	gulp
		.src([
			'bower_components/angular-ui-validate/dist/*'])
		.pipe(gulp.dest('Scripts/angular-ui-validate'));

	// bootstrap
	gulp
		.src([
			'bower_components/bootstrap/dist/js/bootstrap.js',
			'bower_components/bootstrap/dist/js/bootstrap.min.js'
		])
		.pipe(gulp.dest('Scripts/bootstrap'));

	gulp
		.src(['bower_components/bootstrap/dist/css/*'])
        .pipe(gulp.dest('Content/bootstrap'));

	gulp
		.src(['bower_components/bootstrap/fonts/*'])
        .pipe(gulp.dest('Content/fonts'));

});