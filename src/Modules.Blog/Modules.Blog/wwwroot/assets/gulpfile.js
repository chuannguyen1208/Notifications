const rollupStream = require("@rollup/stream");
const { nodeResolve } = require("@rollup/plugin-node-resolve");
const commonjs = require("@rollup/plugin-commonjs");
const source = require("vinyl-source-stream");
const buffer = require("vinyl-buffer");
const gulp = require("gulp");
const uglify = require("gulp-uglify");
const gulpSass = require("gulp-sass")(require("sass"));
const cleanCss = require("gulp-clean-css");
const concat = require("gulp-concat");
const del = require('del');

async function clean() {
    await del(['js', 'css']);
}

function js(filename) {
  const options = {
    input: `src/js/${filename}`,
    output: { format: "es", sourcemap: true },
    plugins: [nodeResolve({ browser: true }), commonjs()],
  };
  return rollupStream(options)
    .pipe(source(filename))
    .pipe(buffer())
    .pipe(uglify())
    .pipe(gulp.dest("js"));
}

function css() {
  return gulp
    .src("./src/css/*.scss")
    .pipe(gulpSass())
    .pipe(concat("app.css"))
    .pipe(cleanCss())
    .pipe(gulp.dest("css"));
}

const editor = () => js('editor.js');
const common = () => js('common.js');
const build = gulp.series(clean, css, gulp.parallel(editor, common));

exports.editor = editor;
exports.common = common;
exports.css = css;
exports.clean = clean;
exports.build = build;
exports.default = build;
