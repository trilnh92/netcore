const path = require('path');
const HtmlWebpackPlugin = require("html-webpack-plugin");
const htmlWebpackPlugin = new HtmlWebpackPlugin({
    template: path.join(__dirname, "index.html"),
    filename: "./index.html"
});

module.exports = {
    entry: {
       index: path.join(__dirname, "src/index.jsx")
    },
    output: {
        path: path.join(__dirname, "dist"),
        filename: "bundle.js",
        chunkFilename:'[id][hash].js',
        publicPath:'/'
    },
    module: {
        rules: [
            {
                test: /\.(js|jsx)$/,
                use: "babel-loader",
                exclude: /node_modules/
            },
            {
                test: /\.css$/,
                use: ["style-loader", "css-loader"]
            }
        ]
    },
    plugins: [htmlWebpackPlugin],
    resolve: {
        extensions: [".js", ".jsx"]
    },
    devServer: {
        port: 5001
    }
};