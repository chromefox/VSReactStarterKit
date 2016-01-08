/// <binding ProjectOpened='Hot' />
var path = require('path');
var webpack = require('webpack');

module.exports = {
    devServer: {
        contentBase: './build',
        host: 'localhost',
        port: 3000
    },
    entry: {
        main: [
          'webpack-dev-server/client?http://localhost:3000',
          'webpack/hot/only-dev-server',
          './app/index'
        ]
    },
    output: {
        path: path.join(__dirname, 'build'),
        filename: '[name].js'
    },
    resolve: {
        // Add `.ts` and `.tsx` as a resolvable extension.
        extensions: ['', '.webpack.js', '.web.js', '.ts', '.tsx', '.js', '.scss','.css']
    },
    module: {
        loaders: [
             {
                 test: /\.scss$/,
                 loaders: ["style", "css", "sass"],
                 exclude: /node_modules/,
             },
              // all files with a `.ts` or `.tsx` extension will be handled by `ts-loader`
            {
                test: /\.ts(x?)$/,
                loaders: ['react-hot', 'babel-loader', 'ts-loader'],
                exclude: /node_modules/,
            },
            {
                test: /\.js$/,
                loaders: ['react-hot', 'babel-loader'],
                exclude: /node_modules/,
                include: __dirname
            }, {
                test: /\.css?$/,
                loaders: ['style', 'raw'],
                include: __dirname
            }]
    }
};
