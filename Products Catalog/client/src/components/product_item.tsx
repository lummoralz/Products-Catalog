
import Card from 'react-bootstrap/Card';
import Image from 'next/image';

export default function ProductItem({ product }: any) {
  return (<Card className='rounded shadow-sm border-0 mb-3'>
    <Card.Body className='p-4'>
      <Image src="https://picsum.photos/200/200" width={200} height={200} alt="" className="img-fluid d-block mx-auto mb-3" />
      <h5><a href="#" className="text-dark">{product.name}</a></h5>
      <p className="small text-muted font-italic">{product.description}<br/>Unit Price: ${product.unitPrice}, Available: {product.amount}</p>
    </Card.Body>
  </Card>);
};
