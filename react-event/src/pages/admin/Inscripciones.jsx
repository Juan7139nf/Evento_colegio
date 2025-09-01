import React, { useEffect, useState } from "react";
import { format } from "date-fns";
import { es } from "date-fns/locale";
import { Container, Image, Table } from "react-bootstrap";

export default function Inscripciones() {
  const [eventos, setEventos] = useState([]);

  useEffect(() => {
    const fetchInscripciones = async () => {
      try {
        const response = await fetch(
          "https://localhost:7294/api/Inscripciones"
        );
        const data = await response.json();
        console.log(data);
        setEventos(data);
      } catch (error) {
        console.error("Error al cargar inscripciones:", error);
      } /* finally {
        setLoading(false);
      } */
    };

    fetchInscripciones();
  }, []);
  return (
    <Container className="mt-4">
      <div className="d-flex justify-content-between align-items-center mb-3">
        <h2>Gestión de Eventos</h2>
      </div>
      <Table bordered hover striped responsive>
        <thead className="table-dark">
          <tr>
            <th>Evento</th>
            <th>Usuario</th>
            <th>Fecha</th>
            <th>Costo</th>
            <th>Estado</th>
          </tr>
        </thead>
        <tbody>
          {eventos && eventos.length > 0 ? (
            eventos.map((item, idx) => (
              <tr key={idx}>
                <td className="flex align-items-center">
                  <Image
                    src={
                      item.evento.archivos?.[0]?.url ||
                      "https://via.placeholder.com/150"
                    }
                    width={50}
                    height={50}
                    className="object-fit-cover rounded-circle me-2"
                  />
                  <b>{item.evento.nombre_Evento}</b>
                </td>
                <td className="align-middle">{item.usuario.nombre} {item.usuario.apellido}</td>
                <td className="align-middle">
                  {format(new Date(item.fecha_Inscripcion), "dd 'de' MMMM 'de' yyyy", {
                    locale: es,
                  })}
                </td>
                <td className="align-middle">{item.evento.costo} Bs</td>
                <td className="align-middle">{item.estado}</td>
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
