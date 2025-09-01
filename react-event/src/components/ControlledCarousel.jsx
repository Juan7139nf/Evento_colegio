import { useState } from "react";
import { Image } from "react-bootstrap";
import Carousel from "react-bootstrap/Carousel";
import { NavLink } from "react-router-dom";

export default function ControlledCarousel({ data = [] }) {
  const [index, setIndex] = useState(0);

  const handleSelect = (selectedIndex) => {
    setIndex(selectedIndex);
  };

  return (
    <Carousel activeIndex={index} onSelect={handleSelect}>
      {data.map((item, index) => (
        <Carousel.Item key={index}>
          <Image
            src={item.img}
            fluid
            style={{ maxHeight: "80vh", minHeight: "80vh" }}
            className="object-fit-cover w-100"
          />
          <Carousel.Caption className="bg-black bg-opacity-50 mb-5 rounded-5">
            <h3>{item.title}</h3>
            <p>{item.descripcion}</p>
            <NavLink to={`/show/${item.id}`} className="btn btn-primary">Ver detalles</NavLink>
          </Carousel.Caption>
        </Carousel.Item>
      ))}
    </Carousel>
  );
}
