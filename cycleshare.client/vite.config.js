import {defineConfig} from 'vite';
import react from '@vitejs/plugin-react';

// https://vite.dev/config/
export default defineConfig({
    plugins: [react()],

    server: {
        proxy: {
            '/api': {
                target: 'https://eb1e-62-205-146-73.ngrok-free.app',
                changeOrigin: true,
                // rewrite: (path) => path.replace(/^\/api/, ''),

                configure: (proxy, options) => {
                    proxy.on('error', (err, _req, _res) => {
                        console.log('error', err);
                    });
                    proxy.on('proxyReq', (proxyReq, req, _res) => {
                        console.log('Request sent to target:', req.method, req.url);
                    });
                    proxy.on('proxyRes', (proxyRes, req, _res) => {
                        console.log('Response received from target:', proxyRes.statusCode, req.url);
                    });
                },
            },
        }
    },

    esbuild: {
        loader: 'jsx',
        include: /.*\.jsx?$/,
        exclude: []
    },
    optimizeDeps: {
        esbuildOptions: {
            loader: {
                '.js': 'jsx',
            },
        },
    },
});
