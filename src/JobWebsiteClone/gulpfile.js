///// <binding BeforeBuild='clean' AfterBuild='sass, min' Clean='clean' ProjectOpened='watch-sass, watch-scripts' />
//"use strict";

//var gulp = require("gulp"),
//  rimraf = require("rimraf"),
//  concat = require("gulp-concat"),
//  cssmin = require("gulp-cssmin"),
//  uglify = require("gulp-uglify"),
//    sass = require("gulp-sass"),
//    gutil = require('gulp-util');

var paths = {
    webroot: "./wwwroot/"
};

paths.js = paths.webroot + "lib/**/*.js";
paths.minJs = paths.webroot + "lib/**/*.min.js";
paths.css = paths.webroot + "lib/**/*.css";
paths.minCss = paths.webroot + "lib/**/*.min.css";
paths.concatJsDest = paths.webroot + "js/site.min.js";
paths.concatCssDest = paths.webroot + "css/site.min.css";
paths.scss = "Styles/scss/**/*.scss";
paths.scripts = "Scripts/**/*.js";

//gulp.task("clean:js", function (cb) {
//    rimraf(paths.concatJsDest, cb);
//});

//gulp.task("clean:css", function (cb) {
//    rimraf(paths.concatCssDest, cb);
//});

//gulp.task("sass", function () {
//    return gulp.src(paths.scss)
//      .pipe(sass())
//      .pipe(gulp.dest('wwwroot/css'));
//});

//gulp.task("scripts", function () {
//    return gulp.src(paths.scripts)
//        .pipe(concat(paths.webroot + "js/scripts.js"))
//    .pipe(uglify())
//    .pipe(gulp.dest("."));
//})

//gulp.task('watch-sass', function () {
//    gulp.watch(paths.scss, ['sass']);
//})

//gulp.task('watch-scripts', function () {
//    gulp.watch(paths.scripts, ['scripts']);
//})

//gulp.task("clean", ["clean:js", "clean:css"]);

//gulp.task("min:js", function () {
//    return gulp.src([paths.js, "!" + paths.minJs, "!" + paths.webroot + "lib/city-autocomplete/*.js"], { base: "." })
//      .pipe(concat(paths.concatJsDest))
//      .pipe(uglify().on('error', gutil.log))
//      .pipe(gulp.dest("."));
//});

//gulp.task("min:css", function () {
//    return gulp.src([paths.css, "!" + paths.minCss])
//      .pipe(concat(paths.concatCssDest))
//      .pipe(cssmin())
//      .pipe(gulp.dest("."));
//});

//gulp.task("min", ["min:js", "min:css"]);

var gulp = require('gulp');
var browserify = require('browserify');
var source = require('vinyl-source-stream');
//var jshint = require('gulp-jshint');
var uglify = require('gulp-uglify');
var utilities = require('gulp-util');
var concat = require('gulp-concat');
//var del = require('del');
var buildProduction = utilities.env.production;
//var browserSync = require('browser-sync').create();
var lib = require('bower-files')({
    "overrides": {
        "bootstrap": {
            "main": [
              "less/bootstrap.less",
              "dist/css/bootstrap.css",
              "dist/js/bootstrap.js"
            ]
        }
    }
});

gulp.task('jsBrowserify', ['concatInterface'], function () {
    return browserify({ entries: ['./tmp/everyConcat.js'] })
    .bundle()
    .pipe(source('app.js'))
    .pipe(gulp.dest('./build/js'));
});

gulp.task('bower', ['bowerJS', 'bowerCSS']);

gulp.task('bowerJS', function () {
    return gulp.src(lib.ext('js').files)
      .pipe(concat('vendor.min.js'))
      .pipe(uglify())
      .pipe(gulp.dest('./build/js'));
});

gulp.task('bowerCSS', function () {
    return gulp.src(lib.ext('css').files)
      .pipe(concat('vendor.min.css'))
      .pipe(gulp.dest('./build/css'));
});

gulp.task('concatInterface', function () {
    return gulp.src([paths.scripts, paths.webroot + "lib/city-autocomplete/jquery.city-autocomplete.js"])
      .pipe(concat('everyConcat.js'))
      .pipe(gulp.dest('./tmp'));
});

gulp.task("minifyScrpts", ["jsBrowserify"], function () {
    return gulp.src("./build/js/app.js")
    .pipe(uglify())
    .pipe(gulp.dest("./build/js"));
});

//gulp.task("clean", function () {
//    return del(['build', 'tmp']);
//});

gulp.task('jshint', function () {
    return gulp.src(['js/*.js'])
  .pipe(jshint())
  .pipe(jshint.reporter('default'));
});

gulp.task("build", function () {
    if (buildProduction) {
        gulp.start('minifyScripts');
    } else {
        gulp.start('jsBrowserify');
    }
    gulp.start('bower');
});

gulp.task('serve', function () {
    browserSync.init({
        server: {
            baseDir: "./",
            index: "index.html"
        }
    });

    gulp.watch(['js/*.js'], ['jsBuild']);
    gulp.watch(['bower.json'], ['bowerBuild']);
    gulp.watch(['*.html'], ['htmlBuild']);
});


gulp.task('jsBuild', ['jsBrowserify', 'jshint'], function () {
    browserSync.reload();
});

gulp.task('bowerBuild', ['bower'], function () {
    browserSync.reload();
});

gulp.task('htmlBuild', function () {
    browserSync.reload();
});