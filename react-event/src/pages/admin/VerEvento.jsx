import React, { useEffect, useState } from "react";
import { Button, Card, Col, Container, Row, Spinner } from "react-bootstrap";
import { NavLink, useNavigate, useParams } from "react-router-dom";
import { format } from "date-fns";
import { es } from "date-fns/locale";

export default function VerEvento() {
  const { id } = useParams(); // Obtenemos el id del evento
  const navigate = useNavigate();
  const [evento, setEvento] = useState({});
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchEvento = async () => {
      try {
        const response = await fetch(
          `https://localhost:7294/api/Eventos/${id}`
        );
        if (!response.ok) throw new Error("Error al cargar evento");
        const data = await response.json();
        setEvento({
          nombre_Evento: data.nombre_Evento || "",
          tipo: data.tipo || "",
          fecha: data.fecha ? data.fecha.slice(0, 16) : "",
          lugar: data.lugar || "",
          capacidad_Max: data.capacidad_Max || 1,
          estado: data.estado || "Activo",
          encuesta: data.encuesta ? JSON.parse(data.encuesta) : [],
          costo: data.costo || "",
          content: data.content || [],
          archivos: data.archivos || [],
        });
        setLoading(false);
      } catch (error) {
        console.error(error);
      }
    };

    fetchEvento();
  }, [id]);
  if (loading)
    return (
      <div className="d-flex justify-content-center mt-5">
        <Spinner animation="border" />
      </div>
    );

  return (
    <Container fluid="sm" className="mt-3 mb-5">
      <div className="d-flex justify-content-between align-items-center mb-3">
        <h2>Ver Evento</h2>
        <Button onClick={() => navigate(-1)} className="btn btn-secondary">
          Volver
        </Button>
      </div>
      <h1 className="fw-bold text-center">{evento.nombre_Evento}</h1>
      <Card className="p-3 my-3 shadow">
        <p>Tipo: {evento.tipo}</p>
        <p>
          Fecha:{" "}
          {format(new Date(evento.fecha), "dd 'de' MMMM 'de' yyyy, HH:mm", {
            locale: es,
          })}
        </p>
        <p>Lugar: {evento.lugar}</p>
        <p>Capacidad Máxima: {evento.capacidad_Max}</p>
        <p>Costo: {evento.costo} Bs</p>
      </Card>
      {/* <div>{JSON.stringify(evento, null, 2)}</div> */}
      <Row className="mt-4">
        {evento.archivos?.map((archivo) => (
          <Col key={archivo.orden} xs={12} sm={6} lg={4} className="mb-4">
            <Card className="aspect-ratio-1x1 h-100 shadow-sm">
              <Card.Img
                className="aspect-ratio-1x1 h-100 w-100 object-fit-cover"
                src={archivo.url || "https://via.placeholder.com/300"}
                alt={evento.nombre_Evento}
              />
            </Card>
          </Col>
        ))}
      </Row>
      {evento.content?.map((section, idx) => (
        <Card key={idx} className="p-3 my-3 shadow">
          <h3 className="fw-bold">{section.title}</h3>
          <p>{section.content}</p>
        </Card>
      ))}
    </Container>
  );
}
