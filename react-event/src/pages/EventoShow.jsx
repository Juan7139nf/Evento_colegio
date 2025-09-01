import React, { useContext, useEffect, useState } from "react";
import {
  Button,
  Card,
  Col,
  Container,
  Modal,
  Row,
  Spinner,
} from "react-bootstrap";
import { NavLink, useNavigate, useParams } from "react-router-dom";
import { format } from "date-fns";
import { da, es } from "date-fns/locale";
import { UserContext } from "../contexts/UserContext";
import Login from "./auth/Login";
import Encuesta from "../components/Encuesta";

export default function EventoShow() {
  const { id } = useParams(); // Obtenemos el id del evento
  const { usuario, login } = useContext(UserContext);

  const navigate = useNavigate();
  const [evento, setEvento] = useState({});
  const [inscripcion, setInscripcion] = useState([]);
  const [pago, setPago] = useState([]);
  const [encuesta, setEncuesta] = useState([]);
  const [loading, setLoading] = useState(true);
  const [showPago, setShowPago] = useState(false);
  const [showEncuesta, setShowEncuesta] = useState(false);

  useEffect(() => {
    const fetchEvento = async () => {
      const dataUser = JSON.parse(localStorage.getItem("usuario"));
      login(dataUser);
      try {
        if (dataUser) {
          const response = await fetch(
            `https://localhost:7294/api/DetalleEvento/${id}/{${dataUser.id}}`
          );
          if (!response.ok) throw new Error("Error al cargar evento");
          const data = await response.json();
          console.log(data);
          setEvento({
            id: data.evento.id,
            nombre_Evento: data.evento.nombre_Evento || "",
            tipo: data.evento.tipo || "",
            fecha: data.evento.fecha ? data.evento.fecha.slice(0, 16) : "",
            lugar: data.evento.lugar || "",
            capacidad_Max: data.evento.capacidad_Max || 1,
            estado: data.evento.estado || "Activo",
            encuesta: data.evento.encuesta
              ? JSON.parse(data.evento.encuesta)
              : [],
            costo: data.evento.costo || "",
            content: data.evento.content || [],
            archivos: data.evento.archivos || [],
          });
          if (data.inscripcion) setInscripcion(data.inscripcion);
          if (data.pago) setPago(data.pago);
          if (data.encuesta) setEncuesta(data.encuesta);
        } else {
          console.log("No hay usuario");
          const response = await fetch(
            `https://localhost:7294/api/Eventos/${id}`
          );
          if (!response.ok) throw new Error("Error al cargar evento");
          const data = await response.json();
          setEvento({
            id: data.id,
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
        }
        setLoading(false);
      } catch (error) {
        console.error(error);
      }
    };

    fetchEvento();
  }, [id]);

  const handleInscribir = async () => {
    const payload = {
      fecha_Inscripcion: new Date().toISOString(),
      estado: "Pendiente",
      id_Usuario: usuario.id,
      id_Evento: evento.id,
    };

    try {
      const response = await fetch("https://localhost:7294/api/Inscripciones", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
      });

      if (response.ok) {
        const data = await response.json();
        alert(data.message);
        console.log(data);
        setInscripcion(data.data);
        setShowPago(true);
      } else {
        const error = await response.text();
        alert("Error al registrar la inscripción ❌");
        console.error(error);
      }
    } catch (error) {
      console.error("Error en la solicitud:", error);
    }
  };

  const handleConfirmPago = async () => {
    const payload = {
      id_Inscripcion: inscripcion.id,
      fecha_Inscripcion: inscripcion.fecha_Inscripcion,
      estado: "Completado",
      id_Usuario: inscripcion.id_Usuario,
      id_Evento: inscripcion.id_Evento,
    };
    const payloapdPago = {
      fecha_Pago: new Date().toISOString(),
      monto: evento.costo,
      metodo: "Web",
      estado: "Completado",
      id_Inscripcion: inscripcion.id,
    };

    try {
      const response = await fetch("https://localhost:7294/api/Pagos", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(payloapdPago),
      });
      if (response.ok) {
        const data = await response.json();
        alert("Se ha realizado el pago con éxito");
        console.log(data);
        setPago(data);
        // Actualizar estado de la inscripción
        const responsePago = await fetch(
          `https://localhost:7294/api/Inscripciones/${inscripcion.id}`,
          {
            method: "PUT",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify(payload),
          }
        );
        const dataInscripcion = await responsePago.json();
        console.log(dataInscripcion);
        setInscripcion(dataInscripcion.data);
        setShowPago(false);
      } else {
        const error = await response.text();
        alert("Error al registrar la inscripción ❌");
        console.error(error);
      }
    } catch (error) {
      console.error("Error en la solicitud:", error);
    }
  };

  if (loading)
    return (
      <div className="d-flex justify-content-center mt-5">
        <Spinner animation="border" />
      </div>
    );

  return (
    <Container fluid="sm" className="mt-4 mb-5">
      <div className="d-flex justify-content-end align-items-center">
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
        {inscripcion && <p>Estado de Inscripción: {inscripcion.estado}</p>}
      </Card>
      {evento.estado === "Activo" && (
        <>
          {usuario ? (
            <>
              <div className="text-center">
                {inscripcion.length === 0 ? (
                  <Button variant="success" onClick={handleInscribir}>
                    Deseas Inscribirte?
                  </Button>
                ) : pago.length === 0 ? (
                  <Button variant="primary" onClick={() => setShowPago(true)}>
                    Realizar Pago
                  </Button>
                ) : (
                  inscripcion.estado === "Completado" && (
                    <Button
                      variant="primary"
                      onClick={() => setShowEncuesta(true)}
                    >
                      {encuesta.length === 0
                        ? "Deseas Responder la Encuesta?"
                        : "Puedes modificar tu Respuesta"}
                    </Button>
                  )
                )}
              </div>
              <div className="">{console.log(encuesta)}</div>
              {showPago && (
                <Modal show={showPago} onHide={() => setShowPago(false)}>
                  <Modal.Header closeButton>
                    <Modal.Title>Realizar Pago</Modal.Title>
                  </Modal.Header>
                  <Modal.Body>
                    <p>¿Estás seguro de que deseas realizar el pago?</p>
                    <p>Este es un pago de {evento.costo} Bs</p>
                  </Modal.Body>
                  <Modal.Footer>
                    <Button
                      variant="secondary"
                      onClick={() => setShowPago(false)}
                    >
                      Cancelar
                    </Button>
                    <Button variant="primary" onClick={handleConfirmPago}>
                      Confirmar Pago
                    </Button>
                  </Modal.Footer>
                </Modal>
              )}
              {showEncuesta && (
                <Encuesta
                  dataEncuesta={encuesta}
                  encuesta={encuesta.value}
                  setEncuesta={setEncuesta}
                  showEncuesta={showEncuesta}
                  setShowEncuesta={setShowEncuesta}
                  data={evento.encuesta}
                  id_Evento={evento.id}
                  id_Inscripcion={inscripcion.id}
                />
              )}
            </>
          ) : (
            <div className="text-center">
              <Login text="Inicia sesión para poder inscribirte en el evento" />
            </div>
          )}
        </>
      )}

      {/* <div>{JSON.stringify(evento.encuesta, null, 2)}</div> */}
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
