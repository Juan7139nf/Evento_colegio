import React, { useEffect, useState } from "react";
import { Card, Col, Container, Nav, Row, Spinner } from "react-bootstrap";
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
        <Spinner animation="border" />
      </div>
    );
  }

  return (
    <Container className="mt-4">
      <h1 className="text-center">Eventos</h1>
      <Row>
        {eventos.map((evento) => (
          <Col key={evento.id} xs={12} sm={6} lg={4} className="mb-4">
            <Card className="h-100 shadow-sm">
              <Card.Img
                variant="top"
                src={
                  evento.archivos?.[0]?.url || "https://via.placeholder.com/300"
                }
                alt={evento.nombre_Evento}
                style={{ objectFit: "cover", height: "200px" }}
              />
              <Card.Body>
                <Card.Title className="fw-bold">
                  {evento.nombre_Evento}
                </Card.Title>
                <Card.Text className="mb-1">
                  <strong>Fecha:</strong>{" "}
                  {format(new Date(evento.fecha), "dd 'de' MMMM 'de' yyyy", {
                    locale: es,
                  })}{" "}
                  <br />
                  <strong>Lugar:</strong> {evento.lugar}
                </Card.Text>
                <div className="d-flex justify-content-between">
                  <div className="">
                    <strong>Costo:</strong> {evento.costo} Bs <br />
                    <strong>Tipo:</strong> {evento.tipo}
                  </div>
                  <div className="">
                    <NavLink to={`/show/${evento.id}`} className="btn btn-primary">
                      Ver Detalles
                    </NavLink>
                  </div>
                </div>
              </Card.Body>
            </Card>
          </Col>
        ))}
      </Row>
    </Container>
  );
}
