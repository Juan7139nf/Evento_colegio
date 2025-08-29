import React, { useContext } from "react";
import {
  Button,
  Container,
  Form,
  Nav,
  Navbar,
  NavDropdown,
  Offcanvas,
} from "react-bootstrap";
import { NavLink } from "react-router-dom";
import { UserContext } from "../contexts/UserContext";
import Logout from "../pages/auth/Logout";

export default function NavbarOffcanvas() {
  const { usuario } = useContext(UserContext);
  const expand = "lg";
  return (
    <Navbar
      bg="primary"
      data-bs-theme="dark"
      expand={expand}
      className="sticky-top"
    >
      <Container fluid>
        <NavLink to={"/"} className={"navbar-brand"}>
          Home
        </NavLink>
        <Navbar.Toggle aria-controls={`offcanvasNavbar-expand-${expand}`} />
        <Navbar.Offcanvas
          id={`offcanvasNavbar-expand-${expand}`}
          aria-labelledby={`offcanvasNavbarLabel-expand-${expand}`}
          placement="end"
        >
          <Offcanvas.Header closeButton>
            <Offcanvas.Title id={`offcanvasNavbarLabel-expand-${expand}`}>
              Offcanvas
            </Offcanvas.Title>
          </Offcanvas.Header>
          <Offcanvas.Body>
            <Nav className="justify-content-end flex-grow-1 pe-3">
              <NavLink to={"/"} className={"nav-link"}>
                Inicio
              </NavLink>
              <NavLink to={"/eventos"} className={"nav-link"}>Eventos</NavLink>
              <NavLink to={"/inscriptos"} className={"nav-link"}>Inscriptos</NavLink>
              {usuario?.rol === "Admin" && (
                <NavDropdown
                  title="Admin"
                  id={`offcanvasNavbarDropdown-expand-${expand}`}
                >
                  <NavLink to={"/admin/dashboard"} className={"dropdown-item"}>
                    Dashboard
                  </NavLink>
                  <NavDropdown.Divider />
                  <NavLink to={"/admin/eventos"} className={"dropdown-item"}>
                    Eventos
                  </NavLink>
                  <NavLink to={"/admin/usuarios"} className={"dropdown-item"}>
                    Usuarios
                  </NavLink>
                </NavDropdown>
              )}
              {usuario?.token ? (
                <Logout />
              ) : (
                <NavLink to={"/login"} className={"nav-link"}>
                  Login
                </NavLink>
              )}
            </Nav>
            {/* <Form className="d-flex">
              <Form.Control
                type="search"
                placeholder="Search"
                className="me-2"
                aria-label="Search"
              />
              <Button variant="outline-success">Search</Button>
            </Form> */}
          </Offcanvas.Body>
        </Navbar.Offcanvas>
      </Container>
    </Navbar>
  );
}
