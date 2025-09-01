import { useState } from "react";
import { Image } from "react-bootstrap";
import Carousel from "react-bootstrap/Carousel";
import { NavLink } from "react-router-dom";
import "./CarouselCustom.css"; // 👈 estilos personalizados

export default function ControlledCarousel({ data = [] }) {
  const [index, setIndex] = useState(0);

  const handleSelect = (selectedIndex) => {
    setIndex(selectedIndex);
  };

  return (
    <Carousel
      activeIndex={index}
      onSelect={handleSelect}
      fade
      className="custom-carousel"
    >
      {data.map((item, idx) => (
        <Carousel.Item key={idx} className="carousel-slide">
          <Image
            src={item.img}
            fluid
            className="carousel-image w-100"
          />
          <Carousel.Caption className="carousel-caption-custom">
            <h3 className="carousel-title">{item.title}</h3>
            <p className="carousel-text">{item.descripcion}</p>
            <NavLink to={`/show/${item.id}`} className="btn btn-light btn-lg shadow-sm custom-btn">
              Ver detalles
            </NavLink>
          </Carousel.Caption>
        </Carousel.Item>
      ))}
    </Carousel>
  );
}
