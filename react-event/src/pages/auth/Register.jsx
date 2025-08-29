import React, { useContext, useState } from "react";
import { Alert, Button, Container, Form } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import { UserContext } from "../../contexts/UserContext";

export default function Register() {
  const navigate = useNavigate();
const { login } = useContext(UserContext);
  const usuario = localStorage.getItem("usuario");
  const [form, setForm] = useState({
    nombre: "",
    apellido: "",
    correo: usuario ? JSON.parse(usuario).correo : "",
    contrasenia: "",
    confirmar: "",
  });

  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (form.contrasenia !== form.confirmar) {
      setError("Las contraseñas no coinciden");
      return;
    }

    try {
      const response = await fetch("https://localhost:7294/api/Usuarios", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          nombre: form.nombre,
          apellido: form.apellido,
          correo: form.correo,
          contrasenia: form.contrasenia,
          token: usuario ? JSON.parse(usuario).token : "",
          rol: "Usuario",
        }),
      });

      const data = await response.json();

      if (data.success) {
        setSuccess(data.message);
        console.log("Usuario registrado:", data);
        const usuario = {
          id: data.data.id,
          nombre: data.data.nombre,
          apellido: data.data.apellido,
          token: data.data.token,
          correo: data.data.correo,
          rol: data.data.rol,
          displayName: JSON.parse(usuario).displayName,
          photoURL: JSON.parse(usuario).photoURL,
        };
        login(usuario);
        navigate("/");
      } else {
        setError(data.message || "Error al registrar usuario");
      }
    } catch (err) {
      setError("Hubo un problema con el servidor");
    }
  };
  return (
    <Container fluid="sm" className="mt-5">
      <h1>Register</h1>
      {error && <Alert variant="danger">{error}</Alert>}
      {success && <Alert variant="success">{success}</Alert>}

      <Form onSubmit={handleSubmit}>
        <Form.Group className="mb-3">
          <Form.Label>Nombre</Form.Label>
          <Form.Control
            type="text"
            name="nombre"
            value={form.nombre}
            onChange={handleChange}
            required
          />
        </Form.Group>

        <Form.Group className="mb-3">
          <Form.Label>Apellido</Form.Label>
          <Form.Control
            type="text"
            name="apellido"
            value={form.apellido}
            onChange={handleChange}
            required
          />
        </Form.Group>

        <Form.Group className="mb-3">
          <Form.Label>Correo</Form.Label>
          <Form.Control
            type="email"
            name="correo"
            value={form.correo}
            onChange={handleChange}
            required
            disabled={!!usuario}
          />
        </Form.Group>

        <Form.Group className="mb-3">
          <Form.Label>Contraseña</Form.Label>
          <Form.Control
            type="password"
            name="contrasenia"
            value={form.contrasenia}
            onChange={handleChange}
            required
          />
        </Form.Group>

        <Form.Group className="mb-3">
          <Form.Label>Confirmar Contraseña</Form.Label>
          <Form.Control
            type="password"
            name="confirmar"
            value={form.confirmar}
            onChange={handleChange}
            required
          />
        </Form.Group>

        <Button variant="primary" type="submit">
          Registrarse
        </Button>
      </Form>
    </Container>
  );
}
