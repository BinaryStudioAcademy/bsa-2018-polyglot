const ftp = require('vinyl-ftp');
const gutil = require('gulp-util');
const minimist = require('minimist');
const args = minimist(process.argv.slice(2));

gulp.task('deploy', () => {
    const remotePath = '/site/wwwroot/';
    const conn = ftp.create({
        host: 'waws-prod-am2-163.ftp.azurewebsites.windows.net',
        user: args.user,
        password: args.password,
        log: gutil.log
    });

    gulp.src('frontend/dist/polyglot/**')
        .pipe(conn.newer(remotePath))
        .pipe(conn.dest(remotePath));
});