const gulp = require('gulp');
const uglify = require('gulp-uglify');
const webpack = require('webpack-stream');
const del = require('del');
const dartSass = require('sass');
const gulpSass = require('gulp-sass')(dartSass);
const concat = require('gulp-concat');
const cleanCss = require('gulp-clean-css');
const babel = require('gulp-babel');

gulp.task("clean", async function () {
    await del(['js']);
});

gulp.task("editorjs", function () {
    return webpack({
            entry: './src/js/editor.js',
            output: {
                filename: 'editor.js'
            }
        })
        .pipe(uglify())
        .pipe(gulp.dest('js'));
});

gulp.task('watchEditorjs', function () {
    gulp.watch('./src/js/editor.js', gulp.series('editorjs'));
});

gulp.task("commonjs", function () {
    return gulp.src('./src/js/common.js')
        .pipe(babel())
        .pipe(uglify())
        .pipe(gulp.dest('js'));
});

gulp.task('sass', function () {
    return gulp.src('./src/css/*.scss')
        .pipe(gulpSass().on('error', gulpSass.logError))
        .pipe(concat('app.css'))
        .pipe(cleanCss())
        .pipe(gulp.dest('css'));
});

gulp.task('default', gulp.series('clean', 'editorjs', 'sass'));