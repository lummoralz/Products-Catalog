import { useState, useEffect } from 'react';
import { useRouter } from 'next/router';
import api from '../services/api';
import Container from 'react-bootstrap/Container';
import Alert from 'react-bootstrap/Alert';
import ProductItem from '@/components/product_item';
import AppNavbar from '@/components/app_navbar';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

const EmptyCatalog = () => (<Alert variant='info'>There are no available products!</Alert>);

const CatalogView = ({ products }: any) => (<>
  <h2>All products ({products.length})</h2>
  <Row>
    {products.map((p: any) => (<Col key={p.id} md={4}><ProductItem product={p} /></Col>))}
  </Row>
</>);

export default function IndexPage() {
  const router = useRouter();
  const [products, setProducts] = useState([]);

  useEffect(() => {
    let mounted = true;
    async function getProducts() {
      try {
        const response = await api.get('/api/Products');
        setProducts(response.data);
      } catch {
        router.push('/signin');
      }
    }
    if (mounted) {
      getProducts();
    }
    return () => { mounted = false; };
  }, [router]);

  return (<>
    <AppNavbar />
    <Container className='mt-5'>
      {products ? <CatalogView products={products} /> : <EmptyCatalog />}
    </Container>
  </>);
};