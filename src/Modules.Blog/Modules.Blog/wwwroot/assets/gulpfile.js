const gulp = require('gulp');
const uglify = require('gulp-uglify');
const del = require('del');
const dartSass = require('sass');
const gulpSass = require('gulp-sass')(dartSass);
const concat = require('gulp-concat');
const cleanCss = require('gulp-clean-css');

const plumber = require('gulp-plumber');
const buffer = require('vinyl-buffer');
const source = require('vinyl-source-stream');

const rollupStream = require('@rollup/stream');
const commonjs = require('@rollup/plugin-commonjs');
const { babel } = require('@rollup/plugin-babel');
const { nodeResolve } = require('@rollup/plugin-node-resolve');

gulp.task("clean", async function () {
    await del(['js']);
});

gulp.task("editorjs", function () {
    let outputOptions = {
        sourcemap: true,
        format: 'es'
    }
    

    let stream = rollupStream({
        input: './src/js/editor.js',
        output: outputOptions,
        plugins: [
            babel({
                exclude: 'node_modules/**',
                presets: ['@babel/preset-env'],
                babelHelpers: 'bundled',
            }),
            nodeResolve({
                browser: true,
                preferBuiltins: false,
            }),
            commonjs({
                include: ['node_modules/**'],
                exclude: [],
                sourceMap: true,
            }),
        ],
    })

    stream = stream.pipe(source('editor.js'))
        .pipe(buffer())
        .pipe(plumber())
        .pipe(uglify());

    return stream.pipe(gulp.dest('js'));
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

gulp.task('watchEditorjs', function () {
    gulp.watch('./src/js/editor.js', gulp.series('editorjs'));
});

gulp.task('default', gulp.series('clean', 'editorjs', 'sass'));