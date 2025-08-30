import React from "react";
import { Container, NavLink } from "react-bootstrap";

export default function Eventos() {
  return (
    <Container>
      <NavLink to={"/admin/eventos/crear"}>Crear</NavLink>
      <div>Eventos</div>
    </Container>
  );
}
