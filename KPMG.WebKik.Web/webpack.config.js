/// <binding />
var isDevBuild = process.argv.indexOf('--env.prod') < 0;
var path = require('path');
var webpack = require('webpack');
var nodeExternals = require('webpack-node-externals');
var merge = require('webpack-merge');
var allFilenamesExceptJavaScript = /\.(?!js(\?|$))([^.]+(\?|$))/;
var CopyWebpackPlugin = require('copy-webpack-plugin');

// Configuration in common to both client-side and server-side bundles
var sharedConfig = {
    resolve: { extensions: [ '', '.ts', '.js' ] },
    output: {
        filename: '[name].js',
        publicPath: '/dist/' // Webpack dev middleware, if enabled, handles requests for this URL prefix
    },
    module: {
        loaders: [
            { test: /\.ts$/, include: /ClientApp/, loader: 'ts', query: { silent: true } },
            { test: /\.html$/, loader: 'raw' },
            { test: /\.css$/, loader: 'to-string!css' },
            { test: /\.scss$/, loaders: ['raw-loader','sass-loader'] },
            { test: /\.(png|jpg|jpeg|gif|svg)$/, loader: 'url', query: { limit: 25000 } }
        ]
    }
};

// Configuration for client-side bundle suitable for running in browsers
var clientBundleConfig = merge(sharedConfig, {
    entry: { 'main-client': './ClientApp/boot.ts' },
    output: { path: path.join(__dirname, './wwwroot/dist') },
    plugins: [
        new webpack.DllReferencePlugin({
            context: __dirname,
            manifest: require('./wwwroot/dist/vendor-manifest.json')
        }),
        new CopyWebpackPlugin([
            {
                from: './ClientApp/**/*.png'
            }
        ]),
        new CopyWebpackPlugin([
        {
            from: './ClientContent/images',
            to: 'images'
        }]),
		new CopyWebpackPlugin([
        {
            from: './ClientContent/styles/dist',
            to: 'styles'
        }])
    ].concat(isDevBuild ? [
        // Plugins that apply in development builds only
        new webpack.SourceMapDevToolPlugin({ moduleFilenameTemplate: '../../[resourcePath]' }) // Compiled output is at './wwwroot/dist/', but sources are relative to './'
    ] : [
        // Plugins that apply in production builds only
        new webpack.optimize.OccurenceOrderPlugin(),
        new webpack.optimize.UglifyJsPlugin()
    ])
});

module.exports = [clientBundleConfig];
