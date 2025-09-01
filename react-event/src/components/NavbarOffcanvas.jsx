import React, { useContext } from "react";
import {
  Container,
  Nav,
  Navbar,
  NavDropdown,
  Offcanvas,
} from "react-bootstrap";
import { NavLink } from "react-router-dom";
import { UserContext } from "../contexts/UserContext";
import Logout from "../pages/auth/Logout";
import Login from "../pages/auth/Login";
import "./NavbarCustom.css"; // 👈 Importamos los estilos personalizados

export default function NavbarOffcanvas() {
  const { usuario } = useContext(UserContext);
  const expand = "sm";

  return (
    <Navbar
      expand={expand}
      className="custom-navbar sticky-top"
    >
      <Container fluid>
        {/* Logo + Marca */}
        <div className="d-flex align-items-center">
          <img
            src="https://cdn-icons-gif.flaticon.com/12743/12743734.gif"
            alt="Logo"
            className="navbar-logo"
          />
          <NavLink to={"/"} className="navbar-brand custom-brand ms-2">
            Eventos
          </NavLink>
        </div>

        <Navbar.Toggle
          aria-controls={`offcanvasNavbar-expand-${expand}`}
          className="custom-toggle"
        />
        <Navbar.Offcanvas
          id={`offcanvasNavbar-expand-${expand}`}
          aria-labelledby={`offcanvasNavbarLabel-expand-${expand}`}
          placement="end"
          className="custom-offcanvas"
        >
          <Offcanvas.Header closeButton>
            <Offcanvas.Title id={`offcanvasNavbarLabel-expand-${expand}`} className="custom-title">
              Eventos
            </Offcanvas.Title>
          </Offcanvas.Header>
          <Offcanvas.Body>
            <Nav className="justify-content-end flex-grow-1 pe-3 custom-nav">
              <NavLink to={"/"} className="nav-link custom-link">
                Inicio
              </NavLink>
              <NavLink to={"/eventos"} className="nav-link custom-link">
                Eventos
              </NavLink>

              {usuario && (
                <NavLink to={"/inscriptos"} className="nav-link custom-link">
                  Inscripciones
                </NavLink>
              )}

              {usuario?.rol === "Admin" && (
                <NavDropdown
                  title="Admin"
                  id={`offcanvasNavbarDropdown-expand-${expand}`}
                  className="custom-dropdown"
                >
                  <NavLink to={"/admin/dashboard"} className="dropdown-item custom-dropdown-item">
                    Dashboard
                  </NavLink>
                  <NavDropdown.Divider />
                  <NavLink to={"/admin/eventos"} className="dropdown-item custom-dropdown-item">
                    Eventos
                  </NavLink>
                  <NavLink to={"/admin/inscripciones"} className="dropdown-item custom-dropdown-item">
                    Inscripciones
                  </NavLink>
                  <NavLink to={"/admin/usuarios"} className="dropdown-item custom-dropdown-item">
                    Usuarios
                  </NavLink>
                </NavDropdown>
              )}

              {usuario?.token ? <Logout /> : <Login />}
            </Nav>
          </Offcanvas.Body>
        </Navbar.Offcanvas>
      </Container>
    </Navbar>
  );
}
