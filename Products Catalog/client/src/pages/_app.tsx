import '@/styles/globals.css';
import type { AppProps } from 'next/app';
import 'bootstrap/dist/css/bootstrap.min.css';
import RouteGuard from '@/components/route_guard';

export default function App({ Component, pageProps }: AppProps) {
  return (<RouteGuard>
    <Component {...pageProps} />
  </RouteGuard>);
};
