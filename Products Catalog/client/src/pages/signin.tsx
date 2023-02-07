import { useState } from 'react';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Card from 'react-bootstrap/Card';
import Container from 'react-bootstrap/Container';
import Alert from 'react-bootstrap/Alert';
import { useRouter } from 'next/router';
import Link from 'next/link';
import api from '../services/api';

export default function Home() {
  const router = useRouter();

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const [emailError, setEmailError] = useState('');
  const [passError, setPassError] = useState('');
  const [error, setError] = useState('');

  async function signIn() {
    setError('');
    setEmailError('');
    setPassError('');

    try {
      const result = await api.post('/api/Users/signin', { Email: email, Password: password });

      //I know I should not store JWT tokens in the localStorage, but for simplicity sake I will do it anyways!
      const { token, expiration } = result.data;
      localStorage.setItem("token", token);
      localStorage.setItem("expiration", expiration);
      router.push('/');
    } catch (ex: any) {
      const { errors, error } = ex.response.data;
      if (!!errors) {
        setEmailError(errors?.Email?.[0]);
        setPassError(errors?.Password?.[0]);
      } else if (error) {
        setError(error);
      } else {
        setError(ex.response.data[0].description);
      }
      console.error(ex);
    }
  }

  return (<Container className='d-flex align-items-center justify-content-center h-100'>
    <Card className="w-50">
      <Card.Header><h3 className='mb-0'>Sign In</h3></Card.Header>
      <Card.Body>
        {!!error && (<Alert variant='danger'>{error}</Alert>)}

        <Form.Group className="mb-3" controlId="formBasicEmail">
          <Form.Label>Email address</Form.Label>
          <Form.Control type="email"
            placeholder="Enter email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            isInvalid={!!emailError}
            required
          />
          <Form.Control.Feedback type="invalid">{emailError}</Form.Control.Feedback>
        </Form.Group>

        <Form.Group className="mb-3" controlId="formBasicPassword">
          <Form.Label>Password</Form.Label>
          <Form.Control type="password"
            placeholder="Password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            isInvalid={!!passError}
            required
          />
          <Form.Control.Feedback type="invalid">{passError}</Form.Control.Feedback>
        </Form.Group>

        <Button variant="primary" type="button" onClick={signIn}>
          Sign In
        </Button>

        <p className='text-end'>You don&apos;t have an account? <Link href="/signup">Sign-Up Here</Link></p>
      </Card.Body>
    </Card>
  </Container>);
}
