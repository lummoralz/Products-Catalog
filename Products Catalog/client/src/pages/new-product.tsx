import { useState } from 'react';
import { useRouter } from 'next/router';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Card from 'react-bootstrap/Card';
import Container from 'react-bootstrap/Container';
import Alert from 'react-bootstrap/Alert';
import AppNavbar from '@/components/app_navbar';
import api from '../services/api';

export default function NewProductPage() {
  const router = useRouter();

  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [price, setPrice] = useState(0);
  const [amount, setAmount] = useState(0);

  const [error, setError] = useState('');
  const [nameError, setNameError] = useState('');

  async function createProduct() {
    setError('');
    setNameError('');

    try {
      const product = {
        Name: name,
        Description: description,
        UnitPrice: price,
        Amount: amount
      };
      await api.post('/api/Products', product);
      router.push('/');
    } catch (ex: any) {
      const { errors, error } = ex.response.data;
      if (!!errors) {
        setNameError(errors?.Name?.[0]);
      } else if (error) {
        setError(error);
      } else {
        setError(ex.response.data[0].description);
      }
      console.error(ex);
    }
  }

  return (<>
    <AppNavbar />
    <Container className='d-flex align-items-center justify-content-center h-100'>
      <Card className="w-50">
        <Card.Header><h3 className='mb-0'>New product</h3></Card.Header>
        <Card.Body>
          {!!error && (<Alert variant='danger'>{error}</Alert>)}

          <Form.Group className="mb-3" controlId="name">
            <Form.Label>Product name</Form.Label>
            <Form.Control type="text"
              placeholder="Enter name"
              value={name}
              onChange={(e) => setName(e.target.value)}
              isInvalid={!!nameError}
              required
            />
            <Form.Control.Feedback type="invalid">{nameError}</Form.Control.Feedback>
          </Form.Group>

          <Form.Group className="mb-3" controlId="description">
            <Form.Label>Description</Form.Label>
            <Form.Control as="textarea"
              value={description}
              onChange={(e) => setDescription(e.target.value)}
              rows={3}
            />
          </Form.Group>

          <Form.Group className='mb-3' controlId='price'>
            <Form.Label>Unit Price</Form.Label>
            <Form.Control type="number"
              value={price}
              onChange={(e) => setPrice(Number(e.target.value))}
            />
          </Form.Group>

          <Form.Group className='mb-3' controlId='amount'>
            <Form.Label>Amount</Form.Label>
            <Form.Control type="number"
              value={amount}
              onChange={(e) => setAmount(Number(e.target.value))}
            />
          </Form.Group>

          <Button variant="primary" type="button" onClick={createProduct}>
            Create
          </Button>
        </Card.Body>
      </Card>
    </Container>
  </>);
}