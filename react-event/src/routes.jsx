import React from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from "./pages/Home";
import Login from "./pages/auth/Login";
import Register from "./pages/auth/Register";
import Logout from "./pages/auth/Logout";
import NavbarOffcanvas from "./components/NavbarOffcanvas";
import PageEventos from "./pages/Eventos";
import PageInscriptos from "./pages/Inscriptos";
import Dashboard from "./pages/admin/Dashboard";
import Eventos from "./pages/admin/Eventos";
import Usuarios from "./pages/admin/Usuarios";

export default function MisRutas() {
  return (
    <BrowserRouter>
      <div className="min-vh-100">
        <NavbarOffcanvas />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />

          <Route path="/eventos" element={<PageEventos />} />
          <Route path="/inscriptos" element={<PageInscriptos />} />

          <Route path="/admin/dashboard" element={<Dashboard />} />
          <Route path="/admin/eventos" element={<Eventos />} />
          <Route path="/admin/usuarios" element={<Usuarios />} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}
