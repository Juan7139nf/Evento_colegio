import React, { useEffect, useState } from "react";
import { Card, Col, Container, Row, Spinner } from "react-bootstrap";
import { format } from "date-fns";
import { da, es } from "date-fns/locale";
import { NavLink } from "react-router-dom";

export default function PageInscriptos() {
  const [inscripciones, setInscripciones] = useState([]);
  const [loading, setLoading] = useState(true);
  const dataUser = JSON.parse(localStorage.getItem("usuario"));

  useEffect(() => {
    const fetchEventos = async () => {
      try {
        const response = await fetch(
          "https://localhost:7294/api/Inscripciones"
        );
        const data = await response.json();
        console.log(data);
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
    <Container className="mt-4">
      <h1 className="text-center">Inscriptos</h1>
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
              <Card className="h-100 shadow-sm position-relative">
                <Card.Img
                  variant="top"
                  src={
                    inscripcion.evento.archivos?.[0]?.url ||
                    "https://via.placeholder.com/300"
                  }
                  alt={inscripcion.evento.nombre_Evento}
                  style={{ objectFit: "cover", height: "200px" }}
                />
                <Card.Body>
                  <Card.Title className="fw-bold">
                    {inscripcion.evento.nombre_Evento}
                  </Card.Title>
                  <Card.Text className="mb-1">
                    <strong>Fecha inscripcion:</strong>{" "}
                    {format(
                      new Date(inscripcion.fecha_Inscripcion),
                      "dd 'de' MMMM 'de' yyyy",
                      {
                        locale: es,
                      }
                    )}{" "}
                    <br />
                    <strong>Fecha:</strong>{" "}
                    {format(
                      new Date(inscripcion.evento.fecha),
                      "dd 'de' MMMM 'de' yyyy",
                      {
                        locale: es,
                      }
                    )}{" "}
                    <br />
                    <strong>Lugar:</strong> {inscripcion.evento.lugar}
                  </Card.Text>
                  <div className="d-flex justify-content-between">
                    <div className="">
                      <strong>Costo:</strong> {inscripcion.evento.costo} Bs{" "}
                      <br />
                      <strong>Tipo:</strong> {inscripcion.evento.tipo}
                    </div>
                    <div className="">
                      <NavLink
                        to={`/show/${inscripcion.evento.id}`}
                        className="btn btn-primary"
                      >
                        Ver Detalles
                      </NavLink>
                    </div>
                  </div>
                </Card.Body>
                <div className="position-absolute top-0 end-0">
                  <span
                    className={`badge bg-${
                      inscripcion.estado === "Completado"
                        ? "success"
                        : "secondary"
                    } m-2 p-2`}
                  >
                    {inscripcion.estado}
                  </span>
                </div>
              </Card>
            </Col>
          ))}
      </Row>
    </Container>
  );
}
