import React, { useEffect, useState } from "react";
import ControlledCarousel from "../components/ControlledCarousel";
import { format } from "date-fns";
import { es } from "date-fns/locale";
import { Card, Col, Container, Row, Spinner } from "react-bootstrap";
import { NavLink } from "react-router-dom";

const carrousel = [
  {
    img: "https://cdn-imgix.headout.com/media/images/c9db3cea62133b6a6bb70597326b4a34-388-dubai-img-worlds-of-adventure-tickets-01.jpg",
    title: "Titulo 1",
    descripcion: "Descripcion 1",
  },
  {
    img: "https://cdn-imgix.headout.com/media/images/c9db3cea62133b6a6bb70597326b4a34-388-dubai-img-worlds-of-adventure-tickets-01.jpg",
    title: "Titulo 2",
    descripcion: "Descripcion 2",
  },
  {
    img: "https://cdn-imgix.headout.com/media/images/c9db3cea62133b6a6bb70597326b4a34-388-dubai-img-worlds-of-adventure-tickets-01.jpg",
    title: "Titulo 3",
    descripcion: "Descripcion 3",
  },
];

export default function Home() {
  const [eventos, setEventos] = useState([]);
  const [loading, setLoading] = useState(true);
  const [slider, setSlider] = useState([]);

  useEffect(() => {
    const fetchEventos = async () => {
      try {
        const response = await fetch("https://localhost:7294/api/Eventos");
        const data = await response.json();
        setEventos(data);

        // Seleccionar 5 eventos aleatorios
        const shuffled = data.sort(() => 0.5 - Math.random());
        const selected = shuffled.slice(0, 5);

        // Mapear solo los campos necesarios para el slider
        const sliderData = selected.map((evento) => ({
          id: evento.id,
          img:
            evento.archivos?.[0]?.url || "https://via.placeholder.com/600x400",
          title: evento.nombre_Evento,
          descripcion: `Es de tipo ${evento.tipo} a las ${format(
            new Date(evento.fecha),
            "dd 'de' MMMM 'de' yyyy",
            { locale: es }
          )}`,
        }));

        setSlider(sliderData);
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
    <>
      <ControlledCarousel data={slider} />
      <Container className="">
        <div className="my-4 text-center">
          <h1 className="fw-bold">Eventos de colegio</h1>
          <p className="fs-5">
            Este sistema permitirá planificar, administrar y promocionar eventos
            organizados por el Colegio de Profesionales, tales como cursos,
            conferencias, charlas, partidos deportivos y actividades
            recreativas. Los usuarios podrán inscribirse en línea, realizar
            pagos si corresponde, y recibir recordatorios automáticos por correo
            o mensaje. El sistema gestionará la disponibilidad de cupos,
            permitirá a los organizadores subir material de apoyo y, al
            finalizar el evento, habilitará encuestas de satisfacción para
            evaluar la calidad. También generará reportes de participación y
            estadísticas para optimizar la organización futura.
          </p>
        </div>
        <Row>
          {eventos.map((evento) => (
            <Col key={evento.id} xs={12} sm={6} lg={4} className="mb-4">
              <Card className="h-100 shadow-sm">
                <Card.Img
                  variant="top"
                  src={
                    evento.archivos?.[0]?.url ||
                    "https://via.placeholder.com/300"
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
                      <NavLink
                        to={`/show/${evento.id}`}
                        className="btn btn-primary"
                      >
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
    </>
  );
}
