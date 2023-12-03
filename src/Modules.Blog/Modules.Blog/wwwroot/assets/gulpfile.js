var gulp = require('gulp');
var uglify = require('gulp-uglify');
const webpack = require('webpack-stream');
const del = require('del');

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

gulp.task("watch", function () {
    gulp.watch("src/js/editor.js", gulp.series('editorjs'));
});

gulp.task('default', gulp.series('clean', 'editorjs'));