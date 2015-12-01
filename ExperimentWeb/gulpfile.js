/// <binding AfterBuild='default' ProjectOpened='watch' />
// Include gulp
var gulp = require('gulp');

// Include Our Plugins
var jshint = require('gulp-jshint');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var rename = require('gulp-rename');
var react = require('gulp-react');
var htmlreplace = require('gulp-html-replace');
var eslint = require('gulp-eslint');

var path = {
    HTML: 'src/index.html',
    ALL: ['src/js/*.js', 'src/js/**/*.js', 'src/index.html'],
    JS: ['main.js'],
    MINIFIED_OUT: 'build.min.js',
    DEST_SRC: 'dist/src',
    DEST_BUILD: 'dist/build',
    DEST: 'dist'
};

gulp.task('transform', function () {
    gulp.src(path.JS)
      .pipe(react())
      .pipe(gulp.dest(path.DEST_SRC));
});

// Lint Task
gulp.task('lint', function () {
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
gulp.task('scripts', function () {
    return gulp.src('Scripts/main.js')
        .pipe(concat('all.js'))
        .pipe(gulp.dest('dist'))
        .pipe(rename('all.min.js'))
        .pipe(uglify())
        .pipe(gulp.dest('dist'));
});

// Watch Files For Changes
gulp.task('watch', function () {
    gulp.watch('Scripts/main.js', ['lint']);
});

// Default Task
gulp.task('default', ['watch']);