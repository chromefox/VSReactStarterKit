/// <binding ProjectOpened='watch' />
// Include gulp
var gulp = require('gulp');
var browserify = require('browserify');
var watchify = require('watchify');

// Include Our Plugins
var assign = require('lodash.assign');
var jshint = require('gulp-jshint');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var rename = require('gulp-rename');
var react = require('gulp-react');
var htmlreplace = require('gulp-html-replace');
var eslint = require('gulp-eslint');
var mainBowerFiles = require('gulp-main-bower-files');
var source = require('vinyl-source-stream');

// To merge two different streams
var es = require('event-stream');
var merge = require('merge-stream')();

var path = {
    HTML: 'src/index.html',
    ALL: ['src/js/*.js', 'src/js/**/*.js'],
    JS: ['main.js'],
    MINIFIED_OUT: 'build.min.js',
    DEST_SRC: 'dist/src',
    DEST_BUILD: 'dist/build',
    DEST: 'dist',
    BOWER_PATH: 'bower_components/*.js'
};

// add custom browserify options here
var customOpts = {
    entries: ['./npm-dependency.js']
};
var opts = assign({}, watchify.args, customOpts);
var b = watchify(browserify(opts));

gulp.task('transform', function () {
    gulp.src(path.JS)
      .pipe(react())
      .pipe(gulp.dest(path.DEST_SRC));
});

// Lint Task
gulp.task('eslint', function () {
    return gulp.src('main.js')
        .pipe(eslint())
        // eslint.format() outputs the lint results to the console.
        // Alternatively use eslint.formatEach() (see Docs).
        .pipe(eslint.format())
        // To have the process exit with an error code (1) on
        // lint error, return the stream and pipe to failAfterError last.
        .pipe(eslint.failAfterError());
});

// Concatenate & Minify JS
gulp.task('devBuild', function () {
    return gulp.src(path.ALL)
        .pipe(concat("main.js"))
        .pipe(gulp.dest('dist/js'));
});

// build js.
gulp.task('scripts', function () {
    return gulp.src('Scripts/main.js')
        .pipe(concat('all.js'))
        .pipe(gulp.dest('dist'))
        .pipe(rename('all.min.js'))
        .pipe(uglify())
        .pipe(gulp.dest('dist'));
});

// building browser js from npm modules is recommended in this article https://gofore.com/ohjelmistokehitys/stop-using-bower/
// build js from npm components
gulp.task('npmComponents', function () {
    buildNpmStream();
});

// watch files for changes
gulp.task('watch', function () {
    gulp.watch('Scripts/main.js', ['eslint']);
    gulp.watch(path.ALL, ['devBuild']);
});

gulp.task('typescript-example', function () {
    return browserifyTsExampleOutput();
});

gulp.task('typescript-example-style', function () {
    return buildTsExampleCss();
});

function buildNpmStream() {
    // demonstrates how to build module files from a defined npm module dependency file.
    return b.bundle() // ??
        .pipe(source('dependencies.js')) // rename the file to dependencies.js
        .pipe(gulp.dest('dist/js')); // output the stream to destination path to dump the bundle.js
}

function buildNpmCssStream() {
    // demonstrates how to build module files from a defined npm module dependency file.
    b.transform('browserify-css', { global: true });
    return b.bundle() // ??
        .pipe(source('dependencies.css')) // rename the file to dependencies.js
        .pipe(gulp.dest('dist/css')); // output the stream to destination path to dump the bundle.js
}

/*
 * The function below is to help browserify the js content made for typescript example
 * 1) Get all js on ts-example
 * 2) Run browserify
 */

function browserifyTsExampleOutput() {
    var tsPath = "src/js/ts-example/main-ts-example.js";
    return browserify(tsPath).bundle()
        // vinyl-source-stream makes the bundle compatible with gulp
        .pipe(source('typescript-example.js')) // Desired filename
        // Output the file
        .pipe(gulp.dest('dist/js'));
}

function buildTsExampleCss() {
    var tsPath = "./node_modules/datatables.net-bs/css/dataTables.bootstrap.css";
    return gulp.src(tsPath)
        // vinyl-source-stream makes the bundle compatible with gulp
        .pipe(rename('typescript-example.css')) // Desired filename
        // Output the file
        .pipe(gulp.dest('dist/css'));
}