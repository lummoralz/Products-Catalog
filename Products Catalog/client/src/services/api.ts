import axios from 'axios';
import https from 'https';
import getConfig from 'next/config';

let headers;
if (typeof window !== 'undefined') {
  headers = {
    get Authorization() {
      return `Bearer ${localStorage.getItem("token")}`;
    }
  };
}

const { publicRuntimeConfig } = getConfig();
const api = axios.create({
  baseURL: publicRuntimeConfig.backendUrl,
  httpsAgent: new https.Agent({
    rejectUnauthorized: false,
  }),
  headers,
});

export default api;
