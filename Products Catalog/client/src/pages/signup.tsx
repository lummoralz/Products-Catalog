import { useState } from 'react';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Card from 'react-bootstrap/Card';
import Container from 'react-bootstrap/Container';
import Alert from 'react-bootstrap/Alert';
import axios from 'axios';
import { useRouter } from 'next/router';
import Link from 'next/link';

export default function SignUpPage() {
  const router = useRouter();

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPass, setConfirmPass] = useState('');

  const [emailError, setEmailError] = useState('');
  const [passError, setPassError] = useState('');
  const [confirmPassError, setConfirmPassError] = useState('');
  const [error, setError] = useState('');

  async function signUp() {
    setError('');
    setEmailError('');
    setPassError('');
    setConfirmPassError('');

    try {
      await axios.post('https://localhost:7208/api/Users/signup', { Email: email, Password: password, ConfirmPassword: confirmPass });
      router.push('/signin');
    } catch (ex: any) {
      const { errors, error } = ex.response.data;
      if (!!errors) {
        setEmailError(errors?.Email?.[0]);
        setPassError(errors?.Password?.[0]);
        setConfirmPassError(errors?.ConfirmPassword?.[0]);
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
      <Card.Header><h3>Sign Up</h3></Card.Header>
      <Card.Body>
        {!!error && (<Alert variant='danger'>{error}</Alert>)}

        <Form.Group className="mb-3" controlId="email">
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

        <Form.Group className="mb-3" controlId="password">
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

        <Form.Group className='mb-3' controlId='confirm'>
          <Form.Label>Confirm Password</Form.Label>
          <Form.Control type='password'
            placeholder='Confirm password'
            value={confirmPass}
            onChange={(e) => setConfirmPass(e.target.value)}
            isInvalid={!!confirmPassError}
            required
          />
          <Form.Control.Feedback type='invalid'>{confirmPassError}</Form.Control.Feedback>
        </Form.Group>

        <Button variant="primary" type="button" onClick={signUp}>
          Sign Up
        </Button>

        <p className='text-end'>You already have an account? <Link href="/signin">Sign-In Here</Link></p>
      </Card.Body>
    </Card>
  </Container>);
}