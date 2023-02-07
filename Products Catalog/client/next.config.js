/** @type {import('next').NextConfig} */
const nextConfig = {
    reactStrictMode: true,
    publicRuntimeConfig: {
        backendUrl: 'https://localhost:7208'
    },
    images: {
        domains: ['picsum.photos']
    }
};

module.exports = nextConfig;
