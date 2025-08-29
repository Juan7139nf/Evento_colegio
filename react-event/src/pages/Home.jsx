import React from "react";
import ControlledCarousel from "../components/ControlledCarousel";

const carrousel = [
  {
    img: "https://cdn-imgix.headout.com/media/images/c9db3cea62133b6a6bb70597326b4a34-388-dubai-img-worlds-of-adventure-tickets-01.jpg",
    title: "Titulo 1",
    descripcion: "Descripcion 1",
  },
  {
    img: "https://cdn-imgix.headout.com/media/images/c9db3cea62133b6a6bb70597326b4a34-388-dubai-img-worlds-of-adventure-tickets-01.jpg",
    title: "Titulo 2",
    descripcion: "Descripcion 2",
  },
  {
    img: "https://cdn-imgix.headout.com/media/images/c9db3cea62133b6a6bb70597326b4a34-388-dubai-img-worlds-of-adventure-tickets-01.jpg",
    title: "Titulo 3",
    descripcion: "Descripcion 3",
  },
];

export default function Home() {
  return (
    <>
      <ControlledCarousel data={carrousel} />
      <h1>Inicio</h1>
      
    </>
  );
}
