module.exports = {
    devServer: {
        proxy: {
            '/Client': {
                target: 'http://localhost:5000/',
                changeOrigin: true
            }
        }
    }
}