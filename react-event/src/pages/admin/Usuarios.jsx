import React, { useEffect, useState } from "react";
import { Container, Table, Button, Form } from "react-bootstrap";

export default function Usuarios() {
  const [usuarios, setUsuarios] = useState([]);

  useEffect(() => {
    const fetchUsuarios = async () => {
      try {
        const response = await fetch("https://localhost:7294/api/Usuarios");
        const data = await response.json();
        setUsuarios(data);
      } catch (error) {
        console.error("Error al cargar usuarios:", error);
      }
    };

    fetchUsuarios();
  }, []);

  const actualizarRol = async (id, rol) => {
    try {
      const response = await fetch(
        "https://localhost:7294/api/Usuarios/actualizar-rol",
        {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({ id, rol }),
        }
      );

      if (response.ok) {
        const data = await response.json();
        alert(`Rol actualizado a: ${data.rol}`);
        // Actualizamos el estado local para reflejar el cambio en la tabla
        setUsuarios((prev) =>
          prev.map((u) => (u.id === id ? { ...u, rol } : u))
        );
      } else {
        console.error("Error al actualizar rol");
      }
    } catch (error) {
      console.error("Error al actualizar rol:", error);
    }
  };

  return (
    <Container className="mt-4">
      <h2>Gestión de Usuarios</h2>
      <Table bordered hover striped responsive>
        <thead className="table-dark">
          <tr>
            <th>Usuario</th>
            <th>Correo</th>
            <th>Rol</th>
            <th>Actualizar Rol</th>
            <th>Inscripciones Pendiente</th>
            <th>Inscripciones Completas</th>
          </tr>
        </thead>
        <tbody>
          {usuarios && usuarios.length > 0 ? (
            usuarios.map((item) => (
              <tr key={item.id}>
                <td className="align-middle">
                  <b>{item.nombre} {item.apellido}</b>
                </td>
                <td className="align-middle">{item.correo}</td>
                <td className="align-middle">{item.rol}</td>
                <td className="align-middle" style={{ minWidth: '120px' }}>
                  <Form.Select
                    defaultValue={item.rol}
                    onChange={(e) => actualizarRol(item.id, e.target.value)}
                  >
                    <option value="Usuario">Usuario</option>
                    <option value="Admin">Admin</option>
                    {/* <option value="Moderador">Moderador</option> */}
                  </Form.Select>
                </td>
                <td className="align-middle">{item.pendientes} inscripciones pendientes</td>
                <td className="align-middle">{item.completadas} inscripciones completadas</td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan="6" className="text-center">
                No hay registros disponibles
              </td>
            </tr>
          )}
        </tbody>
      </Table>
    </Container>
  );
}
