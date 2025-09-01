import React, { useEffect, useState } from "react";
import { Card, Col, Container, Row, Spinner, Badge } from "react-bootstrap";
import { format } from "date-fns";
import { es } from "date-fns/locale";
import { NavLink } from "react-router-dom";

export default function PageEventos() {
  const [eventos, setEventos] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchEventos = async () => {
      try {
        const response = await fetch("https://localhost:7294/api/Eventos");
        const data = await response.json();
        setEventos(data);
      } catch (error) {
        console.error("Error al cargar eventos:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchEventos();
  }, []);

  if (loading) {
    return (
      <div className="d-flex justify-content-center mt-5">
        <Spinner animation="border" variant="primary" />
      </div>
    );
  }

  return (
    <Container className="mt-5">
      {/* Logo en la parte superior */}
      <div className="text-center mb-4">
        <img
          src="https://i.pinimg.com/originals/e1/59/25/e15925c931a81678a3c2e0c0a40db781.gif"
          alt="Logo"
          style={{ width: "80px", height: "80px" }}
          className="rounded-circle shadow-sm"
        />
      </div>

      <h1 className="text-center mb-5 fw-bold text-primary">
        🌟 Próximos Eventos
      </h1>

      <Row>
        {eventos
          .filter((evento) => evento.estado === "Activo")
          .map((evento) => (
            <Col key={evento.id} xs={12} sm={6} lg={4} className="mb-4">
              <Card className="h-100 shadow-lg border-0 rounded-4 overflow-hidden card-hover">
                <div className="position-relative">
                  <Card.Img
                    variant="top"
                    src={
                      evento.archivos?.[0]?.url ||
                      "https://via.placeholder.com/300"
                    }
                    alt={evento.nombre_Evento}
                    style={{ objectFit: "cover", height: "220px" }}
                  />
                  <Badge
                    bg="danger"
                    className="position-absolute top-0 end-0 m-2 px-3 py-2 shadow"
                  >
                    {evento.tipo}
                  </Badge>
                </div>

                <Card.Body className="d-flex flex-column">
                  <Card.Title className="fw-bold text-dark">
                    {evento.nombre_Evento}
                  </Card.Title>

                  <Card.Text className="text-muted mb-3">
                    <i className="bi bi-calendar-event me-2 text-primary"></i>
                    {format(new Date(evento.fecha), "dd 'de' MMMM 'de' yyyy", {
                      locale: es,
                    })}
                    <br />
                    <i className="bi bi-geo-alt me-2 text-danger"></i>
                    {evento.lugar}
                  </Card.Text>

                  <div className="mt-auto d-flex justify-content-between align-items-center">
                    <span className="fw-semibold text-success">
                      {evento.costo > 0 ? `${evento.costo} Bs` : "Gratis"}
                    </span>
                    <NavLink
                      to={`/show/${evento.id}`}
                      className="btn btn-primary btn-sm rounded-pill px-3"
                    >
                      Ver Detalles
                    </NavLink>
                  </div>
                </Card.Body>
              </Card>
            </Col>
          ))}
      </Row>

      {/* CSS extra para efecto hover */}
      <style>
        {`
          .card-hover {
            transition: transform 0.3s ease, box-shadow 0.3s ease;
          }
          .card-hover:hover {
            transform: translateY(-8px);
            box-shadow: 0 12px 30px rgba(0,0,0,0.15);
          }
        `}
      </style>
    </Container>
  );
}
