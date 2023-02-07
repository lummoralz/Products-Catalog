import Container from 'react-bootstrap/Container';
import Navbar from 'react-bootstrap/Navbar';
import Nav from 'react-bootstrap/Nav';
import Link from 'next/link';

export default function AppNavbar() {
  return (<Navbar bg="dark" expand="lg" variant="dark">
    <Container>
      <Navbar.Brand href="/">Products catalog</Navbar.Brand>
      <Navbar.Toggle aria-controls="basic-navbar-nav" />
      <Navbar.Collapse id="basic-navbar-nav">
        <Nav className="me-auto">
          <Nav.Link as={Link} href="/new-product">New product</Nav.Link>
        </Nav>
      </Navbar.Collapse>
    </Container>
  </Navbar>);
}