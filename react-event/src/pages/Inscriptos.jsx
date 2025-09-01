import React, { useEffect, useState } from "react";
import { Card, Col, Container, Row, Spinner } from "react-bootstrap";
import { format } from "date-fns";
import { es } from "date-fns/locale";
import { NavLink } from "react-router-dom";
import "./PageInscriptos.css"; // 👈 Importa el CSS

export default function PageInscriptos() {
  const [inscripciones, setInscripciones] = useState([]);
  const [loading, setLoading] = useState(true);
  const dataUser = JSON.parse(localStorage.getItem("usuario"));

  useEffect(() => {
    const fetchEventos = async () => {
      try {
        const response = await fetch("https://localhost:7294/api/Inscripciones");
        const data = await response.json();
        setInscripciones(data);
      } catch (error) {
        console.error("Error al cargar inscripciones:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchEventos();
  }, []);

  if (loading) {
    return (
      <div className="d-flex justify-content-center mt-5">
        <Spinner animation="border" />
      </div>
    );
  }

  return (
    <Container className="mt-4 page-inscriptos">
      <h1 className="text-center mb-4 title-inscriptos">Mis Inscripciones</h1>
      <Row>
        {inscripciones
          .filter(
            (inscripcion) => inscripcion.id_Usuario === dataUser?.id || ""
          )
          .sort(
            (a, b) =>
              new Date(b.fecha_Inscripcion) - new Date(a.fecha_Inscripcion)
          )
          .map((inscripcion) => (
            <Col key={inscripcion.id} xs={12} sm={6} lg={4} className="mb-4">
              <Card className="h-100 shadow card-inscripcion">
                <div className="img-container">
                  <Card.Img
                    variant="top"
                    src={
                      inscripcion.evento.archivos?.[0]?.url ||
                      "https://via.placeholder.com/300"
                    }
                    alt={inscripcion.evento.nombre_Evento}
                    className="card-img-top"
                  />
                  <span
                    className={`estado-badge ${
                      inscripcion.estado === "Completado"
                        ? "bg-success"
                        : "bg-secondary"
                    }`}
                  >
                    {inscripcion.estado}
                  </span>
                </div>

                <Card.Body>
                  <Card.Title className="fw-bold text-primary">
                    {inscripcion.evento.nombre_Evento}
                  </Card.Title>
                  <Card.Text>
                    <strong>Inscripción:</strong>{" "}
                    {format(
                      new Date(inscripcion.fecha_Inscripcion),
                      "dd 'de' MMMM 'de' yyyy",
                      { locale: es }
                    )}
                    <br />
                    <strong>Evento:</strong>{" "}
                    {format(
                      new Date(inscripcion.evento.fecha),
                      "dd 'de' MMMM 'de' yyyy",
                      { locale: es }
                    )}
                    <br />
                    <strong>Lugar:</strong> {inscripcion.evento.lugar}
                  </Card.Text>
                  <div className="d-flex justify-content-between align-items-center">
                    <div>
                      <strong>Costo:</strong> {inscripcion.evento.costo} Bs
                      <br />
                      <strong>Tipo:</strong> {inscripcion.evento.tipo}
                    </div>
                    <NavLink
                      to={`/show/${inscripcion.evento.id}`}
                      className="btn btn-outline-primary btn-sm"
                    >
                      Ver Detalles
                    </NavLink>
                  </div>
                </Card.Body>
              </Card>
            </Col>
          ))}
      </Row>
    </Container>
  );
}
