var gulp = require('gulp');
var uglify = require('gulp-uglify');
const webpack = require('webpack-stream');

var paths = {
    scripts: {
        src: 'src/scripts/*.js',
        dest: 'js'
    }
};

function scripts() {
    return gulp.src(paths.scripts.src, { sourcemaps: true })
        .pipe(webpack())
        .pipe(uglify())
        .pipe(gulp.dest(paths.scripts.dest));
}

function watch() {
    gulp.watch(paths.scripts.src, scripts);
}

var build = gulp.series(scripts);

exports.scripts = scripts;
exports.watch = watch;

exports.default = build;