var ENV = process.env;
var TARGET_PATH = ENV.FTP_SERVER_PATH;
var USERNAME = ENV.FTP_USERNAME;
var PASSWORD = ENV.FTP_PASSWORD;
var HOST = ENV.FTP_SERVER_HOST;
var PORT = ENV.FTP_SERVER_PORT || 21;
 
//Connect
var ftp = new EasyFTP();
var config = {
    host: ENV.FTP_SERVER_HOST,
    port: ENV.FTP_SERVER_PORT,
    username: ENV.FTP_USERNAME,
    password: ENV.FTP_PASSWORD,
    type : 'ftp'
};
ftp.connect(config);

ftp.upload("/dist/polyglot/**", ENV.FTP_SERVER_PATH, function(err){
    console.log("Done!");
    ftp.close();
});