import { useState, useEffect, ReactNode } from 'react';
import { useRouter } from 'next/router';

const PUBLIC_PATHS = [
  '/signin',
  '/signup'
];

function checkUser() {
  const expiration = localStorage.getItem("expiration");
  if (!expiration) {
    return false;
  }
  const expirationDate = new Date(expiration);
  if (expirationDate < new Date()) {
    return false;
  }
  const token = localStorage.getItem("token");
  if (!token) {
    return false;
  }
  return true;
}

type Props = {
  children: JSX.Element;
};

export default function RouteGuard({ children }: Props) {
  const router = useRouter();
  const [authorized, setAuthorized] = useState(false);
  
  useEffect(() => {
    function authCheck(url: string) {
      const path = url.split('?')[0];
      if (!checkUser() && !PUBLIC_PATHS.includes(path)) {
        setAuthorized(false);
        router.push('/signin');
      } else {
        setAuthorized(true);
      }
    }

    authCheck(router.asPath);
    const hideContent = () => setAuthorized(false);
    router.events.on('routeChangeStart', hideContent);
    router.events.on('routeChangeComplete', authCheck);

    return () => {
      router.events.off('routeChangeStart', hideContent);
      router.events.off('routeChangeComplete', authCheck);
    };
  }, [router]);

  return (authorized ? children : <></>);
}