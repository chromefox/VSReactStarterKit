/// <binding ProjectOpened='Hot' />
var path = require('path');
var webpack = require('webpack');
var HtmlWebpackPlugin = require('html-webpack-plugin');
var pkg = require('./package.json');

// bundle dependencies in separate vendor bundle
var vendorPackages = Object.keys(pkg.dependencies).filter(function (el) {
    return el.indexOf('font') === -1; // exclude font packages from vendor bundle
});

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
        ],
        vendor: vendorPackages
    },
    output: {
        path: path.join(__dirname, 'build'),
        filename: '[name].js'
    },
    plugins: [
      new webpack.DefinePlugin({
          __API_URL__: JSON.stringify(process.env.API_URL || '//localhost:51407')
      }),
      new webpack.optimize.CommonsChunkPlugin({
          name: 'vendor',
          filename: 'vendor.js',
          minChunks: Infinity
      }),
      new HtmlWebpackPlugin({
          template: 'index.html'
      })
    ],
    resolveLoader: {
        'fallback': path.join(__dirname, 'node_modules')
    },
    resolve: {
        // Add `.ts` and `.tsx` as a resolvable extension.
        extensions: ['', '.webpack.js', '.web.js', '.ts', '.tsx', '.js']
    },
    module: {
        loaders: [
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
