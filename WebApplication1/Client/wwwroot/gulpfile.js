'use strict';

/*global require*/
/*global process*/

const gulp = require('gulp');
const sass = require('gulp-sass');

const dirs = {
    scss: {
        src: 'Styles',
        dest: 'css'
    }
};

const stylesTask = function stylesTask() {
    gulp.src(`${dirs.scss.src}/*.scss`)
        .pipe(sass().on('error', sass.logError))
        .pipe(gulp.dest(dirs.scss.dest));    
};

const watchTask = function watchTask() {
    
    gulp.watch(`${dirs.scss.src}/**/*.scss`, gulp.series(stylesTask));
};

const buildTask = function buildTask() {
    return new Promise(function (resolve) {
        gulp.task('styles')();
        resolve();
    });
};


gulp.task('styles', stylesTask);
gulp.task('watch', watchTask);
gulp.task('build', buildTask);
gulp.task('default', buildTask);