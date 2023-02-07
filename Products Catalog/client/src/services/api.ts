import axios from 'axios';
import https from 'https';
import getConfig from 'next/config';

const { publicRuntimeConfig } = getConfig();
const api = axios.create({
  baseURL: publicRuntimeConfig.backendUrl,
  httpsAgent: new https.Agent({
    rejectUnauthorized: false,
  }),
});

api.interceptors.request.use((req) => {
  const token = localStorage.getItem("token");
  if (!!token) {
    req.headers.Authorization = `Bearer ${token}`;
  }
  return req;
})

export default api;
