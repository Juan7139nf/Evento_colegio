import React, { useEffect, useState } from "react";
import { ButtonGroup, Container, Image, Table } from "react-bootstrap";
import { NavLink } from "react-router-dom";
import { format } from "date-fns";
import { es } from "date-fns/locale";

export default function Eventos() {
  const [eventos, setEventos] = useState([]);

  useEffect(() => {
    const fetchEventos = async () => {
      try {
        const response = await fetch("https://localhost:7294/api/Eventos");
        const data = await response.json();
        setEventos(data);
      } catch (error) {
        console.error("Error al cargar eventos:", error);
      } /* finally {
        setLoading(false);
      } */
    };

    fetchEventos();
  }, []);
  return (
    <Container className="mt-4">
      <div className="d-flex justify-content-between align-items-center mb-3">
        <h2>Gestión de Eventos</h2>
        <NavLink to="/admin/eventos/crear" className="btn btn-success">
          Crear Evento
        </NavLink>
      </div>
      <Table bordered hover striped responsive>
        <thead className="table-dark">
          <tr>
            <th>Evento</th>
            <th>Tipo</th>
            <th>Lugar</th>
            <th>Fecha</th>
            <th>Capacidad</th>
            <th>Costo</th>
            <th>Estado</th>
            <th>Acciones</th>
          </tr>
        </thead>
        <tbody>
          {eventos && eventos.length > 0 ? (
            eventos.map((item, idx) => (
              <tr key={idx}>
                <td className="flex align-items-center">
                  <Image
                    src={
                      item.archivos?.[0]?.url ||
                      "https://via.placeholder.com/150"
                    }
                    width={50}
                    height={50}
                    className="object-fit-cover rounded-circle me-2"
                  />
                  <b>{item.nombre_Evento}</b>
                </td>
                <td className="align-middle">{item.tipo}</td>
                <td className="align-middle">{item.lugar}</td>
                <td className="align-middle">
                  {format(new Date(item.fecha), "dd 'de' MMMM 'de' yyyy", {
                    locale: es,
                  })}
                </td>
                <td className="align-middle">{item.capacidad_Max} personas</td>
                <td className="align-middle">{item.costo} Bs</td>
                <td className="align-middle">{item.estado}</td>
                <td className="align-middle">
                  <ButtonGroup>
                    <NavLink
                      to={`/admin/eventos/${item.id}`}
                      className="btn btn-sm btn-primary"
                    >
                      Ver
                    </NavLink>
                    <NavLink
                      to={`/admin/eventos/${item.id}/editar`}
                      className="btn btn-sm btn-success"
                    >
                      Editar
                    </NavLink>
                  </ButtonGroup>
                </td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan="5" className="text-center">
                No hay registros disponibles
              </td>
            </tr>
          )}
        </tbody>
      </Table>
    </Container>
  );
}
